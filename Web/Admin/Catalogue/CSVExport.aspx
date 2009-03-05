<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CSVExport.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.CSVExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>CSV Export</h1>
    <div class="post">
      <p>By pressing the download button, a csv file will be sent to the browser which contains you entire product catalogue.</p>
          </div>	
         	<p><asp:LinkButton ID="lnkDownload" Text="Download Catalogue" runat="server"></asp:LinkButton></p> 
    
 
    </form>
</body>
</html>