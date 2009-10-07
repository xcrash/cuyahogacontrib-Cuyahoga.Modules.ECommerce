using System.Collections.Generic;
using System.Collections;
using Igentics.Common.ECommerce;
using Igentics.Common.ECommerce.DataTransferObjects;

namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

	/// <summary>
	/// Takes lines with the same item codes and reduces them to single lines
	/// </summary>
	public class ItemCodeCondenser : IOrderProcessor {

        public ItemCodeCondenser() {
		}

		public ProcessStatusMessage Process(Basket basket) {

            BasketItem[] lineArray = new BasketItem[basket.BasketItemList.Count];
            basket.BasketItemList.CopyTo(lineArray, 0);

            Hashtable original = new Hashtable();

            foreach (BasketItem line in lineArray) {

                string key = GetItemKey(line);
				
				if (original.ContainsKey(key)) {

                    BasketItem orgLine = original[line.ItemCode] as BasketItem;

                    if (orgLine != null) {
                        orgLine.Quantity += line.Quantity;
                        basket.BasketItemList.Remove(line);
                    }

				} else {
                    original.Add(key, line);
				}
			}

            return new ProcessStatusMessage(ProcessStatus.Success);
		}

        protected virtual string GetItemKey(BasketItem line) {

            string key = line.ItemCode;
            
            if (line.OptionList != null) {
                foreach (AttributeOptionChoice choice in line.OptionList) {
                    key += "__" + choice.AttributeID + "__" + choice.OptionID;
                }
            }

            return key;
        }
	}
}