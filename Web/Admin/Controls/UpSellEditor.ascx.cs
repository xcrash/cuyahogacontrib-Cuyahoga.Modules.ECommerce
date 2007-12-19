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
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using System.Collections.Generic;
using Cuyahoga.Modules.ECommerce.Core;

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Controls {
    public partial class UpSellEditor : LocalizedModuleConsumerControl {

        public int upSellProducts {

            get {
                if (ViewState["upSellProducts"] != null) {
                    return (int)ViewState["upSellProducts"];
                } return 0;
            }

            set {
                ViewState["upSellProducts"] = value;
            }
        }

        protected LinkButton lnkAddUpSell;
        protected Repeater rptUpSell;
        protected PlaceHolder plhUpSellAdditions;
        public IProduct product;
        int i;
        protected void Page_Load(object sender, EventArgs e) {

            lnkAddUpSell.Click += new EventHandler(lnkAddUpSell_Click);

            i = 1;
            while (i <= upSellProducts) {
                TextBox txt = new TextBox();
                txt.ID = "txtUpSellAddition" + i;
                plhUpSellAdditions.Controls.Add(txt);


                CustomValidator upCustomValidator = new CustomValidator();
                upCustomValidator.ControlToValidate = txt.ID;
                upCustomValidator.Enabled = true;
                upCustomValidator.EnableViewState = true;
                upCustomValidator.SetFocusOnError = true;
                upCustomValidator.Text = "This is not a valid product";
                upCustomValidator.ErrorMessage = "A upsell product you added is invlaid.";
                upCustomValidator.ServerValidate += RelatedProductValidation;

                plhUpSellAdditions.Controls.Add(upCustomValidator);

                LiteralControl l = new LiteralControl();
                l.Text = "<br/><br/>";
                plhUpSellAdditions.Controls.Add(l);
                i++;
            }

            if (product.UpSellList.Count > 0) {
                rptUpSell.DataSource = product.UpSellList;
                rptUpSell.DataBind();
            }
        }

        public void lnkAddUpSell_Click(object sender, EventArgs e) {

            upSellProducts++;
            TextBox txt = new TextBox();
            txt.ID = "txtUpSellAddition" + upSellProducts;
            plhUpSellAdditions.Controls.Add(txt);
            LiteralControl l = new LiteralControl();
            l.Text = "<br/><br/>";
            plhUpSellAdditions.Controls.Add(l);

        }

        public List<ProductRelation> GetUpdatedUpSellProducts() {
            List<ProductRelation> upSellList = new List<ProductRelation>();
            foreach (RepeaterItem item in rptUpSell.Items) {
                TextBox txtBox = item.FindControl("txtPartNumber") as TextBox;
                CheckBox chkBox = item.FindControl("chkRemove") as CheckBox;
                if (!chkBox.Checked) {
                    ProductRelation rp = new ProductRelation();
                    rp.ProductID = rp.ProductID = Convert.ToInt64(txtBox.Text);
                    rp.RelationType = new RelationType();
                    rp.RelationType.RelationTypeid = (short) Util.Enums.RelatedProductType.UpSell;
                    rp.ParentID = product.ProductID;
                    upSellList.Add(rp);
                }

            }

            int i = 1;

            while (i <= upSellProducts) {

                long productCode = 0;
                TextBox t = (TextBox)plhUpSellAdditions.FindControl("txtUpSellAddition" + i);

                if (t.Text != "") {
                    try {
                        productCode = Convert.ToInt64(t.Text);
                        if (productCode != 0) {
                            ProductRelation rp = new ProductRelation();
                            rp.ParentID = product.ProductID;
                            rp.ProductID = productCode;

                            rp.RelationType = new RelationType();
                            rp.RelationType.RelationTypeid = (short) Util.Enums.RelatedProductType.UpSell;

                            upSellList.Add(rp);
                        }
                    } catch {

                    }
                }
                i++;
            }

            return upSellList;
        }

        public void RelatedProductValidation(object source, ServerValidateEventArgs args) { // this is repeated in cross sell ediitot. maybe put in Related products class
            try {

                CatalogueViewModule controller = Module as CatalogueViewModule;
                long productCode = Convert.ToInt64(args.Value);
                IProduct p = controller.CatalogueViewer.GetProduct(controller.Section.Node.Site.Id, controller.Section.Node.Culture, productCode);

                if (p != null) {

                    args.IsValid = true;
                } else {
                    args.IsValid = false;
                }



            } catch (Exception ex) {

                args.IsValid = false;

            }

        }
    }
}