<%@ Control Language="c#" AutoEventWireup="false" Codebehind="UserDetailsView.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.UserDetailsView" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util.Interfaces" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util" %>
<div class="confirmation form">
<h3><%=GetText("user details")%></h3>
<table>
<colgroup>
	<col class="title"/>
	<col class="field"/>
</colgroup>
<tbody><%if (UserDetailsAlt != null) {%><% if (!String.IsNullOrEmpty(UserDetailsAlt.AccountNumber)) {%>
	<tr>
		<td class="title"><%=GetText("account number")%></td>
		<td><%=UserDetailsAlt.AccountNumber%></td>		
	</tr><%}%><% if (!String.IsNullOrEmpty(UserDetailsAlt.CompanyName)) {%>
	<tr>
		<td class="title"><%=GetText("company name")%></td>
		<td><%=UserDetailsAlt.CompanyName%></td>		
	</tr><%}%>
	<%}%>
	
	<% if (UserDetails != null) {  %>
	
	<% if (!String.IsNullOrEmpty(UserDetails.FirstName)) {%>
	<tr>
		<td class="title"><%=GetText("first name")%></td>
		<td><%=UserDetails.FirstName%></td>		
	</tr><%}%><% if (!String.IsNullOrEmpty(UserDetails.LastName)) {%>
	<tr>
		<td class="title"><%=GetText("last name")%></td>
		<td><%=UserDetails.LastName%></td>		
	</tr><%}%><% if (!String.IsNullOrEmpty(UserDetails.EmailAddress)) {%>
	<tr>
		<td class="title"><%=GetText("email address")%></td>
		<td><%=UserDetails.EmailAddress%></td>		
	</tr><%}%><% if (!String.IsNullOrEmpty(UserDetails.TelephoneNumber)) {%>
	<tr>
		<td class="title"><%=GetText("telephone number")%></td>
		<td><%=UserDetails.TelephoneNumber%></td>		
	</tr><%}%>
	
	<% } %>
</tbody>
</table>
</div>