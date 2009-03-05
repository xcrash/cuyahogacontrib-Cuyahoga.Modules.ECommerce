<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Checkout.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Views.Checkout" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="ecommerce" TagName="Address" Src="../Controls/AddressEdit.ascx" %>
<%@ Register TagPrefix="ecommerce" TagName="User" Src="../Controls/UserDetailsEdit.ascx" %>
<%@ Register TagPrefix="ecommerce" TagName="Login" Src="../Controls/Login.ascx" %>
<%@ Register TagPrefix="ecommerce" TagName="OrderViewComposite" Src="../Controls/OrderViewComposite.ascx" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util" %>

<asp:placeholder id="phStep1" runat="server" Visible="false">
   
    <asp:RadioButtonList ID="rblPaymentMethod" runat="server" RepeatDirection="Vertical">
        <asp:ListItem Value="creditcard">By credit/debit card</asp:ListItem>
        <asp:ListItem Value="account">On account</asp:ListItem>
    </asp:RadioButtonList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblPaymentMethod" ErrorMessage="You must select a payment method" />
</asp:placeholder>

<asp:placeholder id="phStep1a" runat="server">
     <ecommerce:Login runat="server" id="ctlLogin" />  
</asp:placeholder>

<asp:placeholder id="phStep2a" runat="server">
  
  <p class="checkout">Enter your address details below and submit your order request.</p>
  <table>
<tr>
	<td class="title"><%=GetText("account number")%>:&nbsp;*</td>
	<td>
		<asp:textbox id="txtAccountNumber" Columns="45" runat="server" textmode="SingleLine" />
		<asp:RequiredFieldValidator ID="rfvAccountNumber" ControlToValidate="txtAccountNumber" display="dynamic" Runat="Server" ErrorMessage="Required Field"/>
	</td>
</tr>
<tr>
	<td class="title"><%=GetText("company name")%>:&nbsp;*</td>
	<td>
		<asp:textbox id="txtCompanyName" Columns="45" runat="server" textmode="SingleLine" />
		<asp:RequiredFieldValidator ID="rfvCompanyName" ControlToValidate="txtCompanyName" display="dynamic" Runat="Server" ErrorMessage="Required Field"/>
	</td>
</tr>
  <ecommerce:user runat="server" id="ctlUserAcct" />
  </table>
</asp:placeholder>
<asp:placeholder id="phStep2b" runat="server">
 
 <table>
  <tr><td colspan="2"><h3><%=GetText("User Details")%></h3></td></tr>
  <ecommerce:user runat="server" id="ctlUser" />
  <tr><td colspan="2">&nbsp;</td></tr>
  <tr><td colspan="2"><h3><%=GetText("Invoice Address")%></h3></td></tr>
  <ecommerce:address runat="server" id="ctlInvoiceAddress" AutoPostBackOnCountryChange="true" />
  <tr><td colspan="2">&nbsp;</td></tr>
  <tr><td colspan="2"><h3><%=GetText("Delivery Address")%></h3></td></tr>
  <tr><td colspan="2"><%=GetText("Delivery Address same as invoice address")%> <asp:checkbox id="chkSameAddress" checked="true" runat="server" autopostback="true"/></td></tr>
  <tr><td colspan="2">&nbsp;</td></tr>
  <ecommerce:address runat="server" id="ctlDeliveryAddress" enabled="false" countryenabled="false" />
  </table>
</asp:placeholder>
<asp:placeholder id="phStep3a" runat="server">
 
  <p class="checkout">Thank you for your order. Your order details have been submitted to leeCommerce.</p>
</asp:placeholder>
<asp:placeholder id="phStep3b" runat="server">
 
  <p class="checkout">Thank you for your order. Your order details have been submitted to leeCommerce. Please check order details and proceed to pay.</p>
  <ecommerce:orderviewcomposite runat="server" id="ctlOrderViewComposite" />  
</asp:placeholder>
<div class="clear"></div>
<asp:button id="btnPrevious" CssClass="updateButton"  runat="server" OnClientClick="history.back();"/><asp:button id="btnNext" CssClass="updateButton" runat="server"/>
