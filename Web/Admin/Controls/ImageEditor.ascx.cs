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
using Cuyahoga.Modules.ECommerce.Util.Enums;

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Controls {

    public class WebImageMakerAdapter : Domain.Catalogue.Image {

        public WebImageMakerAdapter(WebImageMaker imaker, short type) {

            AltText = "";

            if (!string.IsNullOrEmpty(imaker.ImageHeight)) {
                try {
                    Height = Convert.ToInt16(imaker.ImageHeight);
                } catch {
                    Height = 0;
                }
            }

            if (!string.IsNullOrEmpty(imaker.ImageWidth)) {
                try {
                    Width = Convert.ToInt16(imaker.ImageWidth);
                } catch {
                    Width = 0;
                }
            }
            if (imaker.WebImagePath != null) {
                if (type == (short)Cuyahoga.Modules.ECommerce.Util.Enums.ImageType.Large) {
                    Url = WebHelper.GetLargeImageUrl(imaker.WebImagePath);
                } else {
                     Url = WebHelper.GetImageUrl(imaker.WebImagePath);
                }
            } else {

                if (type == (short)Cuyahoga.Modules.ECommerce.Util.Enums.ImageType.Large) {
                     Url = imaker.ImageUrl.Remove(0, WebHelper.GetLargeImagePathWeb().Length);
                } else {
                    Url = imaker.ImageUrl.Remove(0, WebHelper.GetImagePathWeb().Length);
                }
            }

            ImageType = type;


        }

    }

    public class HtmlInputImageAdapter : Domain.Catalogue.Image {

        private System.Drawing.Image ConvertToImage(HtmlInputFile file) {

            if (file.PostedFile.ContentLength > 0) {

                 try {
                     using (System.Drawing.Image input =  System.Drawing.Image.FromStream(file.PostedFile.InputStream)) {
                         file.PostedFile.SaveAs(WebHelper.GetImageWorkingDirectory() + "web\\Large\\" + file.PostedFile.FileName);
                                return input;
                      }
                 } catch {
                 }
             }

            return null;
        }

        public HtmlInputImageAdapter(HtmlInputFile input) {

            AltText = "";
            System.Drawing.Image image;
            if (input.PostedFile != null) {
                
               image = ConvertToImage(input);
                 try {
                     Height = Convert.ToInt16(image.Height);
                } catch {
                     Height = 0;
                }

               try {
                    Width = Convert.ToInt16(image.Width);
                } catch {
                    Width = 0;
                }

                Url = input.PostedFile.FileName;
                ImageType = (short)Cuyahoga.Modules.ECommerce.Util.Enums.ImageType.Large;
            }
           
        }

            
        
    }

    public class ImageEditor : LocalizedModuleConsumerControl {

        private const string ID_WIM = "wim";
        private const string ID_REMOVE = "chkRemove";

        protected LinkButton lnkAddImages;
        protected LinkButton lnkAddLargeImages;
        protected PlaceHolder plhImages;
        protected PlaceHolder plhImagesAdditions;
        protected PlaceHolder plhLargeImagesAdditions;
        protected PlaceHolder plhLargeImages;

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

        private int AdditionalLargeImageCount {
            get {
                if (ViewState["largeImages"] != null) {
                    return (int)ViewState["largeImages"];
                } return 0;
            }
            set {
                ViewState["largeImages"] = value;
            }
        }

        private string AdditionalLargeImageType {
            get {
                if (ViewState["largeImageType"] != null) {
                    return (string)ViewState["largeImageType"];
                } return Convert.ToString(ImageUploadType.Simple);
            }
            set {
                ViewState["largeImageType"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

            plhImages.EnableViewState = true;
            lnkAddImages.Click += new EventHandler(lnkAddImages_Click);
            lnkAddLargeImages.Click += new EventHandler(lnkAddLargeImages_Click);

            DisplayExistingImages();

            int totalImageCount = (AdditionalImageCount + AdditionalLargeImageCount);
            if (Product != null) {
                totalImageCount += Product.ProductImages.Count;
            }

            for (int i = 0; i < AdditionalImageCount; i++) {
                AddImageMaker(plhImagesAdditions, i.ToString());
            }
            
            for (int i = 0; i < AdditionalLargeImageCount; i++) {
                if (AdditionalLargeImageType == Convert.ToString(ImageUploadType.Simple)) {
                    AddEmptySimpleUpload(plhLargeImagesAdditions);
                } else {
                    AddEmptyImageMaker(plhLargeImagesAdditions);
                }
            }

            lnkAddLargeImages.Visible = lnkAddImages.Visible = (totalImageCount < MaxProductImages);
        }

        private void AddImageMaker(Control ctrl, string id) {
            ctrl.Controls.Add(new LiteralControl("<tr><td>"));
            ctrl.Controls.Add(CreateImageMaker(id, null));
            ctrl.Controls.Add(new LiteralControl("</td><td>&nbsp;</td></tr>"));
        }

        private void AddEmptyImageMaker(Control ctrl) {
            ctrl.Controls.Add(new LiteralControl("<tr><td>"));
            ctrl.Controls.Add(CreateImageMaker());
            ctrl.Controls.Add(new LiteralControl("</td><td>&nbsp;</td></tr>"));
        }

        private void AddEmptySimpleUpload(PlaceHolder plh) {
            plh.Controls.Add(new LiteralControl("<tr><td>"));
            plh.Controls.Add(new HtmlInputFile());
            Button uploadButton = new Button();
            uploadButton.Text = "Upload";
            uploadButton.Click += new EventHandler(uploadButton_Click);
            plh.Controls.Add(uploadButton);
            plh.Controls.Add(new LiteralControl("</td><td>&nbsp;</td></tr>"));
        }

        protected void uploadButton_Click(object sender, EventArgs e) {
            foreach (Control ctrl in plhLargeImagesAdditions.Controls) {

                HtmlInputFile input = ctrl as HtmlInputFile;

                if (input != null && input.PostedFile != null) {
                    try {
                        
                        WebImageMaker iMaker = CreateImageMaker(null, new HtmlInputImageAdapter(input));


                        plhLargeImages.Controls.Add(new LiteralControl("<tr><td>"));
                        plhLargeImages.Controls.Add(iMaker);
                        plhLargeImages.Controls.Add(new LiteralControl("</td><td>&nbsp;</td></tr>"));

                        AdditionalLargeImageType = Convert.ToString(ImageType.Large);
                        
                    } catch (Exception ex) {}
                }
            }
            
            plhLargeImagesAdditions.Controls.Clear();
            
            
        }

        private void DisplayExistingImages() {
            int i = 0;
            if (Product != null) {
                foreach (IImage pi in Product.ProductImages) {

                    if (pi.ImageType != (short)ImageType.Large) {

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
                    } else {
                        plhLargeImages.Controls.Add(new LiteralControl("<tr><td>"));

                        plhLargeImages.Controls.Add(CreateImageMaker(ID_WIM + i, pi));

                        plhLargeImages.Controls.Add(new LiteralControl("</td><td>"));

                        CheckBox chkBox = new CheckBox();
                        chkBox.ID = ID_REMOVE + i;
                        chkBox.EnableViewState = true;
                        chkBox.Enabled = true;
                        plhLargeImages.Controls.Add(chkBox);

                        plhLargeImages.Controls.Add(new LiteralControl("</td></tr>"));

                        i++;
                    }
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

                    if (!chkBox.Checked && wim != null && Product.ProductImages[i].ImageType == Convert.ToInt16(ImageType.Small)) {
                        images.Add(new WebImageMakerAdapter(wim,Convert.ToInt16(ImageType.Small)));
                    }
                    i++;
                }
            }

            foreach (Control ctrl in plhImagesAdditions.Controls) {

                WebImageMaker imaker = ctrl as WebImageMaker;

                if (imaker != null && imaker.WebImagePath != null) {
                    images.Add(new WebImageMakerAdapter(imaker, Convert.ToInt16(ImageType.Small)));
                }
            }
            i = 0;
            if (Product != null && Product.ProductImages != null) {
                while (i < Product.ProductImages.Count) {

                    CheckBox chkBox = plhLargeImages.FindControl(ID_REMOVE + i) as CheckBox;
                    WebImageMaker wim = plhLargeImages.FindControl(ID_WIM + i) as WebImageMaker;

                    if (!chkBox.Checked && wim != null && Product.ProductImages[i].ImageType == Convert.ToInt16(ImageType.Large)) {
                        images.Add(new WebImageMakerAdapter(wim, Convert.ToInt16(ImageType.Large)));
                    }
                    i++;
                }
            }

            foreach (Control ctrl in plhLargeImagesAdditions.Controls) {

                WebImageMaker imaker = ctrl as WebImageMaker;
                
                if (imaker != null) {
                    images.Add(new WebImageMakerAdapter(imaker, Convert.ToInt16(ImageType.Large)));
                }
            }

            return images;
        }

        private void AddImage(PlaceHolder plh, ImageUploadType type){

            int totalImageCount = 0;
            if (Product != null && Product.ProductImages != null) {
                totalImageCount = totalImageCount + Product.ProductImages.Count;
            }

            totalImageCount += (AdditionalImageCount + AdditionalLargeImageCount);

            if (totalImageCount < MaxProductImages) {

                totalImageCount++;

                if (type == ImageUploadType.ImageEditor) {
                    AddEmptyImageMaker(plh);
                    AdditionalImageCount++;
                } else {
                    AddEmptySimpleUpload(plh);
                    AdditionalLargeImageCount++;
                }

                //plh.Visible = (totalImageCount < MaxProductImages);
            }
        }



        private void lnkAddImages_Click(object sender, EventArgs e) {
            AddImage(plhImagesAdditions, ImageUploadType.ImageEditor);

        }

        private void lnkAddLargeImages_Click(object sender, EventArgs e) {

            AddImage(plhLargeImagesAdditions, ImageUploadType.Simple);
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
                if (image.ImageType == (short)Cuyahoga.Modules.ECommerce.Util.Enums.ImageType.Large) {
                    wim.ImageUrl = WebHelper.GetLargeImagePathWeb() + image.Url;                  
                } else {
                    wim.ImageUrl = WebHelper.GetImagePathWeb() + image.Url;
                }

                wim.ImageHeight = (image.Height > 0) ? ImageHeight.ToString() : "*";
                wim.ImageWidth = (image.Width > 0) ? ImageWidth.ToString() : "*";
            } else {
                wim.ImageHeight = (ImageHeight > 0) ? ImageHeight.ToString() : "150";
                wim.ImageWidth = (ImageWidth > 0) ? ImageWidth.ToString() : "*";
            }

            return wim;
        }
    }
}