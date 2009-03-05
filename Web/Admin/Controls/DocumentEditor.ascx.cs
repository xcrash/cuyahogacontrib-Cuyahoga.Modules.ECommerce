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

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Controls {

    public partial class DocumentEditor : System.Web.UI.UserControl {

        protected LinkButton lnkAddDocument;
        protected Repeater rptDocuments;
        protected Repeater rptNewDocuments;
        protected PlaceHolder plhDocuments;

        public IProduct product;
        public List<Document> documents;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DisplayDocuments();
            }
        }

        public List<Document> GetNewDocuments() {

            if (documents == null || product == null) {
                return new List<Document>();
            }

            List<Document> newDocuments = new List<Document>();
            bool selected = false;

            foreach (Document doc in documents) {

                selected = false;

                foreach (RelatedDocument d in product.DocumentList) {
                    if (d.Name == doc.DocumentName) {
                        selected = true;
                    }
                }

                if (!selected) {
                    newDocuments.Add(doc);
                }
            }

            return newDocuments;
        }

        public void DisplayDocuments() {

            if (product != null && product.DocumentList.Count > 0) {
                    rptDocuments.DataSource = product.DocumentList;
                    rptDocuments.DataBind();
            }
            rptNewDocuments.DataSource = GetNewDocuments();
            rptNewDocuments.DataBind();
        }

        public List<ProductDocument> GetUpdatedDocuments() {

            List<ProductDocument> documentList = new List<ProductDocument>();

            //gets existing documents 
            foreach (RepeaterItem item in rptDocuments.Items) {
                TextBox txtBox = item.FindControl("txtdocumentID") as TextBox;
                CheckBox chkBox = item.FindControl("chkRemove") as CheckBox;
                if (!chkBox.Checked) {
                    ProductDocument pd = new ProductDocument();
                    if (product != null) {
                        pd.ProductID = product.ProductID;
                    }
                    pd.DocumentID = Convert.ToInt64(txtBox.Text);
                    documentList.Add(pd);
                    pd = null;
                }
            }

            //gets new documents
            foreach (RepeaterItem item in rptNewDocuments.Items) {
                TextBox txtBox = item.FindControl("txtdocumentID") as TextBox;
                CheckBox chkBox = item.FindControl("chkAdd") as CheckBox;
                if (chkBox.Checked) {
                    ProductDocument pd = new ProductDocument();
                    if (product != null) {
                        pd.ProductID = product.ProductID;
                    }
                    pd.DocumentID = Convert.ToInt64(txtBox.Text);
                    documentList.Add(pd);
                    pd = null;
                }
            }

            return documentList;
        }
    }
}