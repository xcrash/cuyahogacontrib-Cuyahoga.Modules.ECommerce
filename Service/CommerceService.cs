using System;
using System.Collections.Generic;

using Cuyahoga.Core.Domain;

using Cuyahoga.Modules.ECommerce.DataAccess;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Service;
using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;
using Cuyahoga.Modules.ECommerce.Service.Translation;
using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;
using Cuyahoga.Modules.ECommerce.Util.Location;
using Cuyahoga.Modules.ECommerce.Service.Email;

namespace Cuyahoga.Modules.ECommerce.Service {

    public class CommerceService : ICommerceService {

        private IPaymentProvider _provider;
        private ICommerceDao _dao;
        private IExtCommonDao _commonDao;
        private IBasketRules _rules;
        private IChargeService _chargeService;
        private IOrderProcessorFactory _processorFactory;

        public CommerceService(IExtCommonDao commonDao, ICommerceDao dao, IPaymentProvider provider, IBasketRules rules, IOrderProcessorFactory processorFactory, ITemplateEngine templateEngine, IChargeService chargeService) {
            _commonDao = commonDao;
            _dao = dao;
            _provider = provider;
            _rules = rules;
            _chargeService = chargeService;
            _processorFactory = processorFactory;
		}

        public IBasket GetCurrentBasket(IStoreContext context) {
            if (context.CurrentBasket == null) {
                RetrieveBasketAndLastOrder(context);
            }
            return context.CurrentBasket;
        }

        public void SetCurrentBasket(IStoreContext context, IBasket basket) {
            if (basket != null) {
                context.CurrentBasket = basket;
                context.BasketID = basket.BasketID;
            } else {
                context.BasketID = StoreContext.ID_NULL;
                context.IsBasketEmpty = true;
            }
        }

        private void RetrieveBasketAndLastOrder(IStoreContext context) {

            if (context.CurrentBasket == null && context.BasketID > 0) {
                context.CurrentBasket = GetBasket(context.BasketID);

                //If we can't find the basket, no point looking again in future
                if (context.CurrentBasket == null) SetCurrentBasket(context, null);
            }

            //Set this as the last order, if necessary
            if (context.CurrentBasket != null) {

                BasketDecorator cbd = new BasketDecorator(context.CurrentBasket);

                if (cbd.IsPurchased || cbd.IsAwaitingAccountPaymentApproval) {

                    //Update the order with basket details
                    context.LastOrder = context.CurrentBasket;
                    context.CurrentBasket = null;

                    return;
                }
            }

            if (context.LastOrder == null && context.LastOrderID > 0) {

                context.LastOrder = GetBasket(context.LastOrderID);

                //If we can't find the last order, no point looking again in future
                if (context.LastOrder == null) context.LastOrderID = StoreContext.ID_NULL;
            }

            //Make sure this order is still current
            if (context.LastOrder != null) {

                BasketDecorator lod = new BasketDecorator(context.LastOrder);

                if (!lod.IsPurchased && !lod.IsAwaitingAccountPaymentApproval) {
                    context.CurrentBasket = context.LastOrder;
                    context.LastOrder = null;
                }
            }
        }

        public IBasket GetLastOrder(IStoreContext context) {
            if (context.LastOrder == null) {
                RetrieveBasketAndLastOrder(context);
            }
            return context.LastOrder;
        }

        public void SetLastOrder(IStoreContext context, IBasket order) {
            context.LastOrder = order;
            if (order != null) {
                context.LastOrderID = order.BasketID;
            } else {
                context.LastOrderID = StoreContext.ID_NULL;
            }
        }

		/// <summary>
		/// Gets either the current basket or creates a new one
		/// </summary>
		/// <returns></returns>
		public IBasket GetOrCreateBasket(IStoreContext context) {

			if (GetCurrentBasket(context) == null) {
				context.CurrentBasket = CreateBasket(context);
			}

			return context.CurrentBasket;
		}

        public IBasketLine AddItem(IBasket basket, string itemCode, int quantity) {

            CheckAddItemRequest(basket.UserDetails, quantity);

            Basket b = basket as Basket;
            if (b.BasketID == 0) {
                _dao.Save(b);
            }

            return basket.AddItem(itemCode, quantity);
        }

        public IBasketLine AddItem(IStoreContext context, string itemCode, int quantity) {

            CheckAddItemRequest(context, quantity);

			IBasket basket = GetOrCreateBasket(context);
			return basket.AddItem(itemCode, quantity);
		}

        public IBasketLine AddItem(IStoreContext context, string itemCode, IList<IAttributeSelection> optionList, int quantity) {

            CheckAddItemRequest(context, quantity);

            Basket basket = GetOrCreateBasket(context) as Basket;
            return basket.AddItem(itemCode, quantity);
        }

        public IBasketLine AddItem(IStoreContext context, long productID, IList<IAttributeSelection> optionList, int quantity) {

            Product product = _commonDao.GetObjectById(typeof(Product), productID) as Product;

            if (product != null) {
                return AddItem(context, product, optionList, quantity);
            }

            return null;
        }

        public IBasketLine AddItem(IStoreContext context, Product product, int quantity) {

            CheckAddItemRequest(context, quantity);

            Basket basket = GetOrCreateBasket(context) as Basket;
            return basket.AddItem(product, quantity);
        }

        public IBasketLine AddItem(IStoreContext context, Product product, IList<IAttributeSelection> optionList, int quantity) {

            CheckAddItemRequest(context, quantity);

            Basket basket = GetOrCreateBasket(context) as Basket;
            return basket.AddItem(product, optionList, quantity);
        }

        private void CheckAddItemRequest(IStoreContext context, int quantity) {
            CheckAddItemRequest(context.CurrentUser, quantity);
        }

        private void CheckAddItemRequest(User user, int quantity) {

            if (!_rules.AllowAddToBasket(user)) {
                throw new InvalidOperationException("Current user not allowed to add to basket");
            }

            if (quantity < 1) {
                throw new ArgumentException("Invalid quantity");
            }
        }

        public void RemoveItem(IStoreContext context, long basketLineID) {
            IBasketLine line = _dao.FindBasketLine(basketLineID);
            GetCurrentBasket(context).RemoveItem(line);
        }

        public void AmendItemQuantity(IStoreContext context, long basketLineID, int newQuantity) {
            IBasketLine line = _dao.FindBasketLine(basketLineID);
            line.Quantity = newQuantity;
            line.Status = PricingStatus.NotChecked;
        }

		//Calls all the processes registered to manipulate a basket
        public void RefreshBasket(IStoreContext context) {

			IBasket basket = GetCurrentBasket(context);

			if (basket != null) {

				RefreshBasket(basket);

				context.BasketID = basket.BasketID;
				context.CurrencyCode = basket.CurrencyCode;
				context.IsBasketEmpty = new BasketDecorator(basket).IsEmpty;

			} else {
				context.BasketID = 0;
				context.IsBasketEmpty = true;
			}

			context.NotifyBasketChanged(basket);
		}

		public bool CloseCurrentOrder(IStoreContext context, ITextTranslator translator) {

			IBasket order = GetCurrentBasket(context);
            BasketDecorator decorator = order as BasketDecorator;

            if (decorator != null) {
                order = decorator.Basket;
            }
			
			if (order != null && CloseOrder(order, translator)) {

                SetLastOrder(context, order);
                SetCurrentBasket(context, null);
				
				return true;

			} else {
				return false;
			}
		}

        public bool CloseOrder(IBasket order, ITextTranslator translator) {

            BasketDecorator b = new BasketDecorator(order);
            if (b.IsPurchased) {
                throw new InvalidOperationException("Cannot close an order that is already purchased");
            }

            order.OrderHeader.Status = Util.Enums.OrderStatus.OrderedPaid;
            order.OrderHeader.OrderedDate = DateTime.Now;

            IOrderProcessor processor = _processorFactory.GetCloseProcessor();

            if (processor != null) {
                processor.Process(order);
            }

            //And save the results
            _dao.Save(order);
            return true;
        }

        public bool SubmitCurrentOrder(IStoreContext context, ITextTranslator translator) {

            IBasket order = GetCurrentBasket(context);

            if (order != null && SubmitOrder(order, translator)) {

                SetLastOrder(context, order);
                SetCurrentBasket(context, null);

                return true;

            } else {
                return false;
            }
        }

        public bool SubmitOrder(IBasket order, ITextTranslator translator) {

            BasketDecorator b = new BasketDecorator(order);
            if (b.IsPurchased || b.OrderHeader.Status == OrderStatus.OrderedSubmittedForAccountPayment) {
                throw new InvalidOperationException("Cannot close an order that is already purchased or submitted");
            }

            order.OrderHeader.Status = Util.Enums.OrderStatus.OrderedSubmittedForAccountPayment;
            order.OrderHeader.OrderedDate = DateTime.Now;

            IOrderProcessor processor = _processorFactory.GetProcessor("order.submit");

            if (processor != null) {
                processor.Process(order);
            }

            //And save the results
            _dao.Save(order);
            return true;
        }

        public IBasket GetBasket(long basketID) {
            return _dao.FindBasket(basketID);
        }

        public IBasket CreateBasket(IStoreContext context) {
            IBasket basket = _dao.CreateBasket(context);
            return basket;
        }

        public void ChangeCurrencyCode(IStoreContext context, string currencyCode) {
            context.CurrencyCode = currencyCode;
            ChangeCurrencyCode(GetCurrentBasket(context), currencyCode);
        }

        public void ChangeCurrencyCode(IBasket basket, string currencyCode) {
            if (string.Compare(basket.CurrencyCode, currencyCode, true) != 0) {
                basket.CurrencyCode = currencyCode.ToUpper();
            }
        }

        //Calls all the processes registered to manipulate a basket
        public virtual void RefreshBasket(IBasket basket) {

            if (basket == null) {
                return;
            }

            IOrderProcessor processor = _processorFactory.GetRefreshProcessor();
           
            _processorFactory.SetMinimumDeliveryCharge(_chargeService.GetMinimumDeliveryCharge(basket.CultureCode)); 

            if (processor != null) {
               
                processor.Process(basket);
            }

            if (basket.OrderHeader != null) {

                if (basket.OrderHeader.InvoiceAddress != null) {
                    _dao.Save(basket.OrderHeader.InvoiceAddress);
                }
                if (basket.OrderHeader.DeliveryAddress != null) {
                    _dao.Save(basket.OrderHeader.DeliveryAddress);
                }

                _dao.Save(basket.OrderHeader);
            }

            _dao.Save(basket);
        }

        public void EmptyBasket(IStoreContext context) {
            BasketDecorator basket = new BasketDecorator(GetCurrentBasket(context));
            basket.Empty();
        }

        public List<IPaymentProvider> GetAllPaymentProviders() {

            List<IPaymentProvider> providers = new List<IPaymentProvider>();

            using (SpHandler sph = new SpHandler("getPaymentProviders")) {

                sph.ExecuteReader();

                while (sph.DataReader.Read()) {

                    int attributeID = Convert.ToInt32(sph.DataReader["attributeID"]);

                   
                    
                }
            }

            return providers;
        }

        public List<IPaymentProvider> GetEnabledPaymentProviders() {
            List<IPaymentProvider> providers = new List<IPaymentProvider>();

            using (SpHandler sph = new SpHandler("getPaymentProviders")) {

                sph.ExecuteReader();

                while (sph.DataReader.Read()) {

                    int attributeID = Convert.ToInt32(sph.DataReader["attributeID"]);



                }
            }

            return providers;
        }
    }
}
