<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderManager.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.OrderManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Order Manager</h1>
   <div class="post">
      <p>From here you can update, add and search all of your online store orders.</p>
     </div>	
     
     			<p><asp:HyperLink ID="hplOrderList" runat="server" Text="List Orders"></asp:HyperLink></p>
				<p><asp:HyperLink ID="hplOrderSearch" runat="server" Text="Search Orders"></asp:HyperLink></p>
				<p><asp:HyperLink ID="hplOrderStats" runat="server" Text="Order Statistics"></asp:HyperLink></p>
    </form>
</body>
</html>
