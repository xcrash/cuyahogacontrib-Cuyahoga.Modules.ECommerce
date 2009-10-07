using System.Collections;
using System.Collections.Generic;

using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {

	/// <summary>
	/// Takes lines with the same item codes and reduces them to single lines
	/// </summary>
	public class ItemCodeCondenserCommand : IOrderProcessor {

		public ItemCodeCondenserCommand() {
		}

		public void Process(IBasket basket) {

            IBasketLine[] lineArray = new IBasketLine[basket.BasketItemList.Count];
            basket.BasketItemList.CopyTo(lineArray, 0);

            Hashtable original = new Hashtable();

			foreach (IBasketLine line in lineArray) {
				
				if (original.ContainsKey(line.ItemCode)) {

                    IBasketLine orgLine = original[line.ItemCode] as IBasketLine;

                    if (orgLine != null) {
                        orgLine.Quantity += line.Quantity;
                        basket.RemoveItem(line);
                    }

				} else {
                    original.Add(line.ItemCode, line);
				}
			}
		}
	}
}