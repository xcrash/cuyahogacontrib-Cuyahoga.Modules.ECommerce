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
    public partial class SynonymEditor : LocalizedModuleConsumerControl {

        public int synonyms {

            get {
                if (ViewState["synonym"] != null) {
                    return (int)ViewState["synonym"];
                } return 0;
            }

            set {
                ViewState["synonym"] = value;
            }
        }

        protected Repeater rptSynonym;
        protected LinkButton lnkAddSynonym;
        protected PlaceHolder plhSynonyms;
        protected PlaceHolder plhSynonymAdditions;
        public IProduct product;

        protected void Page_Load(object sender, EventArgs e) {

            lnkAddSynonym.Click += new EventHandler(lnkAddSynonym_Click);

            DisplaySynonyms();
        }

        public void AddSynonym(int i) {
            TextBox txt = new TextBox();
            txt.ID = "txtAlternativePhrase" + i;
            txt.EnableViewState = true;
            plhSynonymAdditions.Controls.Add(txt);
            txt = null;
            LiteralControl l = new LiteralControl();
            l.Text = "<br/><br/>";
            plhSynonymAdditions.Controls.Add(l);
        }

        public void lnkAddSynonym_Click(object sender, EventArgs e) {


            TextBox txt = new TextBox();
            txt.ID = "txtAlternativePhrase" + synonyms;
            txt.EnableViewState = true;
            plhSynonymAdditions.Controls.Add(txt);
            txt = null;
            LiteralControl l = new LiteralControl();
            l.Text = "<br/><br/>";
            plhSynonymAdditions.Controls.Add(l); synonyms++;

        }

        public void DisplaySynonyms() {
            if (!IsPostBack && product != null) {
                if (product.SynonymList.Count > 0) {
                    rptSynonym.DataSource = product.SynonymList;
                    rptSynonym.DataBind();
                }
            }

            if (synonyms > 0) {
                int i = 0;
                while (i < synonyms) {
                    AddSynonym(i);
                    i++;
                }
            }
        }

        public List<ProductSynonym> GetUpdatedSynonyms() {
            List<ProductSynonym> synonymList = new List<ProductSynonym>();
            int i = 0;
            if (product != null) {
               
                if (product.SynonymList.Count > 0) {

                    foreach (RepeaterItem item in rptSynonym.Items) {
                        CheckBox chkBox = item.FindControl("chkRemove") as CheckBox;
                        TextBox txtAlternativePhrase = (TextBox)item.FindControl("txtAlternativePhrase");
                        if (!chkBox.Checked) {

                            ProductSynonym ps = new ProductSynonym();
                            ps.ProductID = product.ProductID;
                            ps.AlternativePhrase = txtAlternativePhrase.Text;
                            synonymList.Add(ps);
                            ps = null;
                        }

                    }
                }
            }

            i = 0;
            if (synonyms > 0) {
                while (i < synonyms) {
                    ProductSynonym synonym = new ProductSynonym();
                    TextBox txtAlternativePhrase = (TextBox)plhSynonymAdditions.FindControl("txtAlternativePhrase" + i);
                    if (txtAlternativePhrase.Text != "") {
                        synonym.AlternativePhrase = txtAlternativePhrase.Text;
                        if (product != null) {
                            synonym.ProductID = product.ProductID;
                        }
                        synonymList.Add(synonym);
                        synonym = null;
                    }
                    i++;

                }
            }
            return synonymList;
        }
    }
}