using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Cuyahoga.Modules.ECommerce.Util {

    public class SkuParser {

        public IList<IAttributeSelection> ParseSku(string sku) {
            return null;
        }

        public string RenderSku(IList<IAttributeSelection> optionList, string baseSku) {

            Regex re = new Regex(baseSku);

            if (baseSku.Contains("{") && baseSku.Contains("}")) {
            } else {
                return null;
            }

            return null;
        }
    }
}
