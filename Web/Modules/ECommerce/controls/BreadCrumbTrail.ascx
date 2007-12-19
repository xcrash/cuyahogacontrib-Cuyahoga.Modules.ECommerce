<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BreadCrumbTrail.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.BreadCrumbTrail" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<asp:repeater runat="server" id="rptBreadCrumb">
<headertemplate>Location:  <a href="/Default.aspx">Products</a></headertemplate>
<itemtemplate><%#(((ICatalogueNode)Container.DataItem).ParentNodeID == 0) ? ("") : (" \\ <a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICatalogueNode)Container.DataItem) + "\">" + ((ICatalogueNode)Container.DataItem).Name + "</a>") %> </itemtemplate>
<footertemplate></footertemplate>
</asp:repeater>