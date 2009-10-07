<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin._Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ecommerce Module Administration</title>
</head>

<body>
    <form id="form1" runat="server">
     <h1>ECommerce Manager</h1>
 <div class="post">
<p> From here you can manage every aspect of your online store.</p>
<p>Below are links to and descriptions of the most commonly used features. All  ECommerce features can be accessed by hovering over the ECommerce link in the main menu at any time. </p>
 </div>
                <h3>Catalogue Management</h3>

				<blockquote><p>In this section, you can browse the store catalogue and access all the related functions. These include the ability to add, edit, delete and organize all products, attributes and categories....</p></blockquote>

				
				<p>
				<a href="#"><img src="images/cat.jpg" width="80" height="90"  class="float-left" /></a>
				
			    <asp:HyperLink ID="hplCat" runat="server" Text="Browse Catalogue"></asp:HyperLink><br /><br />
				 <asp:HyperLink ID="hplAttributes" runat="server" Text="Manage Attributes"></asp:HyperLink><br /><br />
				</p>
				<br /><br />
                
				<h3>Order Management</h3>

				<blockquote><p>In this section, you can access all customer order related functions. These include the ability to review, edit, delete and search all orders....</p></blockquote>

				
				<p>
				<a href="#"><img src="images/orders.jpg" width="80" height="90"  class="float-left" /></a>

				<asp:HyperLink ID="hplOrderList" runat="server" Text="List Orders"></asp:HyperLink><br /><br />
				</p>				
  
   </form>
   </body>
</html>