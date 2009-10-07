
using System;
using System.Collections.Generic;
using Cuyahoga.Modules.ECommerce.Util.Enums;
using log4net;
using Cuyahoga.Web.Controls;
using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Web.UI;
using System.Web.UI.WebControls;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;


namespace Cuyahoga.Modules.ECommerce.Web.Controls {

    public partial class ProductImages : LocalizedModuleConsumerControl {
        

        protected Image imgProduct;
        protected HyperLink hplImagePopUp;

        protected Image imgProduct2;
        protected HyperLink hplImagePopUp2;

        protected Image imgProduct3;
        protected HyperLink hplImagePopUp3;

        protected void Page_Load(object sender, EventArgs e) {

        }

        public void RenderProductImages(List<IImage> imageList) {

            if (imageList.Count > 0) {
                foreach(IImage image in imageList){
                imgProduct.ImageUrl = WebHelper.GetImagePathWeb() + image.Url;

                if (image.WidthSpecified && image.Width > 0) {
                    imgProduct.Width = new Unit(Convert.ToInt32(image.Width));
                }

                if (image.HeightSpecified && image.Height > 0) {
                    imgProduct.Height = new Unit(Convert.ToInt32(image.Height));
                }
                imgProduct.Visible = true;
                hplImagePopUp.NavigateUrl = WebHelper.GetLargeImagePathWeb() + image.Url;
                hplImagePopUp.Attributes.Add("rel", "lightbox");
                hplImagePopUp.Attributes.Add("title", image.AltText);
                }
            } else {

            }                  
        }      
    }
}