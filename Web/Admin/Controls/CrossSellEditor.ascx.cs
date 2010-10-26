using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using Cuyahoga.Web.Admin.Controls;
//using Cuyahoga.Web.Admin;
using Cuyahoga.Core.Domain;
//using Cuyahoga.Web.Admin.UI;
using Cuyahoga.Web.UI;
using Cuyahoga.Core.Util;
using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Guild.WebControls;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Util.Enums;

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Controls {

    public partial class CrossSellEditor : LocalizedModuleConsumerControl {

        protected LinkButton lnkAddCrossSell;
        protected PlaceHolder plhCrossSellAdditions;
        protected PlaceHolder plhCrossSell;

        private IProduct _product;
        private CatalogueViewModule controller;
        private RelatedProductType _relationType = RelatedProductType.CrossSell;

        public IProduct Product {
            get { return _product; }
            set { _product = value; }
        }

        private int CrossSellAdditionCount {
            get {
                if (ViewState["crossSellProducts"] != null) {
                    return (int)ViewState["crossSellProducts"];
                } return 0;
            }
            set {
                ViewState["crossSellProducts"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

            controller = Module as CatalogueViewModule;

            if (Product != null) {
                RenderCrossSellCurrent();
            }

            lnkAddCrossSell.Click += new EventHandler(lnkAddCrossSell_Click);
            RenderCrossSellAdditions();
        }

        public RelatedProductType RelationType {
            get { return _relationType; }
            set { _relationType = value; }
        }

        private void RenderCrossSellCurrent() {
            foreach (RelatedProducts rp in Product.CrossSellList) {

                plhCrossSell.Controls.Add(new LiteralControl("<tr><td>"));

                TextBox txt = new TextBox();
                txt.ID = "txtOriginalCrossSell" + rp.AccessoryPartNo;
                txt.Text = rp.AccessoryPartNo;
                txt.Visible = false;
                plhCrossSell.Controls.Add(txt);
                plhCrossSell.Controls.Add(new LiteralControl(Server.HtmlEncode(rp.AccessoryPartNo)));

                plhCrossSell.Controls.Add(new LiteralControl("</td><td>"));

                plhCrossSell.Controls.Add(new LiteralControl(Server.HtmlEncode(rp.AccessoryName)));

                plhCrossSell.Controls.Add(new LiteralControl("</td><td>"));

                CheckBox chk = new CheckBox();
                chk.ID = "chkOriginalCrossSell" + rp.AccessoryPartNo;
                chk.Attributes.Add("cid", rp.AccessoryPartNo);
                plhCrossSell.Controls.Add(chk);

                plhCrossSell.Controls.Add(new LiteralControl("</td></tr>"));
            }
        }

        private void RenderCrossSellAdditions() {
            int i = 1;

            while (i <= CrossSellAdditionCount) {

                plhCrossSellAdditions.Controls.Add(new LiteralControl("<tr><td colspan=\"3\">"));
                TextBox txt = new TextBox();
                txt.ID = "txtCrossSellAddition" + i;
                plhCrossSellAdditions.Controls.Add(txt);

                CustomValidator customValidator = new CustomValidator();
                customValidator.ControlToValidate = txt.ID;
                customValidator.Enabled = true;
                customValidator.EnableViewState = true;
                customValidator.SetFocusOnError = true;
                customValidator.Text = "This is not a valid product";
                customValidator.ErrorMessage = "A cross sell product you added is invalid.";
                customValidator.ServerValidate += RelatedProductValidation;
                plhCrossSellAdditions.Controls.Add(customValidator);

                plhCrossSellAdditions.Controls.Add(new LiteralControl("</td></tr>"));
                i++;
            }
        }

        private void lnkAddCrossSell_Click(object sender, EventArgs e) {
            CrossSellAdditionCount++;
            plhCrossSellAdditions.Controls.Add(new LiteralControl("<tr><td>"));
            TextBox txt = new TextBox();
            txt.ID = "txtCrossSellAddition" + CrossSellAdditionCount;
            plhCrossSellAdditions.Controls.Add(txt);
            plhCrossSellAdditions.Controls.Add(new LiteralControl("</td><td colspan=\"2\">&nbsp;</td></tr>"));
        }

        public List<ProductRelation> GetUpdatedCrossSellProducts() {

            List<ProductRelation> crossSellList = new List<ProductRelation>();
            
            AddCrossSellExisting(crossSellList);
            AddCrossSellAdditions(crossSellList);

            return crossSellList;
        }

        private void AddCrossSellAdditions(List<ProductRelation> crossSellList) {

            int i = 1;

            while (i <= CrossSellAdditionCount) {

                string productCode = "";
                TextBox t = (TextBox)plhCrossSellAdditions.FindControl("txtCrossSellAddition" + i);

                if (t.Text != "") {
                    try {
                        productCode = t.Text;
                        if (productCode != "") {
                            Domain.Product p = controller.CatalogueViewer.GetECommerceProductByItemCode(controller.Section.Node.Site.Id, controller.Section.Node.Culture, productCode);
                            if (p != null) {

                                ProductRelation rp = new ProductRelation();
                                rp.ProductID = p.ProductID;
                                if (Product != null) {
                                    rp.ParentID = Product.ProductID;
                                }
                                rp.RelationType = new RelationType();
                                rp.RelationType.RelationTypeid = (short) RelationType;
                                crossSellList.Add(rp);
                            }

                        }
                    } catch { }
                }

                i++;
            }
        }

        private int AddCrossSellExisting(List<ProductRelation> crossSellList) {
            int i = 1;
            while (i < plhCrossSell.Controls.Count) {

                CheckBox cb = plhCrossSell.Controls[i] as CheckBox;

                if (cb != null) {
                    string cid = cb.Attributes["cid"];
                    bool chk = cb.Checked;
                    if (!chk) {
                        try {
                            ProductRelation rp = new ProductRelation();
                            Domain.Product p = controller.CatalogueViewer.GetECommerceProductByItemCode(controller.Section.Node.Site.Id, controller.Section.Node.Culture, cid);
                            if (p != null) {
                                rp.ProductID = p.ProductID;

                                if (Product != null) {
                                    rp.ParentID = Product.ProductID;
                                }
                                rp.RelationType = new RelationType();
                                rp.RelationType.RelationTypeid = (short)RelationType;
                                crossSellList.Add(rp);
                            }
                        } catch { }
                    }
                }
                i++;
            }
            return i;
        }

        private void RelatedProductValidation(object source, ServerValidateEventArgs args) {
            try {

                string productCode = args.Value;
                Domain.Product p = controller.CatalogueViewer.GetECommerceProductByItemCode(controller.Section.Node.Site.Id, controller.Section.Node.Culture, productCode);
                
                if (p != null) {

                    args.IsValid = true;
                } else {
                    args.IsValid = false;
                }
            } catch {
                args.IsValid = false;
            }
        }
    }
}