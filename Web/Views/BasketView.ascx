<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BasketView.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Views.BasketView" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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
				    <th width="100px"></th>
  				    
  			  		<th width="50px"><%=GetText("quantity")%></th>
					<th width="50px"><%=GetText("item")%></th>
					<th width="275px"><%=GetText("description")%></th>
					<th width="50px"><%=GetText("list price")%></th>
					<th width="75px"><%=GetText("total price")%></th>
					<th></th>
					<th></th>
				</tr>
			</thead>
			<tbody>
<asp:repeater id="rBasket" runat="server" >
	<itemtemplate>
			<tr class="padding">
			    <td valign="top" width="100px" class="padding"><img width="100px" src="UserFiles/Image/web/<%# RenderImageUrl((IBasketLine)Container.DataItem)%>" /></td>
				<td valign="top" width="50px" class="padding"><p><asp:textbox columns="6" runat="server" id="txtQuantity" /><asp:textbox id="txtLineID" visible="False" runat="server" text="<%#((IBasketLine)Container.DataItem).BasketItemID%>" /></p></td>
				<td valign="top" width="50px" class="padding"><p><%#((IBasketLine)Container.DataItem).ItemCode%></p></td>
				<td valign="top" width="275px" ><%#((IBasketLine)Container.DataItem).Description%></td>
				<td valign="top" width="50px" class="padding"><p><%# HtmlFormatUtils.FormatUnitLinePrice((IBasketLine)Container.DataItem)%></p></td>
				<td valign="top" width="75px"class="padding"><p><%# RenderItemTotal((IBasketLine)Container.DataItem)%></p></td>
				<td valign="top" class="padding"><p><asp:LinkButton id="btnUpdateQuantity" CssClass="updateButton" Text="Update quantity" onclick="btnUpdateQuantity_Click" runat="server" /></p></td>
				<td valign="top" class="padding"> <p><asp:LinkButton id="btnRemoveItem"  CssClass="updateButton" onclick="btnRemoveItem_Click" Text="Remove from basket" runat="server" /></p></td>
			</tr>
	</itemtemplate>
</asp:repeater>
			</tbody>
		</table>

<% if (CurrentBasket != null) {%>
		
		<table id="basketTotals">
            <tbody> 
            
            <tr>
		        <td class="title"><%=GetText("sub total")%>:</td>
		        <td><%=RenderSubTotal()%></td>
	        </tr>
	        
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
		        <td class="title"><%=GetText("tax")%>:</td>
		        <td><%=HtmlFormatUtils.FormatMoney(CurrentBasket.TaxPrice)%></td>
	        </tr>
	        <tr>
		        <td class="title"><strong><%=GetText("total")%>:</strong></td>
		        <td><strong><%=HtmlFormatUtils.FormatMoney(CurrentBasket.GrandTotal)%></strong></td>
	        </tr>
	        </tbody>
        </table>
<%}%>
<br />
		<div id="basketLinks">
			<asp:linkbutton  CssClass="updateButton" runat="server" id="btnEmpty" />
			<script language="JavaScript">
			<!--
			if (window.print) {
			document.write('<a  class="updateButton" href="javascript:window.print()"><%=GetText("print")%></a>');
			}
			//-->
			</script>
<% if (AllowPlaceOrder()) {%>
			<a class="updateButton"  href="<%=StateInfo.AppPath%>Checkout.aspx" class="submit"><%=GetText("checkout")%></a>
<%}%>
		</div>
  </asp:placeholder>
	</div>
</div>