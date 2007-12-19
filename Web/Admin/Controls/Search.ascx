<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Controls.Search" %>
<h1>Search</h1>
<p>		
<label>Order #</label><asp:TextBox ID="txtOrderNumber" runat="server" /> OR

<label>Account #</label><asp:TextBox ID="txtAccountNumber" runat="server" /> OR

<label>Product Code</label> <asp:TextBox ID="txtProductCode" runat="server" /> OR

<label>Product Name</label><asp:TextBox ID="txtProductName" runat="server" /> 

<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" />
</p>		
				
   