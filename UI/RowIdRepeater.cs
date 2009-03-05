using System;
using System.Reflection;
using System.Web.UI.WebControls;

namespace Cuyahoga.Modules.ECommerce.Core {

	/// <summary>
	/// Summary description for RowIdRepeater.
	/// </summary>
	public class RowIdRepeater : Repeater {

		private string _rowIdDataName = "";

		public RowIdRepeater() {
		}

		public string RowIdDataName {
			get {
				return _rowIdDataName;
			}
			set {
				_rowIdDataName = value;
			}
		}

		protected override RepeaterItem CreateItem(int itemIndex, ListItemType itemType) {
			if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem) { //Plus others??
				RowIdRepeaterItem item = new RowIdRepeaterItem(itemIndex, itemType);
				item.DataBinding += new EventHandler(item_DataBinding);
				return item;
			} else {
				return base.CreateItem (itemIndex, itemType);
			}
		}

		private void SetRowID(RowIdRepeaterItem rItem) {
			if (RowIdDataName != null && RowIdDataName.Length > 0) {
				rItem.RowID = rItem.DataItem.GetType().InvokeMember(RowIdDataName, BindingFlags.GetProperty, null, rItem.DataItem, null);
			}
		}

		private void item_DataBinding(object sender, EventArgs e) {
			RowIdRepeaterItem item = (RowIdRepeaterItem) sender;
			SetRowID(item);
		}
	}

	public class RowIdRepeaterItem : RepeaterItem {

		public RowIdRepeaterItem(int itemIndex, ListItemType itemType) : base(itemIndex, itemType) {
		}

		public object RowID {
			//How are these going to be cleared up?
			get {
				return ViewState[ClientID + "_ID"];
			}
			set {
				ViewState[ClientID + "_ID"] = value;
			}
		}
	}
}