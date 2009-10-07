<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Address.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.AddressEdit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:placeholder id="phError" runat="server" Visible="false">
	<tr>
		<td colspan="2"><p class="warning"><%=GetText("invalid postal address")%></p></td>
	</tr> 
</asp:placeholder>
<asp:placeholder id="phFullInfo" runat="server">
<tr>
	<td class="title"><%=GetText("company name")%>:<%= (rfvCompanyName.Enabled) ? "&nbsp;*" : ""%></td>
	<td>
		<asp:textbox ID="txtContactName" Runat="Server" Columns="45" />
		<asp:requiredfieldvalidator ID="rfvCompanyName" Runat="Server" ControlToValidate="txtContactName" Display="dynamic" />
	</td>
</tr>
</asp:placeholder>
<tr>
	<td class="title"><%=GetText("address 1")%>:<%= (rfvAddress1.Enabled) ? "&nbsp;*" : ""%></td>
	<td>
		<asp:textbox id="txtAddress1" Columns="45" runat="Server"/>
		<asp:RequiredFieldValidator ID="rfvAddress1" ControlToValidate="txtAddress1" display="dynamic" Runat="Server"/>
	</td>
</tr>
<tr>
	<td class="title"><%=GetText("address 2")%>:</td>
	<td><asp:textbox id="txtAddress2" Columns="45" runat="server" textmode="SingleLine" /></td>
</tr>
<tr>
	<td class="title"><%=GetText("address 3")%>:</td>
	<td><asp:textbox id="txtAddress3" Columns="45" runat="server" textmode="SingleLine" /></td>
</tr>
<tr>
	<td class="title"><%=GetText("city")%>:<%= (rfvCity.Enabled) ? "&nbsp;*" : ""%></td>
	<td>
		<asp:textbox id="txtCity" Columns="45" runat="Server"/>
		<asp:RequiredFieldValidator ID="rfvCity" ControlToValidate="txtCity" display="dynamic" Runat="Server"/>
	</td>
</tr>
<tr>
	<td class="title"><%=GetText("region")%>:<%= (rfvRegion.Enabled) ? "&nbsp;*" : ""%></td>
	<td>
		<asp:textbox id="txtRegion" Columns="45" runat="server" textmode="SingleLine" />
		<asp:RequiredFieldValidator ID="rfvRegion" ControlToValidate="txtRegion" display="dynamic" Runat="Server"/>
	</td>
</tr> 
<tr>
	<td class="title"><%=GetText("postcode")%>:<%= (rfvPostCode.Enabled) ? "&nbsp;*" : ""%></td>
	<td>
		<asp:textbox id="txtPostcode" Columns="10" runat="Server"/>
		<asp:RequiredFieldValidator ID="rfvPostCode" ControlToValidate="txtPostcode" display="dynamic" Runat="Server"/>
	</td>
</tr>
<tr>
	<td class="title"><%=GetText("country")%>:&nbsp;*</td>
	<td>
		<asp:dropdownlist id="ddlCountry" runat="server" />
		<asp:RequiredFieldValidator ID="rfvCountry" ControlToValidate="ddlCountry" display="dynamic" Runat="server"/>
	</td>
</tr>