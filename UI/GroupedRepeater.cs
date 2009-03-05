using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cuyahoga.Modules.ECommerce.Core {

    /// <summary>
    /// Adds headings to rows sharing the same values of a specified property
    /// </summary>
    public class GroupedRepeater : RowIdRepeater {

        private IComparer _comparer = null;
        private object lastvalue = null;

        public GroupedRepeater() {
        }

        protected override void AddParsedSubObject(object obj) {
            base.AddParsedSubObject(obj);
        }

        public IComparer Comparer {
            get { return _comparer; }
            set { _comparer = value; }
        }

        protected override void CreateChildControls() {
            lastvalue = null;
            base.CreateChildControls();
        }

        /// <summary>
        /// Checks the current item to see if it differs from the previous item
        /// </summary>
        /// <param name="item">item to be checked</param>
        /// <returns><c>true</c> if this item is different from the previous</returns>
        /// <remarks>Calling this method again on the same item will always return false</remarks>
        public bool IsNewGroupItem(RepeaterItem item) {

            //See if this is a data item with a new value
            if ((item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                && item.DataItem != null && _comparer.Compare(lastvalue, item.DataItem) != 0) {

                //remeber this value
                lastvalue = item.DataItem;
                return true;
            }

            return false;
        }
    }
}
