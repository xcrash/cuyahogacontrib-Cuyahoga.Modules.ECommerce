<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CSVImport.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.CSVImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>CSV Import</h1>
    <div class="post">
      <p>By uploading a csv file containing a modified version of your exported product catalogue, it will apply your changes to your product catalogue.</p>
          </div>	

         	<p> <asp:FileUpload ID="uploadControl" runat="server" />
         	 <asp:LinkButton ID="lnkUpload" Text="Upload" runat="server"></asp:LinkButton></p>
    		<p><asp:Label Id="lblSave" runat="server"/></p>


    </form>
</body>
</html>
