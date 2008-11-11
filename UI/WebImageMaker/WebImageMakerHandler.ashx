<%@ WebHandler Language="C#" Class="WebImageMakerHandler" %>

using System;
using System.Web;
using System.Configuration;
using Guild.WebControls;

public class WebImageMakerHandler : IHttpHandler 
{
    
    public void ProcessRequest (HttpContext context) 
    {
        string workingDirectory = ConfigurationManager.AppSettings["WebImageMakerWorkingDirectory"];
        WebImageMakerImageHelper helper = new WebImageMakerImageHelper(workingDirectory, "");
        helper.serveImage();
    }
 
    public bool IsReusable 
    {
        get { return false; }
    }

}