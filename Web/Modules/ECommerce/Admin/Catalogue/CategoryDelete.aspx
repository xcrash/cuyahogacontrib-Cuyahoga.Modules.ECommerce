<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryDelete.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.CategoryDelete" %>
<%@ Register TagPrefix="breadcrumb" TagName="menu" Src="../Controls/BreadCrumb.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Untitled Page</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div id="contentpane">
                <h1>Category Editor</h1>
                <breadcrumb:menu runat="server" id="ctlBreadCrumb" SetLinkOnCurrentNode="false"/>
                <div class="group">
                    <asp:Label ID="lblDeletion" runat="server"/>
                </div>
   	        </div>
        </form>
    </body>
</html>
