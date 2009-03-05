<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BreadCrumbTrail.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.BreadCrumbTrail" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<p id="breadcrumb">
<asp:repeater runat="server" id="rptBreadCrumb">
<headertemplate><a href="default.aspx">Home</a>  &raquo;  <a href="/demo.aspx">Shop</a>  &raquo;  </headertemplate>
<itemtemplate><%# (Container.ItemIndex > 1) ? "&nbsp; &raquo;  &nbsp;" : ""%><%# (Container.ItemIndex > 0) ? ("<a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICategory)Container.DataItem) + "\">" + ((ICategory)Container.DataItem).Name + "</a>") : "" %></itemtemplate>
<footertemplate></footertemplate>
</asp:repeater>
</p>