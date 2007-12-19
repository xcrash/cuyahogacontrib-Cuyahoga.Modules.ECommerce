<%@ Control Language="c#" AutoEventWireup="false" Codebehind="OrderView.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Albion.Web.Views.OrderView" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="ecommerce" TagName="OrderViewComposite" Src="Controls/OrderViewComposite.ascx" %>
<asp:Label ID="lblMessage" Runat="server"><%=GetText("current_order_not_found")%></asp:Label>
<ecommerce:orderviewcomposite runat="server" id="ctlOrderViewComposite" />