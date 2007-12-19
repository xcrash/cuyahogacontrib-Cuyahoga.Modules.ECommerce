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
using Cuyahoga.Core.Search;
using System.IO;
using Cuyahoga.Core.Communication;

using Guild.WebControls;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce;
using Cuyahoga.Modules.ECommerce.Web.Admin.Controls;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using System.Collections.Generic;

namespace Cuyahoga.Modules.ECommerce.Web.Admin {
    
    public class CSVExport : ModuleAdminBasePage {

        /*This will take a LONG while to execute if they have loads of products */
        public CatalogueViewModule controller;
        string csvText;
        protected LinkButton lnkDownload;
        public string fileName;

        protected void Page_Load(object sender, EventArgs e) {
            controller = Module as CatalogueViewModule;
            lnkDownload.Click += new System.EventHandler(Download_Click);
        }

        public void AddToCSV(Domain.Product p) {
            csvText = csvText + GenerateRow(p);
        }
        public string GenerateRow(Domain.Product p) {
            string row;
            if (p.IsPublished) {
                row = "False" + "|"; // for deleted
            } else {
                row = "TRUE" + "|";
            }
            
            row = row + p.ProductName + "|";
            row = row + p.ItemCode + "|";

            if (p.ProductDescription != null) {
                row = row + p.ProductDescription.Replace("\n", "").Replace("\r", "") + "|";
            } else {
                row = row + " | ";
            }
            if (p.AdditionalInformation != null) {
                row = row + p.AdditionalInformation.Replace("\n", "").Replace("\r", "") + "|";
            } else {
                row = row + " | ";
            }

            string category = "";
            string parentCategory = "";
            foreach (ProductCategory pc in p.Categories) {
                if (pc.CategoryID != 0) {
                    Category c = controller.CatalogueViewer.GetCategory(controller.Section.Node.Site.Id, controller.Section.Node.Culture, pc.CategoryID);
                    parentCategory = c.ParentCategory.CategoryName;
                    category = c.CategoryName;
                }
            }
            string eye = "";
            string gloves = "";
            string mask = "";
            string helmet = "";
            string ear = "";
            string rcd = "";
            string finger = "";
            string face = "";
            string footwear = "";
            string overalls = "";
            string welding = "";
            string harness = "";

            string request = "";

            row = row + category + "|";
            row = row + parentCategory + "|";
            row = row + p.BasePrice + "|";

            foreach (ProductAttributeOptionValue paov in p.Attributes) {

                if (paov.OptionDetails.OptionName == "Eye Protection must be worn") {
                    eye = "True";
                }

                if (paov.OptionDetails.OptionName == "Wear gloves") {
                    gloves = "True";
                }

                if (paov.OptionDetails.OptionName == "Wear dust mask") {
                    mask = "True";
                }
                if (paov.OptionDetails.OptionName == "Wear saftey helmet") {
                    helmet = "True";
                }

                if (paov.OptionDetails.OptionName == "Wear ear protection") {
                    ear = "True";
                }

                if (paov.OptionDetails.OptionName == "Use RCD") {
                    rcd = "True";
                }

                if (paov.OptionDetails.OptionName == "White finger") {
                    finger = "True";
                }

                if (paov.OptionDetails.OptionName == "Wear face shield") {
                    face = "True";
                }

                if (paov.OptionDetails.OptionName == "Wear safety harness") {
                    harness = "True";
                }

                if (paov.OptionDetails.OptionName == "Request for Cash hire") {
                    request = "True";
                }

                if (paov.OptionDetails.OptionName == "Wear protective footwear") {
                    footwear = "True";
                }

                if (paov.OptionDetails.OptionName == "Wear Overalls") {
                    overalls = "True";
                }

                if (paov.OptionDetails.OptionName == "Wear welding mask") {
                    welding = "True";
                }

            }

            row = row + eye + "|";
            row = row + gloves + "|";
            row = row + mask + "|";
            row = row + helmet + "|";
            row = row + ear + "|";
            row = row + rcd + "|";
            row = row + finger + "|";
            row = row + face + "|";
            row = row + footwear + "|";
            row = row + overalls + "|";
            row = row + welding + "|";
            row = row + harness + "|";

            string alternativeWords = "";
            foreach (ProductSynonym ps in p.Synonyms) {
                alternativeWords = alternativeWords + ps.AlternativePhrase + ";";
            }
            row = row + alternativeWords + "|";

            string relatedProducts = "";
            foreach (ProductRelation pr in p.RelatedProducts) {
                relatedProducts = relatedProducts + pr.Product.ItemCode + ";";
            }

            row = row + relatedProducts + "|";
            row = row + request + "\n";
            return row;
        }

        public void SendToBrowser() {
            Response.Redirect("Download.aspx?file=" + fileName);
        }

        public bool CreateFile() {
            fileName = "CSVEXPORT-" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond + ".csv";

            try {
                File.WriteAllText(ConfigurationSettings.AppSettings["CSVExportPath"] + fileName, csvText);
            } catch (Exception ex) {
                return false;
            }

            return true;
        }

        public void Download_Click(object sender, System.EventArgs e) {
            csvText = "Deleted?|Product Name|Item Code|Description|Additional information|Sub Category Name|";
            csvText = csvText + "Category Name|Weekly Price|Eye Protection must be worn|";
            csvText = csvText + "Wear gloves|Wear dust mask|Wear saftey helmet|Wear ear protection|Use RCD|White finger|";
            csvText = csvText + "Wear face shield|Wear protective footwear|Wear Overalls|Wear welding mask|Wear safety harness|";
            csvText = csvText + "Alternative Words|Related Products|Request for Cash hire Link" + "\n";

            List<Domain.Product> productList;
            productList = controller.CatalogueViewer.GetAllProducts(controller.Section.Node.Site.Id, controller.Section.Node.Culture);
            foreach (Domain.Product p in productList) {
                // if (p.IsPublished) {
                AddToCSV(p);
                // }
            }
            if (CreateFile()) {
                SendToBrowser();
            }

        }
    }
}