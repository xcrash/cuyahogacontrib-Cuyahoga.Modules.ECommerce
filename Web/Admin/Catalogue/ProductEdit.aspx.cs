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

using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using log4net;

using DbProduct = Cuyahoga.Modules.ECommerce.Domain.Product;

namespace Cuyahoga.Modules.ECommerce.Web.Admin {

    public class ProductEdit : ModuleAdminBasePage {

        private int _catID;
        private int _productID;

        protected Label lblSave;
        protected Button btnSave;
        protected Button btnCancel;

        protected ValidationSummary summary;

        protected ImageEditor imageEditor;
        protected CrossSellEditor crossSellEditor;
        protected BaseProductEditor productEditor;
        protected BreadCrumb ctlBreadCrumb;

        protected AttributeEditor attributeEditor;
        protected DocumentEditor documentEditor;
        protected SynonymEditor synonymEditor;

        private List<IActiveProductAttribute> _attributes = new List<IActiveProductAttribute>();
        private IProductView _product;
        private List<Document> _documents;

        protected List<IActiveProductAttribute> AttributeList {
            get { return _attributes; }
            set { _attributes = value; }
        }

        public List<Document> DocumentList {
            get { return _documents; }
            set { _documents = value; }
        }

        public IProductView ProductView {
            get { return _product; }
            set { _product = value; }
        }

        public string Referrer {
            get {
                if (ViewState["Referrer"] != null) {
                    return (string)ViewState["Referrer"];
                } return "";
            }
            set {
                ViewState["Referrer"] = value;
            }
        }

        public int CatID {
            get {
                return _catID;
            }
            set {
                _catID = value;
            }
        }

        public int ProductID {
            get {
                return _productID;
            }
            set {
                _productID = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

            if (!IsPostBack && Request.UrlReferrer != null) {
                Referrer = Request.UrlReferrer.ToString();
            }

            try {

                this.btnSave.Text = "Save product";
                this.btnSave.Click += new EventHandler(btnSave_Click);
                this.btnCancel.Click += new EventHandler(btnCancel_Click);

                CatalogueViewModule controller = Module as CatalogueViewModule;

                DocumentList = controller.CatalogueViewer.GetDocuments(controller.Section.Node.Site.Id, controller.Section.Node.Culture);
                AttributeList = controller.CatalogueViewer.GetCatalogueAttributes(controller.Section.Node.Site.Id, controller.Section.Node.Culture);

                ReadRequestParameters();

                if (ProductID > 0) {
                    ProductView = controller.CatalogueViewer.GetProductView(controller.Section.Node.Site.Id, controller.Section.Node.Culture, ProductID);
                } else {
                    ProductView = null;

                    //Base trail on category, not Product
                    ctlBreadCrumb.RenderBreadCrumbTrail(controller.CatalogueViewer.GetCategoryView(controller.Section.Node.Site.Id, controller.Section.Node.Culture, CatID).BreadCrumbTrail);
                }

                if (ProductView != null) {

                    if (imageEditor != null) {
                        imageEditor.Module = Module;
                    }

                    if (crossSellEditor != null) {
                        crossSellEditor.Module = Module;
                    }

                    crossSellEditor.Product = ProductView.ProductDetails;
                    imageEditor.Product = ProductView.ProductDetails;
                    productEditor.product = ProductView.ProductDetails;

                    documentEditor.product = ProductView.ProductDetails;
                    documentEditor.documents = DocumentList;
                    attributeEditor.attributes = AttributeList;
                    attributeEditor.product = ProductView.ProductDetails;
                    synonymEditor.product = ProductView.ProductDetails;

                    ctlBreadCrumb.RenderBreadCrumbTrail(ProductView.BreadCrumbTrail);
                }
            } catch (System.Threading.ThreadAbortException) {
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private void ReadRequestParameters() {
            try {
                CatID = Int32.Parse(Request.Params[CatalogueBrowser.PARAM_CATEGORY_ID]);
            } catch { }

            try {
                ProductID = Int32.Parse(Request.Params[CatalogueBrowser.PARAM_PRODUCT_ID]);
            } catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect(Referrer);
        }

        private void btnSave_Click(object sender, EventArgs e) {

            ReadRequestParameters();
            CatalogueViewModule controller = Module as CatalogueViewModule;

            DbProduct p;

            try {

                if (!string.IsNullOrEmpty(productEditor.txtProductID.Text)) {
                    p = controller.CatalogueViewer.GetECommerceProduct(1, controller.Section.Node.Culture, Convert.ToInt64(productEditor.txtProductID.Text));
                    p.ProductID = Convert.ToInt64(productEditor.txtProductID.Text);
                } else {
                    p = new DbProduct();
                }

                p.ItemCode = productEditor.txtItemCode.Text;
                p.ProductName = productEditor.txtProductName.Text;
                p.ProductDescription = productEditor.fckDescription.Value;
                p.BasePriceDescription = productEditor.txtPriceDescription.Text;
                p.IsPublished = true;

                if (!string.IsNullOrEmpty(productEditor.txtPrice.Text)) {
                    try {
                        p.BasePrice = Convert.ToDecimal(productEditor.txtPrice.Text);
                    } catch {
                        p.BasePrice = 0; //hmmm
                    }
                } else {
                    p.BasePrice = 0;
                }

                //Kit specific items
                p.ShortProductDescription = productEditor.fckShortDescription.Value;
                p.AdditionalInformation = productEditor.fckKitComprises.Value;
                p.Features = productEditor.fckFeatures.Value;

                List<IImage> images = imageEditor.GetUpdatedImages();
                List<ProductRelation> crossSellList = crossSellEditor.GetUpdatedCrossSellProducts();

                List<ProductAttributeOptionValue> aList = attributeEditor.GetUpdatedAttributes(p.ProductID);
                List<ProductDocument> documentList = documentEditor.GetUpdatedDocuments();
                List<ProductSynonym> synonymList = synonymEditor.GetUpdatedSynonyms();

                if (SaveEditedProduct(p, aList, images, crossSellList, documentList, synonymList)) {

                    //Save has removed old values, adding new ones
                    p.SetProductAttributeValue("weight_kg", productEditor.txtWeight.Text);
                    p.SetProductAttributeValue("finish_type", productEditor.txtFinish.Text);

                    long catID = CatID;

                    if (p.Categories != null && p.Categories.Count > 0) {
                        ProductCategory cat = (ProductCategory)p.Categories[0];
                        catID = cat.CategoryID;
                    }

                    Response.Redirect(CatalogueBrowser.GetBrowserUrl(this, catID, "Product saved successfully"));
                }
            } catch (System.Threading.ThreadAbortException) {
            } catch (Exception f) {
                //Something went wrong, possibly field lengths too large
                LogManager.GetLogger(GetType()).Error(f);
            }

            lblSave.Text = "Your changes have failed to save";
        }

        public bool SaveEditedProduct(DbProduct p, List<ProductAttributeOptionValue> paovList, List<IImage> images, List<ProductRelation> crossSellList, List<ProductDocument> documentList, List<ProductSynonym> synonymList) {

            bool isNewProduct = (p.ProductID == 0);

            CatalogueViewModule controller = Module as CatalogueViewModule;
        	try
        	{
        		controller.EditService.SaveProduct(1, p);
				productEditor.txtProductID.Text = p.ProductID.ToString();
        	}
        	catch (Exception)
        	{
				return false;
        	}

            if (isNewProduct) {
                
                ProductCategory pc = new ProductCategory();
                pc.CategoryID = CatID;
                pc.ProductID = p.ProductID;
                pc.Product = p;
                
                if (!controller.EditService.SaveProductCategory(1, "", pc)) {
                    return false;
                }
            }

            if (p.Images != null) {
                foreach (ProductImage pi in p.Images) {
                    controller.EditService.DeleteProductImage(1, pi);
                }
            }

            if (images != null) {
                foreach (IImage i in images) {
                    ProductImage pi = new ProductImage();
                    ImageHelper.CopyImage(i, pi);
                    pi.ProductID = p;
                    pi.ProductID.ProductID = p.ProductID;
                    controller.EditService.SaveProductImage(1, pi);
                }
            }

            if (p.RelatedProducts != null) {
                foreach (ProductRelation pr in p.RelatedProducts) {
                    controller.EditService.DeleteProductRelation(controller.Section.Node.Site.Id, pr);
                }
            }

            if (crossSellList != null) {
                foreach (ProductRelation pr in crossSellList) {
                    controller.EditService.SaveProductRelation(1, pr);
                }
            }

            if (p.Documents != null) {
                foreach (ProductDocument d in p.Documents) {
                    if (!controller.EditService.DeleteProductDocument(controller.Section.Node.Site.Id, controller.Section.Node.Culture, d)) {
                        return false;
                    }
                }
            }

            if (documentList != null) {
                foreach (ProductDocument pd in documentList) {
                    if (!controller.EditService.SaveProductDocument(controller.Section.Node.Site.Id, controller.Section.Node.Culture, pd)) {
                        return false;
                    }
                }
            }

            if (p.Synonyms != null) {
                foreach (ProductSynonym ps in p.Synonyms) {
                    if (!controller.EditService.DeleteProductSynonym(controller.Section.Node.Site.Id, controller.Section.Node.Culture, ps)) {
                        return false;
                    }
                }
            }

            if (synonymList != null) {
                foreach (ProductSynonym ps in synonymList) {
                    if (!controller.EditService.SaveProductSynonym(controller.Section.Node.Site.Id, controller.Section.Node.Culture, ps)) {
                        return false;
                    }
                }
            }
            
            return true;
        }
    }
}