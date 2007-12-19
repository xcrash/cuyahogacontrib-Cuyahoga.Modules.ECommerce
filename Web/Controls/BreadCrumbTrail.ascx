<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BreadCrumbTrail.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.BreadCrumbTrail" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<p id="breadcrumb">
<asp:repeater runat="server" id="rptBreadCrumb">
<headertemplate></headertemplate>
<itemtemplate><%# (Container.ItemIndex > 1) ? "&nbsp;&gt;&nbsp;" : ""%><%# (Container.ItemIndex > 0) ? ("<a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICatalogueNode)Container.DataItem) + "\">" + ((ICatalogueNode)Container.DataItem).Name + "</a>") : "" %></itemtemplate>
<footertemplate></footertemplate>
</asp:repeater>
</p>