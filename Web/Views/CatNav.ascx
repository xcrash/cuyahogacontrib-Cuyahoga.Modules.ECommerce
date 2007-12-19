<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CatNav.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Views.CatNav" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False" %>
<%@ Register tagPrefix="lc" Tagname="breadcrumb" src="..\Controls\BreadCrumbTrail.ascx"%>
<%@ Register tagPrefix="lc" Tagname="ProductList" src="..\Controls\ProductList.ascx"%>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>


	<lc:breadcrumb runat="server" id="ctlBreadCrumb" />

	<asp:Panel ID="pnlSummary" runat="server" Visible="false">
	<h3><asp:Label ID="lblTitle" runat="server" /></h3>
	<asp:Label ID="lblDescription" runat="server" />
	</asp:Panel>

	<asp:repeater runat="server" id="rptCategories">
	<headertemplate></headertemplate>
	    <itemtemplate>
	        <a class="subCategory" href="<%# UrlHelper.GetCatalogueNodeUrl((ICatalogueNode)Container.DataItem)%>"><%#((ICatalogueNode)Container.DataItem).Name%></a>
	    </itemtemplate>
	<footertemplate></div></footertemplate>
	</asp:repeater>

	<asp:Panel ID="pnlCatImage" runat="server" Visible="false">
	
	    <div id="images">
		    <img src="<%=CatImageUrl%>" alt="<%=CatImageAltText%>" />
	    </div>
	    
	</asp:Panel>

<lc:ProductList runat="server" id="ctlProductList" />
