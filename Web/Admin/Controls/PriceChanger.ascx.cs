using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
namespace Cuyahoga.Modules.ECommerce.Web.Admin.Controls {
    public class PriceChanger : System.Web.UI.UserControl {
        
        public const string PARAM_CAT_ID = "cat";
        public const string PARAM_PRODUCT_ID = "pid";

        public long CategoryID {
            get {
                return _catID;
            }
        }

        private long _catID;

        public string PercentageChange {
            get {
                return _percentageChange;
            }
        }

        private string _percentageChange;

        protected void Page_Load(object sender, EventArgs e) {
            try {
                _catID = Int32.Parse(Request.Params[PARAM_CAT_ID]);
            } catch {
            }

            _percentageChange = Request.Params[PARAM_CAT_ID];
        
            //could use NHibernate to update all the prices but that would be slow as hell

            if (PercentageChange != null || PercentageChange != String.Empty) {
                UpdatePriceByPercentage();
            }
        }

        private void UpdatePriceByPercentage() { // FIXME need to abstract these connection strings
            //going to loop through the sub categories here to make the sql easier
            using (SpHandler sph = new SpHandler("UpdateCategoryPriceByPercentage", new SqlParameter("@catID", CategoryID), new SqlParameter("@change", PercentageChange))) {
                sph.ConnectionString = "server=dev-sql-2000;database=albioncms;password=SQLServer;uid=sa";
                sph.ExecuteNonQuery();
            }
        }
    }
}