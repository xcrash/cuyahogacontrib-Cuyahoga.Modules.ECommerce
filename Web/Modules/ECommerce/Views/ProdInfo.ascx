<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProdInfo.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Albion.Web.Views.ProdInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="false" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue" %>
<%@ Register tagPrefix="lc" Tagname="breadcrumb" src="..\Controls\BreadCrumbTrail.ascx"%>
<%@ Register tagPrefix="lc" Tagname="ProductList" src="..\Controls\ProductList.ascx"%>

<div id="onecolumn"><lc:breadcrumb runat="server" id="ctlBreadCrumb" />

<lc:productlist runat="server" id="ctlProductList" />

<asp:repeater runat="server" id="rptCrossSell" Visible="false">
<headertemplate>
	
    <div id="relatedproducts">
        <h4>Display Alternatives</h4>
            <table>
</headertemplate>
<itemtemplate>
		        <tr>
		            <td class="itemcode"><a href="<%# GetProductUrl((IRelatedProducts)Container.DataItem)%>"><%#((IRelatedProducts)Container.DataItem).AccessoryPartNo%></a></td>
		            <td class="description"><%#((IRelatedProducts)Container.DataItem).AccessoryDescription%></td>
		        </tr>
</itemtemplate>
<footertemplate>
        </table>
    </div>
</footertemplate>
</asp:repeater>

</div>