<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AddressView.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.AddressView" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<h3><%=GetText("Invoice Address")%></h3>
<asp:literal id="litInvoiceAddress" runat="server" />
<h3><%=GetText("Delivery Address")%></h3>
<asp:literal id="litDeliveryAddress" runat="server" />