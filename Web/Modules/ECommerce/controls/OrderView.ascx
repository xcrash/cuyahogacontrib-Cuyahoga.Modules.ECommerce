<%@ Control Language="c#" AutoEventWireup="false" Codebehind="OrderView.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Albion.Web.Controls.OrderView" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util.Interfaces" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util" %>
<div class="confirmation form">
<h3><%=GetText("order details")%></h3>
<table>
<colgroup>
	<col class="title"/>
	<col class="field"/>
</colgroup>
<tbody>
    <%if (Order != null && Order.IsPurchased) {%>
	<tr>
		<td class="title"><%=GetText("order number")%></td>
		<td><%=Header.OrderHeaderID%></td>		
	</tr>
	<%}%>
	<%if (Header != null && Header.PurchaseOrderNumber != null && Header.PurchaseOrderNumber.Length > 0) {%>
	<tr>
		<td class="title"><%=GetText("purchase order number")%></td>
		<td><%=Header.PurchaseOrderNumber%></td>		
	</tr><%}%>
    <%if (Header != null) {%>
	<tr>
		<td class="title"><%=GetText("payment method")%></td>
		<td><%=GetText("payment_method_" + (Header.PaymentMethod.ToString().ToLower()))%></td>		
	</tr><%}%>
	</tbody>
</table>
</div>

<div class="confirmation list">
<table id="basketcontents">
	<colgroup><col class="partno"/><col class="listprice"/><col class="unitprice"/><col class="nettprice"/><col class="quantity"/><col class="amend"/></colgroup>
	<thead>
	<tr>
		<th class="description"><%=GetText("product name")%></th>
		<th class="first"><%=GetText("item")%></th>
		<th class="price"><%=GetText("list price")%></th>
		<th class="quantity"><%=GetText("quantity")%></th>
	</tr>
	</thead>
	<tbody>
<asp:repeater runat="server" id="rptBasketLines">
<itemtemplate>
	<tr<%#(Container.ItemIndex % 2 == 1) ? " class=\"even\"" : ""%>>
		<td class="first"><%#((IBasketLine)Container.DataItem).Description%></td>
		<td class="first"><%#((IBasketLine)Container.DataItem).ItemCode%></td>
		<td class="first"><%# HtmlFormatUtils.FormatUnitLinePrice((IBasketLine)Container.DataItem)%></td>
		<td class="quantity"><%#((IBasketLine)Container.DataItem).Quantity.ToString()%></td>
	</tr>
</itemtemplate>
</asp:repeater>
	</tbody>
</table>
<%if (Order != null) {%>
<table id="baskettotal">
	<tbody>
	<%if (Order.DeliveryPrice.Amount > 0) {%><tr>
		<td class="title"><%=GetText("delivery charge")%>:</td>
		<td><%=HtmlFormatUtils.FormatMoney(Order.DeliveryPrice)%></td>
	</tr><%}%>
	<%if (Order.OrderChargePrice.Amount > 0) {%><tr>
		<td class="title"><%=GetText("small order charge")%>:</td>
		<td><%=HtmlFormatUtils.FormatMoney(Order.OrderChargePrice)%></td>
	</tr><%}%>
	<%if (Order.AdministrationCharge.Amount > 0) {%><tr>
		<td class="title"><%=GetText("administration charge")%>:</td>
		<td><%=HtmlFormatUtils.FormatMoney(Order.AdministrationCharge)%></td>
	</tr><%}%>
	<tr>
		<td class="title"><%=GetText("sub total")%>:</td>
		<td><%=HtmlFormatUtils.FormatMoney(Order.SubTotal)%></td>
	</tr>
	<tr>
		<td class="title"><%=GetText("tax")%>:</td>
		<td><%=HtmlFormatUtils.FormatMoney(Order.TaxPrice)%></td>
	</tr>
	<tr>
		<td class="title"><%=GetText("total")%>:</td>
		<td><%=HtmlFormatUtils.FormatMoney(Order.GrandTotal)%></td>
	</tr>
	</tbody>
</table>
<%}%>
</div>