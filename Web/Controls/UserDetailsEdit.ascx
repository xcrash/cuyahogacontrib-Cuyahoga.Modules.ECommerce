<%@ Control Language="c#" AutoEventWireup="false" Codebehind="UserDetailsEdit.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.UserDetailsEdit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<tr>
	<td class="title"><%=GetText("first name")%>:&nbsp;*</td>
	<td>
		<asp:textbox id="txtFirstName" Columns="45" runat="Server"/>
	</td>
	<td><asp:RequiredFieldValidator ID="rfvFirstName" ControlToValidate="txtFirstName" display="dynamic" Runat="Server"/></td>
</tr>
<tr>
	<td class="title"><%=GetText("last name")%>:&nbsp;*</td>
	<td>
		<asp:textbox id="txtLastName" Columns="45" runat="Server"/>
	
	</td>
	<td>	<asp:RequiredFieldValidator ID="rfvLastName" ControlToValidate="txtLastName" display="dynamic" Runat="Server"/></td>
</tr>
<tr>
	<td class="title"><%=GetText("email address")%>:&nbsp;*</td>
	<td>
		<asp:textbox id="txtEmailAddress" Columns="45" runat="server" textmode="SingleLine" />
		
	</td>
	<td><asp:RequiredFieldValidator ID="rfvEmailAddress" ControlToValidate="txtLastName" display="dynamic" Runat="Server"/>
		<asp:RegularExpressionValidator ID="regEmailAddress" ControlToValidate="txtEmailAddress" display="dynamic" Runat="server" /></td>
</tr>

<tr>
	<td class="title"><%=GetText("password")%>:&nbsp;*</td>
	<td>
		<asp:textbox id="txtPassword" TextMode="Password" Columns="45" runat="server"/>
		
		
	</td>
	<td><asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="txtPassword"  display="dynamic" Runat="Server"/>
		<asp:CompareValidator Display="Dynamic" ID="cfvPasswords" runat="server" ControlToValidate="txtPassword"  ControlToCompare="txtConfirmPassword"></asp:CompareValidator></td>
		<asp:CustomValidator ID="cfvInvalidPassword" runat="server" ControlToValidate="txtPassword"></asp:CustomValidator>
</tr>

<tr>
	<td class="title"><%=GetText("confirm password")%>:&nbsp;*</td>
	<td>
		<asp:textbox id="txtConfirmPassword" TextMode="Password" Columns="45" runat="server" />
		
	</td>
	<td><asp:RequiredFieldValidator ID="rfvPasswordConfirm" ControlToValidate="txtConfirmPassword" display="dynamic" Runat="Server"/></td>
</tr>

<tr>
	<td class="title"><%=GetText("telephone number")%>:&nbsp;*</td>
	<td>
		<asp:textbox id="txtTelephoneNumber" Columns="45" runat="server" textmode="SingleLine" />
		
	</td>
	<td><asp:RequiredFieldValidator ID="rfvTelephoneNumber" ControlToValidate="txtTelephoneNumber" display="dynamic" Runat="Server"/></td>
</tr>