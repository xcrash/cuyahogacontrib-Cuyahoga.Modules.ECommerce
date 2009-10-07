<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PromotionManager.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.PromotionManager" %>
<%@ Import Namespace="Guild.WebControls" %>
<%@ Register TagPrefix="Guild"  Namespace="Guild.WebControls" Assembly="Cuyahoga.Modules.ECommerce" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
    <head id="Head1" runat="server">
        <title>Product Edit</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <h1>Image Editor</h1>
            <div class="group">
                <Guild:WebImageMaker id="WebImageMaker1" runat="server" 
                    CancelButtonText="Cancel" ConfirmButtonText="OK" UploadButtonText="Go!" 
                    ImageWidth="203" ImageHeight="*"
                    ImageUrl="http://newsimg.bbc.co.uk/media/images/40793000/jpg/_40793742_mex_esa_203.jpg"
                    WorkingDirectory="~/working" Format="gif" Quality="Medium" HandlerPath="~/WebImageMakerHandler.ashx"  />

            </div>
        </form>
    </body>
</html>