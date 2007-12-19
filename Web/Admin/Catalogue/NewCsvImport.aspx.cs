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
using NExcel;
using Cuyahoga.Core;
using Cuyahoga.Core.Domain;
using Cuyahoga.Core.Service;
using Cuyahoga.Core.Service.Email;
using Cuyahoga.Core.Search;
using System.IO;
using Cuyahoga.Core.Communication;
using Cuyahoga.Web.Util;

using System.Data.SqlClient;
using Guild.WebControls;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce;
using Cuyahoga.Modules.ECommerce.Web.Admin.Controls;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using System.Collections.Generic;
using log4net;

namespace Cuyahoga.Modules.ECommerce.Web.Admin {
    public class CSVImport2 : ModuleAdminBasePage {

        public const string LABEL_DELETED = "Deleted?";
        public const string LABEL_PRODUCT_NAME = "Product Name";
        public const string LABEL_ITEM_CODE = "Item Code";
        public const string LABEL_DESCRIPTION = "Description";
        public const string LABEL_ADDITIONAL_INFORMATION = "Additional information";
        public const string LABEL_CATEGORY_NAME = "Category Name";
        public const string LABEL_SUB_CATEGORY_NAME = "Sub Category Name";
        public const string LABEL_WEEKLY_PRICE = "Weekly Price";
        public const string LABEL_DAILY_PRICE = "Daily Price";
        public const string LABEL_TWO_DAY_PRICE = "Two Day Price";
        public const string LABEL_EYE = "Eye Protection must be worn";
        public const string LABEL_GLOVES = "Wear gloves";
        public const string LABEL_DUST = "Wear dust mask";
        public const string LABEL_HELMET = "Wear saftey helmet";
        public const string LABEL_EAR = "Wear ear protection";
        public const string LABEL_RCD = "Use RCD";
        public const string LABEL_FINGER = "White finger";
        public const string LABEL_FACE = "Wear face shield";
        public const string LABEL_FOOTWEAR = "Wear protective footwear";
        public const string LABEL_OVERALLS = "Wear Overalls";
        public const string LABEL_WELDING = "Wear welding mask";
        public const string LABEL_HARNESS = "Wear safety harness";
        public const string LABEL_REQUEST = "Request for Cash hire Link";
        public const string LABEL_SYNONYMS = "Alternative Words"; //these will  be ; seperated
        public const string LABEL_RELATED = "Related Products";  //these will  be ; seperated
        private const string name = "@name";
        protected FileUpload uploadControl;
        protected LinkButton lnkUpload;
        protected Label lblSave;

        public const int MAX_BLANK_ROWS = 10;
        public const int MAX_ROWS = 65535;
        public List<IProduct> badProducts;
        public CatalogueViewModule controller;
        SqlConnection conn = null;

        protected void Page_Load(object sender, EventArgs e) {
            conn = new SqlConnection("Server=(10.101.10.145);DataBase=MJHire;UserID=Sa;pwd=sqlserver");
            lnkUpload.Click += new System.EventHandler(Upload_Click);
            badProducts = new List<IProduct>();

        }

        public void Upload_Click(object sender, System.EventArgs e) {
            if (ValidateUpload()) {
                try {
                    uploadControl.SaveAs(ConfigurationSettings.AppSettings["CSVUploadPath"] + uploadControl.FileName);
                    string message = PopulateProductList(ConfigurationSettings.AppSettings["CSVUploadPath"] + uploadControl.FileName);
                    lblSave.Text = "Your changes have been made.";
                } catch (Exception f) {
                    LogManager.GetLogger(GetType()).Error(f);
                    lblSave.Text = "Your file was not saved";
                    //logging
                }

            } else {
                lblSave.Text = "Your file was not saved";
            }

        }

        public bool ValidateUpload() {
            if (uploadControl != null) {
                if (uploadControl.HasFile && ValidFileType()) {
                    return true;
                }
            }

            return false;
        }

        public bool ValidFileType() { //should define some kind of enum || define in db/web.config FIXME
            if (Path.GetExtension(uploadControl.PostedFile.FileName) == ".xls") {
                return true;
            }

            return false;
        }



        public string PopulateProductList(string filename) {
            string message = "";
            Workbook book = Workbook.getWorkbook(filename);

            if (book == null) {
                throw new InvalidOperationException("Invalid excel file: " + filename);
            }

            //Use first sheet by default:
            Sheet sheet = book.getSheet(0);

            if (sheet == null) {
                book.close();

                throw new InvalidOperationException("Could not find a default sheet");
            }

            Hashtable labelMap = CreateLabelMap(sheet);

            controller = Module as CatalogueViewModule;


            int productCount = 0;

            //First row contains headings
            for (int i = 1; i < sheet.Rows; i++) {
                if (UpdateProduct(sheet, i)) {

                }

                productCount++;
            }

            book.close();

            if (productCount == 0) {
                throw new InvalidOperationException("Spreadsheet contains no valid products");
            }

            emailErrors();

            return message;
        }

        private string CreateProductListFromUpload(string fileName) {
            string message = "";
            try {

                PopulateProductList(fileName);

            } catch (Exception e) {

                message = "There was an error processing the excel file."; //needs translation
            }

            try {
                //Delete it now we have no use for it
                //  File.Delete(fileName);
            } catch (Exception e) {

            }

            return message;
        }

        private Hashtable CreateLabelMap(Sheet sheet) {
            Hashtable labelMap = new Hashtable();
            for (int i = 0; i < sheet.Columns; i++) {
                try {
                    labelMap.Add(GetLabelText(sheet.getCell(i, 0).Value), i);
                } catch { }
            }
            return labelMap;
        }

        private void GenerateBadProduct(string name, string ItemCode) {
            IProduct p = new Domain.Catalogue.Product();
            p.Name = name;
            p.ItemCode = ItemCode;
            badProducts.Add(p);
        }
        private bool UpdateProduct(Sheet sheet, int rowNumber) {


            //See how many requested cells are null
            int cellCount = 0;
            int nullCellCount = 0;

            //Get case-insensitive map of labels to columns
            Hashtable labelMap = CreateLabelMap(sheet);
            Domain.Product p = new Cuyahoga.Modules.ECommerce.Domain.Product();

            int count = 0;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("selec count(*) as count from product where productName = @name" , conn);
            da.SelectCommand.Parameters.Add(@name, GetCellValue(sheet, rowNumber, labelMap, LABEL_PRODUCT_NAME, ref cellCount, ref nullCellCount));
            DataSet ds = new DataSet();
            da.Fill(ds, "product");
            DataTable dataTable = ds.Tables[0];
            foreach (DataRow dataRow in dataTable.Rows) {
                count = (int)dataRow["count"];
            }

            if (count < 1) {

                p.ProductName = GetCellValue(sheet, rowNumber, labelMap, LABEL_PRODUCT_NAME, ref cellCount, ref nullCellCount);
                p.ItemCode = GetCellValue(sheet, rowNumber, labelMap, LABEL_ITEM_CODE, ref cellCount, ref nullCellCount);
                p.ProductDescription = GetCellValue(sheet, rowNumber, labelMap, LABEL_DESCRIPTION, ref cellCount, ref nullCellCount);
                p.AdditionalInformation = GetCellValue(sheet, rowNumber, labelMap, LABEL_ADDITIONAL_INFORMATION, ref cellCount, ref nullCellCount);
                if (GetCellValue(sheet, rowNumber, labelMap, LABEL_WEEKLY_PRICE, ref cellCount, ref nullCellCount) != " ") {
                    try {
                        p.BasePrice = decimal.Parse(GetCellValue(sheet, rowNumber, labelMap, LABEL_WEEKLY_PRICE, ref cellCount, ref nullCellCount));
                    } catch (Exception ex) {

                    }
                } else {
                    p.BasePrice = 0;
                }
                p.IsPublished = true;

                if (controller.EditService.SaveProduct(controller.Section.Node.Site.Id, p)) {

                    ProductCategory pc = new ProductCategory();
                    pc.ProductID = p.ProductID;
                    pc.CategoryID = controller.CatalogueViewer.GetCategory(controller.Section.Node.Site.Id, controller.Section.Node.Culture, GetCellValue(sheet, rowNumber, labelMap, LABEL_SUB_CATEGORY_NAME, ref cellCount, ref nullCellCount)).CategoryID;
                    pc.SortOrder = Convert.ToInt16(controller.EditService.GetHighestProductOrder(pc.CategoryID) + 1);
                    if (!controller.EditService.SaveProductCategory(controller.Section.Node.Site.Id, controller.Section.Node.Culture, pc)) {
                        GenerateBadProduct(p.ProductName, p.ItemCode);
                        return false;
                    }

                    List<ProductRelation> crossSellList = GenerateCrossSellProducts(GetCellValue(sheet, rowNumber, labelMap, LABEL_RELATED, ref cellCount, ref nullCellCount), p.ProductID);
                    List<ProductSynonym> synonymList = GenerateSynonyms(GetCellValue(sheet, rowNumber, labelMap, LABEL_SYNONYMS, ref cellCount, ref nullCellCount), p.ProductID);

                    string eye = GetCellValue(sheet, rowNumber, labelMap, LABEL_EYE, ref cellCount, ref nullCellCount);
                    string gloves = GetCellValue(sheet, rowNumber, labelMap, LABEL_GLOVES, ref cellCount, ref nullCellCount);
                    string mask = GetCellValue(sheet, rowNumber, labelMap, LABEL_DUST, ref cellCount, ref nullCellCount);
                    string helmet = GetCellValue(sheet, rowNumber, labelMap, LABEL_HELMET, ref cellCount, ref nullCellCount);
                    string ear = GetCellValue(sheet, rowNumber, labelMap, LABEL_EAR, ref cellCount, ref nullCellCount);
                    string rcd = GetCellValue(sheet, rowNumber, labelMap, LABEL_RCD, ref cellCount, ref nullCellCount);
                    string finger = GetCellValue(sheet, rowNumber, labelMap, LABEL_FINGER, ref cellCount, ref nullCellCount);
                    string face = GetCellValue(sheet, rowNumber, labelMap, LABEL_FACE, ref cellCount, ref nullCellCount);
                    string footwear = GetCellValue(sheet, rowNumber, labelMap, LABEL_FOOTWEAR, ref cellCount, ref nullCellCount);
                    string overalls = GetCellValue(sheet, rowNumber, labelMap, LABEL_OVERALLS, ref cellCount, ref nullCellCount);
                    string welding = GetCellValue(sheet, rowNumber, labelMap, LABEL_WELDING, ref cellCount, ref nullCellCount);
                    string harness = GetCellValue(sheet, rowNumber, labelMap, LABEL_HARNESS, ref cellCount, ref nullCellCount);

                    string daily = "";
                    string twoDay = "";
                    if (p.BasePrice > 0 && IsToolCategory(pc.CategoryID)) {

                        daily = Convert.ToString((p.BasePrice / 100) * 75);
                        twoDay = Convert.ToString((p.BasePrice / 100) * 50);
                    }
                    string request = GetCellValue(sheet, rowNumber, labelMap, LABEL_REQUEST, ref cellCount, ref nullCellCount);

                    List<ProductAttributeOptionValue> attributeList = GenerateAttributes(eye, gloves, mask, helmet, ear, rcd, finger, face, footwear, overalls, welding, harness, daily, twoDay, request, p.ProductID);

                    foreach (ProductSynonym ps in synonymList) {
                        if (!controller.EditService.SaveProductSynonym(controller.Section.Node.Site.Id, controller.Section.Node.Culture, ps)) {
                            GenerateBadProduct(p.ProductName, p.ItemCode);
                            return false;
                        }
                    }

                    foreach (ProductRelation pr in crossSellList) {
                        if (!controller.EditService.SaveProductRelation(controller.Section.Node.Site.Id, pr)) {
                            GenerateBadProduct(p.ProductName, p.ItemCode);
                            return false;
                        }
                    }

                    foreach (ProductAttributeOptionValue paov in attributeList) {
                        if (!controller.EditService.SaveProductAttribute(controller.Section.Node.Site.Id, paov)) {
                            GenerateBadProduct(p.ProductName, p.ItemCode);
                            return false;
                        }
                    }


                } else {
                    GenerateBadProduct(p.ProductName, p.ItemCode);
                    return false;
                }



            } else {
                //updated product -- pain all round
                if (GetCellValue(sheet, rowNumber, labelMap, LABEL_DELETED, ref cellCount, ref nullCellCount) == "False") {
                    p.IsPublished = true;
                } else {
                    p.IsPublished = false;

                }
                    p.ProductName = GetCellValue(sheet, rowNumber, labelMap, LABEL_PRODUCT_NAME, ref cellCount, ref nullCellCount);
                    p.ItemCode = GetCellValue(sheet, rowNumber, labelMap, LABEL_ITEM_CODE, ref cellCount, ref nullCellCount);
                    p.ProductDescription = GetCellValue(sheet, rowNumber, labelMap, LABEL_DESCRIPTION, ref cellCount, ref nullCellCount);
                    p.AdditionalInformation = GetCellValue(sheet, rowNumber, labelMap, LABEL_ADDITIONAL_INFORMATION, ref cellCount, ref nullCellCount);
                    if (GetCellValue(sheet, rowNumber, labelMap, LABEL_WEEKLY_PRICE, ref cellCount, ref nullCellCount) != " ") {
                        try {
                            p.BasePrice = decimal.Parse(GetCellValue(sheet, rowNumber, labelMap, LABEL_WEEKLY_PRICE, ref cellCount, ref nullCellCount));
                        } catch (Exception ex) {

                        }
                    } else {
                        p.BasePrice = 0;
                    }


                    ProductCategory pc = new ProductCategory();

                        pc.ProductID = p.ProductID;
                        
                        try {
                            pc.CategoryID = controller.CatalogueViewer.GetCategory(controller.Section.Node.Site.Id, controller.Section.Node.Culture, GetCellValue(sheet, rowNumber, labelMap, LABEL_SUB_CATEGORY_NAME, ref cellCount, ref nullCellCount)).CategoryID;
                        } catch (Exception ex) {
                            GenerateBadProduct(p.ProductName, p.ItemCode);
                            return false;
                        }
                    

                    p.Categories.Add(pc);

                        List<ProductRelation> crossSellList = GenerateCrossSellProducts(GetCellValue(sheet, rowNumber, labelMap, LABEL_RELATED, ref cellCount, ref nullCellCount), p.ProductID);
                        List<ProductSynonym> synonymList = GenerateSynonyms(GetCellValue(sheet, rowNumber, labelMap, LABEL_SYNONYMS, ref cellCount, ref nullCellCount), p.ProductID);

                        string eye = GetCellValue(sheet, rowNumber, labelMap, LABEL_EYE, ref cellCount, ref nullCellCount);
                        string gloves = GetCellValue(sheet, rowNumber, labelMap, LABEL_GLOVES, ref cellCount, ref nullCellCount);
                        string mask = GetCellValue(sheet, rowNumber, labelMap, LABEL_DUST, ref cellCount, ref nullCellCount);
                        string helmet = GetCellValue(sheet, rowNumber, labelMap, LABEL_HELMET, ref cellCount, ref nullCellCount);
                        string ear = GetCellValue(sheet, rowNumber, labelMap, LABEL_EAR, ref cellCount, ref nullCellCount);
                        string rcd = GetCellValue(sheet, rowNumber, labelMap, LABEL_RCD, ref cellCount, ref nullCellCount);
                        string finger = GetCellValue(sheet, rowNumber, labelMap, LABEL_FINGER, ref cellCount, ref nullCellCount);
                        string face = GetCellValue(sheet, rowNumber, labelMap, LABEL_FACE, ref cellCount, ref nullCellCount);
                        string footwear = GetCellValue(sheet, rowNumber, labelMap, LABEL_FOOTWEAR, ref cellCount, ref nullCellCount);
                        string overalls = GetCellValue(sheet, rowNumber, labelMap, LABEL_OVERALLS, ref cellCount, ref nullCellCount);
                        string welding = GetCellValue(sheet, rowNumber, labelMap, LABEL_WELDING, ref cellCount, ref nullCellCount);
                        string harness = GetCellValue(sheet, rowNumber, labelMap, LABEL_HARNESS, ref cellCount, ref nullCellCount);
                        /* calculated for them now */

                        string daily = "";
                        string twoDay = "";

                        if (p.BasePrice > 0 && IsToolCategory(pc.CategoryID)) {

                            daily = Convert.ToString((p.BasePrice / 100) * 75);
                            twoDay = Convert.ToString((p.BasePrice / 100) * 50);
                        }

                        string request = GetCellValue(sheet, rowNumber, labelMap, LABEL_REQUEST, ref cellCount, ref nullCellCount);

                        List<ProductAttributeOptionValue> attributeList = GenerateAttributes(eye, gloves, mask, helmet, ear, rcd, finger, face, footwear, overalls, welding, harness, daily, twoDay, request, p.ProductID);
                       
                        foreach (ProductAttributeOptionValue paov in attributeList){
                           p.Attributes.Add(paov);
                        }

                        foreach (ProductSynonym ps in synonymList) {
                          p.Synonyms.Add(ps);
                        }

                        foreach (ProductRelation pr in crossSellList) {
                            p.RelatedProducts.Add(pr);
                        }

                        if (!UpdateProduct(p)) {
                            GenerateBadProduct(p.ProductName, p.ItemCode);
                            return false;
                        }
            }

            return true;
        }

        public bool IsToolCategory(long nodeID) {
            Category c;
            c = controller.CatalogueViewer.GetCategory(controller.Section.Node.Site.Id, controller.Section.Node.Culture, nodeID);
            if (c != null) {
                if (c.ParentCategory.CategoryName == "Tool Hire") {
                    return true;
                }
            }
            return false;
        }

        public List<ProductSynonym> GenerateSynonyms(string cell, long productID) {

            List<ProductSynonym> synonymList = new List<ProductSynonym>();
            char[] splitter = { ';' };
            if (cell != "" || cell != " ") {
                string[] synonyms = cell.Split(splitter);
                for (int i = 0; i < synonyms.Length; i++) {
                    ProductSynonym ps = new ProductSynonym();
                    ps.ProductID = productID;
                    ps.AlternativePhrase = synonyms[i];
                    synonymList.Add(ps);
                }
            }
            return synonymList;
        }

        public List<ProductRelation> GenerateCrossSellProducts(string cell, long productID) {
            List<ProductRelation> relationList = new List<ProductRelation>();
            char[] splitter = { ';' };
            if (cell != "" && cell != " ") {
                string[] relations = cell.Split(splitter);
                for (int i = 0; i < relations.Length; i++) {
                    if (ValidProduct(relations[i])) {
                        ProductRelation pr = new ProductRelation();
                        pr.ParentID = productID;
                        Domain.Product p = controller.CatalogueViewer.GetECommerceProductByItemCode(controller.Section.Node.Site.Id, controller.Section.Node.Culture, relations[i]);
                        if (p != null) {
                            pr.ProductID = p.ProductID;
                            pr.RelationType = new RelationType();
                            pr.RelationType.RelationTypeid = 1; //FIXME
                            relationList.Add(pr);
                        }
                    }
                }
            }
            return relationList;
        }

        public bool ValidProduct(string productID) {
            Domain.Product p = controller.CatalogueViewer.GetECommerceProductByItemCode(controller.Section.Node.Site.Id, controller.Section.Node.Culture, productID);

            if (p != null) {
                return true;
            }
            return false;
        }

        /* HACK -- need to find a generic way to do this */
        public List<ProductAttributeOptionValue> GenerateAttributes(string eye, string gloves, string mask, string helmet, string ear, string rcd, string finger, string face, string footwear, string overalls, string welding, string harness, string daily, string twoDay, string request, long productID) {
            List<ProductAttributeOptionValue> attributeList = new List<ProductAttributeOptionValue>();

            if (eye == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Eye Protection must be worn");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);
            }

            if (gloves == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Wear gloves");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);
            }

            if (mask == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Wear dust mask");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);
            }

            if (helmet == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Wear saftey helmet");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);
            }

            if (ear == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Wear ear protection");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);
            }

            if (rcd == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Use RCD");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);
            }

            if (finger == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "White finger");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);
            }

            if (face == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Wear face shield");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);
            }

            if (footwear == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Wear protective footwear");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);
            }

            if (overalls == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Wear Overalls");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);
            }

            if (welding == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Wear welding mask");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);
            }

            if (harness == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Wear safety harness");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);
            }


            if (daily != "") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "One Day");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                try {
                    decimal price = decimal.Parse(daily);
                    paov.OptionPrice = price;
                    attributeList.Add(paov);
                } catch (Exception ex) {

                }
            }

            if (twoDay != "") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Two Day");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                try {
                    decimal price = decimal.Parse(twoDay);
                    paov.OptionPrice = price;
                    attributeList.Add(paov);
                } catch (Exception ex) {

                }
            }

            if (request == "True") {
                AttributeOptionValue aov = controller.CatalogueViewer.GetAttributeOptionValue(controller.Section.Node.Site.Id, controller.Section.Node.Culture, "Request for Cash hire");
                ProductAttributeOptionValue paov = new ProductAttributeOptionValue();
                paov.ProductID = productID;
                paov.OptionValueid = aov.OptionID;
                paov.OptionValueCode = aov.OptionID.ToString();
                attributeList.Add(paov);

            }

            return attributeList;
        }

        private void emailErrors() {

            string body;
            body = "The Following entries contained errors:\n";
            if (badProducts != null) {
                foreach (IProduct p in badProducts) {
                    body = body + "ItemCode: " + p.ItemCode + " Product Name: " + p.Name + "\n";
                }
            }
            try {
                Email.Send("lee@igentics.com", "lee@igentics.com", "Importing Products Failed", body);
                Email.Send("phil@igentics.com", "lee@igentics.com", "Importing Products Failed", body);
                Email.Send("trish@mjhire.co.uk", "lee@igentics.com", "Importing Products Failed", body);
                Email.Send(" peter@mjhire.co.uk", "lee@igentics.com", "Importing Products Failed", body);
            } catch (Exception ex) {

            }
        }

        private bool IsNull(string val) {
            return (val == null || val == string.Empty || val.Length == 0);
        }

        private string GetCellValue(Sheet sheet, int rowNumber, Hashtable labelMap, string labelText, ref int cellCount, ref int nullCellCount) {

            string stringVal = "";

            try {
                object val = sheet.getCell((int)labelMap[GetLabelText(labelText)], rowNumber).Value;
                if (val != null) {
                    stringVal = val.ToString();
                }
            } catch { }

            //Update our statistics
            cellCount++;
            if (IsNull(stringVal)) nullCellCount++;

            return stringVal;
        }

        private string GetLabelText(object label) {
            return label.ToString().Trim().ToLower();
        }

        private bool UpdateProduct(Domain.Product p) {
            //hopefully this is a lot faster than nhibernate -- need to get a count of the product instead of getting of pulling everything back
            
            conn.Open();
            SqlCommand cmd = new SqlCommand("Ecommerce_ProductUpdate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@productID", p.ProductID));
            cmd.Parameters.Add(new SqlParameter("@itemCode", p.ItemCode));
            cmd.Parameters.Add(new SqlParameter("@productName", p.ProductName));
            cmd.Parameters.Add(new SqlParameter("@productDescription", p.ProductDescription));
            cmd.Parameters.Add(new SqlParameter("@stockLevel", p.StockLevel));
            cmd.Parameters.Add(new SqlParameter("@isPublished", p.IsPublished));
            cmd.Parameters.Add(new SqlParameter("@basePrice", p.BasePrice));
            cmd.Parameters.Add(new SqlParameter("@AdditionalInformation", p.AdditionalInformation));

            try {
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                conn.Close();
                return false;
            }

            cmd = null;

            cmd = new SqlCommand("Ecommerce_ProductSynonymDelete", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@productID", p.ProductID));

            try {
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                conn.Close();
                return false;
            }

            cmd = null;

            foreach(ProductSynonym ps in p.Synonyms){
                cmd = new SqlCommand("Ecommerce_ProductSynonymInsert", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@productID", p.ProductID));
                cmd.Parameters.Add(new SqlParameter("@AlternativePhrase", ps.AlternativePhrase ));
                cmd.Parameters.Add(new SqlParameter("@inserttimestamp", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@updatetimestamp", DateTime.Now));
             
                try {
                    cmd.ExecuteNonQuery();
                } catch (Exception ex) {
                    conn.Close();
                    return false;
                }

                cmd = null;
            }

            cmd = new SqlCommand("Ecommerce_ProductRelationDelete", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@parentID", p.ProductID));

            try {
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                conn.Close();
                return false;
            }

            cmd = new SqlCommand("Ecommerce_ProductRelationInsert", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductID", p.ProductID));
            cmd.Parameters.Add(new SqlParameter("@parentID", p.ProductID));
            cmd.Parameters.Add(new SqlParameter("@relationTypeID", 1));
            cmd.Parameters.Add(new SqlParameter("@inserttimestamp", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@updatetimestamp", DateTime.Now));

            try {
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                conn.Close();
                return false;
            }

            cmd = null;

            cmd = new SqlCommand("Ecommerce_ProductCategoryDelete", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductID", p.ProductID));

            try {
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                conn.Close();
                return false;
            }

            cmd = null;
            //it should get this far if it does not have  a category - we can safely cast it
            ProductCategory pc = (ProductCategory)p.Categories[0];
            cmd = new SqlCommand("Ecommerce_ProductCategoryInsert", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@categoryID", pc.CategoryID));
            cmd.Parameters.Add(new SqlParameter("@productID", p.ProductID));
            cmd.Parameters.Add(new SqlParameter("@sortOrder", pc.SortOrder));
            cmd.Parameters.Add(new SqlParameter("@inserttimestamp", pc.Inserttimestamp));
            cmd.Parameters.Add(new SqlParameter("@updatetimestamp", pc.Updatetimestamp));

            try {
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                conn.Close();
                return false;
            }

            cmd = null;

            cmd = new SqlCommand("Ecommerce_ProductAttributeOptionValueDelete", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@productID", p.ProductID));

            try {
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                conn.Close();
                return false;
            }

            cmd = null;

            foreach (ProductAttributeOptionValue paov in p.RelatedProducts) {
                cmd = new SqlCommand("Ecommerce_ProductAttributeOptionValueInsert", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@productID", p.ProductID));
                cmd.Parameters.Add(new SqlParameter("@optionValueID", paov.OptionValueid));
                cmd.Parameters.Add(new SqlParameter("@optionPrice", paov.OptionPrice));
                cmd.Parameters.Add(new SqlParameter("@optionValueCode", paov.OptionValueCode));
                cmd.Parameters.Add(new SqlParameter("@sortOrder", paov.SortOrder));
                cmd.Parameters.Add(new SqlParameter("@inserttimestamp", paov.Inserttimestamp));
                cmd.Parameters.Add(new SqlParameter("@updatetimestamp", paov.Updatetimestamp));

                try {
                    cmd.ExecuteNonQuery();
                } catch (Exception ex) {
                    conn.Close();
                    return false;
                }

                cmd = null;
            }

            conn.Close();
            return true;
        }

        private bool InsertProduct() {
            return true;
        }
    }
}
