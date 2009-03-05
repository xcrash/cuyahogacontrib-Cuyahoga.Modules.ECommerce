<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CatNav.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Views.CatNav" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False" %>
<%@ Register tagPrefix="lc" Tagname="breadcrumb" src="..\Controls\BreadCrumbTrail.ascx"%>
<%@ Register tagPrefix="lc" Tagname="ProductList" src="..\Controls\ProductList.ascx"%>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>

	<lc:breadcrumb runat="server" id="ctlBreadCrumb" />

	<asp:Panel ID="pnlSummary" runat="server">
	    <h3><asp:Label ID="lblTitle" runat="server" /></h3>
	    <asp:Label ID="lblDescription" runat="server" />
	</asp:Panel>
	<asp:Panel ID="pnlCatImage" runat="server" Visible="false">
	    <div id="images">
		    <img src="<%=BannerImageUrl%>" alt="<%=CatImageAltText%>" Width="500px"/>
	    </div>
	</asp:Panel>
	<asp:repeater runat="server" id="rptCategories">
	    <headertemplate><table id="categoryList"><tr></headertemplate>
	    <itemtemplate>
	        
	       <td>
	       <h3 class="headerBackGround">
	        <a class="subCategory" href="<%# UrlHelper.GetCatalogueNodeUrl((ICategory)Container.DataItem)%>"><%#((ICategory)Container.DataItem).Name%></a></h3>
	        <asp:Image ID="imgSubCategory" runat="server" CssClass="subCategoryImage" ImageUrl="http://demo.magentocommerce.com/skin/frontend/default/default/images/media/best_selling_img01.jpg" />
	   </td>
	    <asp:Literal runat="server" ID="rowFooter"></tr></asp:Literal>
	    <asp:Literal id="rowHeader" runat="server"><tr></asp:Literal>
	    </itemtemplate>
	    <footertemplate></tr></table></footertemplate>
	</asp:repeater>


	

<lc:productlist runat="server" id="ctlProductList" />
  
