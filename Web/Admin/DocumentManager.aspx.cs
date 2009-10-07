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
using Cuyahoga.Web.UI;
using Cuyahoga.Core.Util;

using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce;
using Cuyahoga.Modules.ECommerce.Domain;
using System.Collections.Generic;
using Koutny.WebControls;
using Cuyahoga.Modules.ECommerce.Core;
using System.IO;
using Cuyahoga.Modules.ECommerce.DataAccess;
using log4net;

namespace Cuyahoga.Modules.ECommerce.Web.Admin {
    public partial class DocumentManager : ModuleAdminBasePage {

        protected Repeater rptDocuments;
        protected LinkButton lnkSave;
        protected LinkButton lnkUpload;
        protected Label lblSave;
        protected Label lblSaveNew;
        protected FileUpload uploadControl;
        protected CatalogueViewModule controller;
        protected List<Document> documents;
        protected IExtCommonDao _dao;

        protected void Page_Load(object sender, EventArgs e) {

        lnkUpload.Click += new System.EventHandler(Upload_Click);
        lnkSave.Click += new System.EventHandler(Save_Click);
            //Display uploaded files in a repeater 
           
                controller = Module as CatalogueViewModule;
                if (!IsPostBack) {
                    documents = controller.CatalogueViewer.GetDocuments(controller.Section.Node.Site.Id, controller.Section.Node.Culture);
                    rptDocuments.DataSource = documents;
                    rptDocuments.DataBind();
                }
        }

        public void Save_Click(object sender, System.EventArgs e) {
            if (Save()) {
                documents = controller.CatalogueViewer.GetDocuments(controller.Section.Node.Site.Id, controller.Section.Node.Culture);
                rptDocuments.DataSource = documents;
                rptDocuments.DataBind();
                lblSave.Text = " The documents have been updated";
            } else {
                lblSave.Text = " Saving Failed. The selected document(s) are probaly attached to a product. ";
            }
        }

        public void Upload_Click(object sender, System.EventArgs e) {
            if (ValidateUpload()) {
                try {
                    uploadControl.SaveAs(ConfigurationManager.AppSettings["DocumentUploadPath"] + uploadControl.PostedFile.FileName);
                    Document d = new Document();
                    d.DocumentName = uploadControl.PostedFile.FileName;
                    d.FilePath =  uploadControl.PostedFile.FileName;
                    d.TypeID = 1; //FIXME HACK!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    if (controller.EditService.SaveDocument(controller.Section.Node.Site.Id, controller.Section.Node.Culture, d)) {
                        lblSave.Text = "Your file was uploaded";
                        documents = controller.CatalogueViewer.GetDocuments(controller.Section.Node.Site.Id, controller.Section.Node.Culture);
                        rptDocuments.DataSource = documents;
                        rptDocuments.DataBind();
                    } else {
                        lblSaveNew.Text = "Your file was not saved";   
                    }
                } catch (Exception f) {
                    lblSaveNew.Text = "Your file was not saved";
                    LogManager.GetLogger(GetType()).Error(f);
                }

            } else {
                lblSaveNew.Text = "Your file was not saved";
            }
           
        }

        public bool ValidateUpload(){
            return (uploadControl != null && uploadControl.HasFile 
                && !File.Exists(ConfigurationManager.AppSettings["DocumentUploadPath"] + uploadControl.PostedFile.FileName) && ValidFileType());
        }

        public bool ValidFileType() { //should define some kind of enum || define in db/web.config FIXME
            return (Path.GetExtension(uploadControl.PostedFile.FileName) == ".pdf" 
                || Path.GetExtension(uploadControl.PostedFile.FileName) == ".doc" 
                || Path.GetExtension(uploadControl.PostedFile.FileName) == ".docx");
        }

        public bool CheckFileSize() {
            if (uploadControl.PostedFile.ContentLength < Convert.ToInt32(ConfigurationManager.AppSettings["MaximumUploadSize"])) {
                return true;
            }

            return false;
        }

        public bool Save() {
     

            foreach (RepeaterItem item in rptDocuments.Items) {
                TextBox txtBox = item.FindControl("txtDocumentID") as TextBox;
                CheckBox chkBox = item.FindControl("chkDelete") as CheckBox;
                if (chkBox.Checked) {
                   Document d =  controller.CatalogueViewer.GetDocument(controller.Section.Node.Site.Id, controller.Section.Node.Culture, Convert.ToInt64(txtBox.Text));
                    if (!controller.EditService.DeleteDocument(controller.Section.Node.Site.Id, controller.Section.Node.Culture, d)) {
                        
                        return false;
                    }
                }
            }
    
            return true;
        }
    }
}

