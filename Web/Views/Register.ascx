<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Register.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Views.Register" %>
<%@ Register tagPrefix="lc" Tagname="AddressDetails" src="..\Controls\AddressEdit.ascx"%>
<%@ Register tagPrefix="lc" Tagname="UserDetails" src="..\Controls\UserDetailsEdit.ascx"%>

 <table id="register">
<lc:UserDetails runat="server" id="ctlBreadCrumb" />

<lc:AddressDetails runat="server" id="ctlAddress" />
</table>

<asp:LinkButton ID="btnRegister" runat="server" CssClass="updateButton" Text="Register"></asp:LinkButton>