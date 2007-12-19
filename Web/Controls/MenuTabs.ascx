<%@ Control Language="c#" AutoEventWireup="false" Codebehind="MenuTabs.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.MenuTabs" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<div id="tabwrapper">
<asp:repeater runat="server" id="rptMenu">
<headertemplate><ul></headertemplate>
<itemtemplate><li><a href="<%#UrlHelper.GetCatalogueNodeUrl((ICatalogueNode) Container.DataItem)%>"><%#((ICatalogueNode)Container.DataItem).Name%></a></li></itemtemplate>
<footertemplate></ul></footertemplate>
</asp:repeater>
</div>