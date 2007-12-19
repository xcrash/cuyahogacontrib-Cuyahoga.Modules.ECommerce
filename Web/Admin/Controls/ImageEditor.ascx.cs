using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cuyahoga.Web.UI;
using Cuyahoga.Core.Util;

using Cuyahoga.Core;
using Cuyahoga.Core.Domain;
using Cuyahoga.Core.Service;
using Cuyahoga.Core.Search;

using Cuyahoga.Core.Communication;
using Cuyahoga.Modules.ECommerce.Web.Admin.Controls;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce;
using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Guild.WebControls;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Controls {

    public class WebImageMakerAdapter : Domain.Catalogue.Image {
        public WebImageMakerAdapter(WebImageMaker imaker) {
            AltText = "";
            Height = Convert.ToInt16(imaker.ImageHeight);
            Width = Convert.ToInt16(imaker.ImageWidth);
            Url = WebHelper.GetImageUrl(imaker.WebImagePath);
        }
    }

    public class ImageEditor : LocalizedModuleConsumerControl {

        private const string ID_WIM = "wim";
        private const string ID_REMOVE = "chkRemove";

        protected LinkButton lnkAddImages;
        protected PlaceHolder plhImages;
        protected PlaceHolder plhImagesAdditions;

        private IProduct _product;

        private int _width = 0;
        private int _height = 0;

        public int ImageWidth {
            get { return _width; }
            set { _width = value; }
        }

        public int ImageHeight {
            get { return _height; }
            set { _height = value; }
        }

        public IProduct Product {
            get { return _product; }
            set { _product = value; }
        }

        private int AdditionalImageCount {
            get {
                if (ViewState["images"] != null) {
                    return (int)ViewState["images"];
                } return 0;
            }
            set {
                ViewState["images"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

            plhImages.EnableViewState = true;
            lnkAddImages.Click += new EventHandler(lnkAddImages_Click);

            DisplayExistingImages();

            int totalImageCount = AdditionalImageCount;
            if (Product != null) {
                totalImageCount += Product.ProductImages.Count;
            }

            for (int i = 0; i < AdditionalImageCount; i++) {
                AddEmptyImageMaker(plhImagesAdditions);
            }

            lnkAddImages.Visible = (totalImageCount < MaxProductImages);
        }

        private void AddEmptyImageMaker(Control ctrl) {
            ctrl.Controls.Add(new LiteralControl("<tr><td>"));
            ctrl.Controls.Add(CreateImageMaker());
            ctrl.Controls.Add(new LiteralControl("</td><td>&nbsp;</td></tr>"));
        }

        private void DisplayExistingImages() {
            int i = 0;
            if (Product != null) {
                foreach (IImage pi in Product.ProductImages) {

                    plhImages.Controls.Add(new LiteralControl("<tr><td>"));

                    plhImages.Controls.Add(CreateImageMaker(ID_WIM + i, pi));

                    plhImages.Controls.Add(new LiteralControl("</td><td>"));

                    CheckBox chkBox = new CheckBox();
                    chkBox.ID = ID_REMOVE + i;
                    chkBox.EnableViewState = true;
                    chkBox.Enabled = true;
                    plhImages.Controls.Add(chkBox);

                    plhImages.Controls.Add(new LiteralControl("</td></tr>"));
                    
                    i++;
                }
            }
        }

        public List<IImage> GetUpdatedImages() {

            List<IImage> images = new List<IImage>();
            CatalogueViewModule controller = Module as CatalogueViewModule;

            //check exsiting images were not deleted
            int i = 0;
            if (Product != null) {

                while (i < Product.ProductImages.Count) {
                    
                    CheckBox chkBox = plhImages.FindControl(ID_REMOVE + i) as CheckBox;
                    WebImageMaker wim = plhImages.FindControl(ID_WIM + i) as WebImageMaker;

                    if (!chkBox.Checked && wim != null) {
                        images.Add(new WebImageMakerAdapter(wim));
                    }
                    i++;
                }
            }

            foreach (Control ctrl in plhImagesAdditions.Controls) {

                WebImageMaker imaker = ctrl as WebImageMaker;

                if (imaker != null && imaker.WebImagePath != null) {
                    images.Add(new WebImageMakerAdapter(imaker));
                }
            }

            return images;
        }

        private void lnkAddImages_Click(object sender, EventArgs e) {

            int totalImageCount = 0;
            if (Product != null) {
                totalImageCount = totalImageCount + Product.ProductImages.Count;
            }

            totalImageCount += AdditionalImageCount;

            if (totalImageCount < MaxProductImages) {
                
                AdditionalImageCount++;
                totalImageCount++;

                AddEmptyImageMaker(plhImagesAdditions);
                lnkAddImages.Visible = (totalImageCount < MaxProductImages);
            }
        }

        private int MaxProductImages {
            get {
                try {
                    return Convert.ToInt32(ConfigurationSettings.AppSettings["MaxProductImages"]);
                } catch {
                    return 1;
                }
            }
        }

        private WebImageMaker CreateImageMaker() {
            return CreateImageMaker(null, null);
        }

        private WebImageMaker CreateImageMaker(string id, IImage image) {
            
            WebImageMaker wim = new WebImageMaker();
            
            wim.ID = AdditionalImageCount.ToString();
            wim.WorkingDirectory = WebHelper.GetImageWorkingDirectory();
            wim.ConfirmButtonText = "Confirm";
            wim.CancelButtonText = "Cancel";
            wim.UploadButtonText = "Upload";
            wim.HandlerPath = "~/WebImageMakerHandler.ashx";
            wim.EnableViewState = true;
            wim.Quality = WebImageQuality.High;

            if (!string.IsNullOrEmpty(id)) {
                wim.ID = id;
            }

            if (image != null) {
                wim.ImageHeight = image.Height.ToString();
                wim.ImageWidth = image.Width.ToString();
                wim.ImageUrl = WebHelper.GetImagePathWeb() + image.Url;
            } else {
                wim.ImageHeight = (ImageHeight > 0) ? ImageHeight.ToString() : "*";
                wim.ImageWidth = (ImageWidth > 0) ? ImageWidth.ToString() : "*";
            }

            return wim;
        }
    }
}