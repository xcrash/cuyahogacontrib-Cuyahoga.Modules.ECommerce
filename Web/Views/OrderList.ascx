<%@ Control Language="c#" AutoEventWireup="false" Codebehind="OrderList.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Views.OrderList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="ecommerce" TagName="Address" Src="Controls/AddressEdit.ascx" %>
<%@ Register TagPrefix="ecommerce" TagName="User" Src="Controls/UserDetailsEdit.ascx" %>
<%@ Register TagPrefix="ecommerce" TagName="OrderViewComposite" Src="Controls/OrderViewComposite.ascx" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util.Interfaces" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util" %>
<asp:placeholder id="phOrderList" runat="server">

<table>
  <tbody>
	<tr><td colspan="2"><%=GetText("search by date")%></td></tr>
    <tr>
      <td><%=GetText("from date")%></td>
      <td><asp:dropdownlist runat="server" id="ddlFromDate" /></td>
    </tr>
    <tr>
      <td><%=GetText("to date")%></td>
      <td><asp:dropdownlist runat="server" id="ddlToDate" /></td>
    </tr>
	<tr><td colspan="2"><asp:Button ID="btnSearch" Runat="server"/></td></tr>
  </tbody>
</table>

 <table>
  <tbody>
	<tr>
	<td><%=GetText("ordered date")%></td>
	<td><%=GetText("order number")%></td>
	<td><%=GetText("total")%></td>
	</tr>
	<asp:Repeater ID="rptOrderList" Runat="server">
	<ItemTemplate>
	<tr<%#(Container.ItemIndex % 2 == 1) ? " class=\"even\"" : ""%>>
		<td><%# HtmlFormatUtils.FormatShortDate(((BasketDecorator)Container.DataItem).OrderHeader.OrderedDate, StateInfo.StoreCulture)%></td>
		<td><a href="<%# GetOrderUrl(Container)%>"><%# ((BasketDecorator)Container.DataItem).OrderHeader.OrderHeaderID %></a></td>
		<td align="right"><%# HtmlFormatUtils.FormatMoney(((BasketDecorator)Container.DataItem).GrandTotal) %></td>
	</tr>
	</ItemTemplate>
</asp:Repeater>
<%= (rptOrderList.Items.Count == 0) ? ("<tr><td colspan='2'>" + GetText("no_order_items_found") + "</td></tr>") : "" %>
  </tbody>
  </table>
</asp:placeholder>
<asp:placeholder id="phOrderView" runat="server">
  <ecommerce:orderviewcomposite runat="server" id="ctlOrderViewComposite" />
  <br /><asp:button id="btnBack" runat="server" />
</asp:placeholder>