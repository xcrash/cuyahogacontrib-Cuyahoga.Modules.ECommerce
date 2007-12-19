<%@ Control Language="c#" AutoEventWireup="false" Codebehind="UserDetailsEdit.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.UserDetailsEdit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<tr>
	<td class="title"><%=GetText("First Name")%>:&nbsp;*</td>
	<td>
		<asp:textbox id="txtFirstName" Columns="45" runat="Server"/>
		<asp:RequiredFieldValidator ID="rfvFirstName" ControlToValidate="txtFirstName" display="dynamic" Runat="Server"/>
	</td>
</tr>
<tr>
	<td class="title"><%=GetText("Last Name")%>:&nbsp;*</td>
	<td>
		<asp:textbox id="txtLastName" Columns="45" runat="Server"/>
		<asp:RequiredFieldValidator ID="rfvLastName" ControlToValidate="txtLastName" display="dynamic" Runat="Server"/>
	</td>
</tr>
<tr>
	<td class="title"><%=GetText("Email Address")%>:&nbsp;*</td>
	<td>
		<asp:textbox id="txtEmailAddress" Columns="45" runat="server" textmode="SingleLine" />
		<asp:RequiredFieldValidator ID="rfvEmailAddress" ControlToValidate="txtLastName" display="dynamic" Runat="Server"/>
		<asp:RegularExpressionValidator ID="regEmailAddress" ControlToValidate="txtEmailAddress" display="dynamic" Runat="server" />
	</td>
</tr>
<tr>
	<td class="title"><%=GetText("telephone number")%>:&nbsp;*</td>
	<td>
		<asp:textbox id="txtTelephoneNumber" Columns="45" runat="server" textmode="SingleLine" />
		<asp:RequiredFieldValidator ID="rfvTelephoneNumber" ControlToValidate="txtTelephoneNumber" display="dynamic" Runat="Server"/>
	</td>
</tr>