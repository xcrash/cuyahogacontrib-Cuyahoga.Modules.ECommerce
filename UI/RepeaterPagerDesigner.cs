using System;

using System.Web.UI.Design;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Koutny.WebControls
{
	/// <summary>
	/// Control designer for RepeaterPager.
	/// </summary>
	public class RepeaterPagerDesigner : ControlDesigner
	{
		public override string			GetDesignTimeHtml()
		{
			return "<div><strong>RepeaterPager v1.0</strong> <br /> &laquo; previous | 1 2 3 ... 8 | next &raquo;</div>";
		}
	}
}