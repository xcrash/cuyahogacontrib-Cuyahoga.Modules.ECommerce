using System;
using System.Collections.Generic;
using System.Collections;
using System.Web.UI.WebControls;

using log4net;

using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;

using Cuyahoga.Web.UI;

namespace Cuyahoga.Modules.ECommerce.Web.Views {
    public class Popup : BaseModuleControl {
       
        private CatalogueViewModule _mod;
        private CatalogueViewModule CatMod {
            get {
                if (_mod == null) {
                    _mod = Module as CatalogueViewModule;
                }
                return _mod;
            }
        }

        protected Image imgDisplayImage;
        protected Label lblMessage;

        protected void Page_Load(object sender, EventArgs e) {
           IImage image = CatMod.CatalogueViewer.GetImageByItemCode(CatMod.Section.Node.Site.Id, CatMod.Section.Node.Culture, CatMod.ItemCode);
           if (image != null) {
               imgDisplayImage.ImageUrl = WebHelper.GetImagePathWeb() + image.Url;
               lblMessage.Visible = false;
               imgDisplayImage.Visible = true;
           } else {
               lblMessage.Visible = true;
               imgDisplayImage.Visible = false;
               lblMessage.Text = "Sorry, we could not find the requested image.";
           }
        }
    }
}