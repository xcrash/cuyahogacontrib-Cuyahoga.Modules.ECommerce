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
using System.IO;
namespace Cuyahoga.Modules.ECommerce.Web.Admin {
    public class Download : System.Web.UI.Page {

        public string FileName {

            get {
                return _fileName;
            }

            set {
                _fileName = value;
            }
        }

        private string _fileName;
        public const string PARAM_FILENAME = "file";
        protected void Page_Load(object sender, EventArgs e) {
            try {
                FileName = Request.Params[PARAM_FILENAME];
            } catch {
            }

                  Response.ContentType = "Application/x-csv";
                  Response.AddHeader("content-disposition", "attachment; filename=export.csv");

                  FileStream sourceFile = new FileStream(ConfigurationSettings.AppSettings["CSVExportPath"] + FileName, FileMode.Open);
                  long FileSize;
                  FileSize = sourceFile.Length;
                  byte[] getContent = new byte[(int)FileSize];
                  sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                  sourceFile.Close();

                  Response.BinaryWrite(getContent);
                 
        
      }
        
    }
}
