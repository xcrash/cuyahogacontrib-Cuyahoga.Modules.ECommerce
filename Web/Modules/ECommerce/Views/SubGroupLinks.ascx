<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubGroupLinks.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Albion.Web.Views.SubGroupLinks" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain" %>
<%@ Import Namespace="Cuyahoga.Web.Util" %>
<asp:repeater runat="server" id="rptLinks">
<headertemplate></headertemplate>
<itemtemplate>
<div class="footerbox">
    <h3><asp:HyperLink ID="hplLink" runat="server" /></h3>
    <div class="footerboxcontent"><a href="<%# UrlHelper.GetFriendlyUrlFromNode(((CategoryLink) Container.DataItem).Node)%>"><asp:Image ID="imgLink" runat="server" Width="95px" Height="105px"/></a></div>
</div>
</itemtemplate>
<footertemplate></footertemplate>
</asp:repeater>
<%if (imgCategory.Visible) {%>
<div class="catlogo"><asp:Image runat="server" ID="imgCategory" Visible="false" /></div>
<%}%>