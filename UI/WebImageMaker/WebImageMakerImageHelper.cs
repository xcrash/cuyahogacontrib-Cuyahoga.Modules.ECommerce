using System;
using System.Web;
using System.IO;

namespace Guild.WebControls
{
    // any code that could be used by the handler as well as the control should 
    // go in this class
    public class WebImageMakerImageHelper
    {
        string workingDirectory;
        string keySuffix;

        public WebImageMakerImageHelper(string workingDirectory, string keySuffix)
        {
            this.workingDirectory = workingDirectory;
            this.keySuffix = keySuffix;
        }

        public void serveImage()
        {
            String mode = HttpContext.Current.Request.QueryString["mode" + keySuffix]; //   _" + WebImageMaker.WebImageMakerID];
            String imageName = HttpContext.Current.Request.QueryString["img" + keySuffix];

            if (imageName.EndsWith(".gif"))
            {
                HttpContext.Current.Response.ContentType = "image/gif";
            }
            else if (imageName.EndsWith(".jpg"))
            {
                HttpContext.Current.Response.ContentType = "image/jpeg";
            }
            else if (imageName.EndsWith(".png"))
            {
                HttpContext.Current.Response.ContentType = "image/png";
            }

            String filepath = "";
            switch (mode)
            {
                case "canvas":
                    filepath = Path.Combine(workingDirectory, WebImageMaker.CanvasImageDirName);
                    break;
                case "web":
                    filepath = Path.Combine(workingDirectory, WebImageMaker.WebImageDirName);
                    break;
                case "thumbnail":
                    filepath = Path.Combine(workingDirectory, WebImageMaker.ThumbnailImageDirName);
                    break;
            }

            filepath = Path.Combine(filepath, imageName);

            HttpContext.Current.Response.WriteFile(filepath);
            HttpContext.Current.Response.End();
        }
    }
}
