<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BasketView.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Albion.Web.Views.BasketView" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util.Interfaces" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util" %>
<div class="list">
	<div class="boxhead"><h2><%=GetText("shopping basket")%></h2></div>
	<div class="boxbody">
	<asp:placeholder runat="server" id="phMessage"><p class="advice" style="color:Black"><%=GetText("your basket is empty")%></p></asp:placeholder>
	<asp:placeholder runat="server" id="phBasket">
		<asp:literal id="litStatusMessage" runat="server"/>
		<table>
			<colgroup><col class="partno"><col class="listprice"><col class="unitprice"><col class="nettprice"><col class="quantity"><col class="amend"></colgroup>
			<thead>
				<tr>
  				    <th><%=GetText("product name")%></td>
					<th><%=GetText("item")%></th>
					<th><%=GetText("list price")%></th>
					<th><%=GetText("quantity")%></th>
					<th><%=GetText("amend line item")%></th>
				</tr>
			</thead>
			<tbody>
<asp:repeater id="rBasket" runat="server" >
	<itemtemplate>
			<tr>
				<td><%#((IBasketLine)Container.DataItem).Description%></td>
				<td><%#((IBasketLine)Container.DataItem).ItemCode%></td>
				<td><%# HtmlFormatUtils.FormatUnitLinePrice((IBasketLine)Container.DataItem)%></td>
				<td><asp:textbox columns="6" runat="server" id="txtQuantity" /><asp:textbox id="txtLineID" visible="False" runat="server" text="<%#((IBasketLine)Container.DataItem).BasketItemID%>" /></td>
				<td><asp:imagebutton id="btnUpdateQuantity" onclick="btnUpdateQuantity_Click" imageurl="~/Modules/ECommerce/Images/cart_edit.png" runat="server" /> <asp:imagebutton id="btnRemoveItem" onclick="btnRemoveItem_Click" imageurl="~/Modules/ECommerce/Images/cart_delete.png" runat="server" /></td>
			</tr>
	</itemtemplate>
</asp:repeater>
			</tbody>
		</table>

<% if (CurrentBasket != null) {%>
		</table>
		<table>
            <tbody>
	        <%if (CurrentBasket.DeliveryPrice.Amount > 0) {%><tr>
		        <td class="title"><%=GetText("delivery charge")%>:</td>
		        <td><%=HtmlFormatUtils.FormatMoney(CurrentBasket.DeliveryPrice)%></td>
	        </tr><%}%>
	        <%if (CurrentBasket.OrderChargePrice.Amount > 0) {%><tr>
		        <td class="title"><%=GetText("small order charge")%>:</td>
		        <td><%=HtmlFormatUtils.FormatMoney(CurrentBasket.OrderChargePrice)%></td>
	        </tr><%}%>
	        <%if (CurrentBasket.AdministrationCharge.Amount > 0) {%><tr>
		        <td class="title"><%=GetText("administration charge")%>:</td>
		        <td><%=HtmlFormatUtils.FormatMoney(CurrentBasket.AdministrationCharge)%></td>
	        </tr><%}%>
	        <tr>
		        <td class="title"><%=GetText("sub total")%>:</td>
		        <td><%=HtmlFormatUtils.FormatMoney(CurrentBasket.SubTotal)%></td>
	        </tr>
	        <tr>
		        <td class="title"><%=GetText("tax")%>:</td>
		        <td><%=HtmlFormatUtils.FormatMoney(CurrentBasket.TaxPrice)%></td>
	        </tr>
	        <tr>
		        <td class="title"><%=GetText("total")%>:</td>
		        <td><%=HtmlFormatUtils.FormatMoney(CurrentBasket.GrandTotal)%></td>
	        </tr>
	        </tbody>
        </table>
<%}%>
		<ul id="basketlinks">
			<li><asp:linkbutton runat="server" id="btnEmpty" />&nbsp;&#187;</li>
			<script language="JavaScript">
			<!--
			if (window.print) {
			document.write('<li><a href="javascript:window.print()"><%=GetText("print")%></a>&nbsp;&#187;</li>');
			}
			//-->
			</script>
<% if (AllowPlaceOrder()) {%>
			<li><a href="<%=StateInfo.AppPath%>Checkout.aspx" class="submit"><%=GetText("checkout")%></a>&nbsp;&#187;</li>
<%}%>
		</ul>
  </asp:placeholder>
	</div>
</div>