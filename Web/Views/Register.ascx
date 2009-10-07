<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Register.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Views.Register" %>
<%@ Register tagPrefix="lc" Tagname="AddressDetails" src="..\Controls\AddressEdit.ascx"%>
<%@ Register tagPrefix="lc" Tagname="UserDetails" src="..\Controls\UserDetailsEdit.ascx"%>

 <table id="register">
<lc:UserDetails runat="server" id="ctlUser" />

<lc:AddressDetails runat="server" id="ctlUserAddress" />
</table>
<asp:Label ID="lbMessage" runat="server" ></asp:Label>
<asp:LinkButton ID="btnRegister" runat="server" CssClass="updateButton" Text="Register"></asp:LinkButton>