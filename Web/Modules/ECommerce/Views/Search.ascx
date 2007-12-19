<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Search.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Albion.Web.Views.Search" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False" %>
<%@ Register tagPrefix="lc" Tagname="ProductList" src="..\Controls\ProductList.ascx"%>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<div id="onecolumn" class="searchresults">
<h3>Search results</h3>
<lc:productlist runat="server" id="ctlProductList" />
<asp:PlaceHolder ID="phNoResults" runat="server" Visible="false">
<p>Sorry, no results have been found for your search. Please alter the terms and try again</p>
</asp:PlaceHolder>
</div>