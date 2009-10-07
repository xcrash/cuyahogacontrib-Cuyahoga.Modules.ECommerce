<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AddressView.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.AddressView" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<h3><%=GetText("invoice address")%></h3>
<asp:literal id="litInvoiceAddress" runat="server" />
<h3><%=GetText("delivery address")%></h3>
<asp:literal id="litDeliveryAddress" runat="server" />