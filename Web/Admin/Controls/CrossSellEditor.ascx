<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CrossSellEditor.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Controls.CrossSellEditor" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce" %><%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<h3>Related Products</h3>
<table>
    <tr><th>Item Code</th><th>Product Name</th><th>Remove?</th></tr>
    <asp:PlaceHolder ID="plhCrossSell" runat="server" />
    <asp:PlaceHolder ID="plhCrossSellAdditions" runat="server" />
    <tr><td colspan="3"><asp:LinkButton ID="lnkAddCrossSell" runat="server" text="Add related product" /></td></tr>
</table>