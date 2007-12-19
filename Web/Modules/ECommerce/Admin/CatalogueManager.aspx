<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatalogueManager.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.CatalogueManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Catalogue Manager</h1>
        <div class="post">
            <p>From here you add browse and edit the store catalogue, as well as importing and exporting it to and from a csv file.</p>
        </div>        
        <p><asp:HyperLink ID="hplBrowse" runat="server" Text="Browse and edit catalogue"></asp:HyperLink></p>
    </form>
</body>
</html>
