<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProdInfo.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Views.ProdInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="false" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue" %>
<%@ Register tagPrefix="lc" Tagname="breadcrumb" src="..\Controls\BreadCrumbTrail.ascx"%>
<%@ Register tagPrefix="lc" Tagname="relateddocuments" src="..\Controls\RelatedDocuments.ascx"%>
<%@ Register tagPrefix="lc" Tagname="relatedproducts" src="..\Controls\RelatedProducts.ascx"%>
<%@ Register tagPrefix="lc" Tagname="skus" src="..\Controls\Skus.ascx"%>
<%@ Register tagPrefix="lc" Tagname="images" src="..\Controls\ProductImages.ascx"%>

<lc:breadcrumb runat="server" id="ctlBreadCrumb" />

<h3><asp:Label ID="lblTitle" runat="server" /></h3>

<div id="description">
	<asp:Label ID="lblDescription" runat="server" />
</div>

<div id="stocked">
Product in stock: <asp:Image ID="imgStockedIcon" runat="server" />
</div>

<div id="pricing">
  <asp:Literal ID="litPrice" runat="server"/>
   
   <asp:LinkButton ID="btnAddToBasket" runat="server"  Text="Add to basket" CssClass="basket" />
</div>  

<div id="documents>    
<lc:relateddocuments runat="server" id="ctlRelatedDocuments" />
</div>

<div id="relatedProducts>
<lc:relatedproducts runat="server" id="ctlRelatedProducts" />
</div>

<div id="skus">
<lc:skus runat="server" id="ctlSkus" />
</div>

<div id="images">
<lc:images runat="server" id="ctlImages" />
</div>

<div id="attributes">
<asp:repeater runat="server" id="rptImageAttributes">
	<headertemplate>				
	</headertemplate>
	<itemtemplate>			
	</itemtemplate>
	<footertemplate>
	</footertemplate>
</asp:repeater>

<asp:repeater runat="server" id="rptTextAttributes">
	<headertemplate>
	<h3>Features</h3>	
	<ul>	
	</headertemplate>
	<itemtemplate>	
	  
	   <li>  <asp:Literal ID="litText" runat="server" />:
	    <asp:Literal ID="litValue" runat="server" />
	   </li>
	     
	</itemtemplate>
	<footertemplate>
	 </ul>
	</footertemplate>
</asp:repeater>

<asp:repeater runat="server" id="rptCheckBoxAttributes">
	<headertemplate>				
	</headertemplate>
	<itemtemplate>	
	    <asp:Label ID="lblCheckBoxAttribute" runat="server" />
	    <asp:CheckBoxList ID="cblCheckBoxList" runat="server"> 
	    </asp:CheckBoxList>		
	</itemtemplate>
	<footertemplate>
	</footertemplate>
</asp:repeater>

<asp:repeater runat="server" id="rptRadioAttributes">
	<headertemplate>				
	</headertemplate>
	<itemtemplate>	
	    <asp:Label ID="lblRadioAttribute" runat="server" />
	    <asp:RadioButtonList ID="rblCheckBoxList" runat="server"> 
	    </asp:RadioButtonList>		
	</itemtemplate>
	<footertemplate>
	</footertemplate>
</asp:repeater>

<asp:repeater runat="server" id="rptTableAttributes">
	<headertemplate>				
	</headertemplate>
	<itemtemplate>	
	    <asp:Label ID="lblTableAttribute" runat="server" />
		<asp:DataList ID="dlTableAttribute" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
		    <HeaderTemplate>
		        
		    </HeaderTemplate>
		    <ItemTemplate>
		       <%#((IAttributeOption)Container.DataItem).PickListValue%>
		    </ItemTemplate>
		    <AlternatingItemTemplate>
	             <%#((IAttributeOption)Container.DataItem).PickListValue%>
    	   </AlternatingItemTemplate>
		</asp:DataList>
	</itemtemplate>
	<footertemplate>
	</footertemplate>
</asp:repeater>

<asp:repeater runat="server" id="rptLinkAttributes">
	<headertemplate>				
	</headertemplate>
	<itemtemplate>
	    <asp:HyperLink ID="hplLinkattribute" runat="server" />			
	</itemtemplate>
	<footertemplate>
	</footertemplate>
</asp:repeater>
<asp:PlaceHolder ID="plhAttributes" runat="server" />
</div>

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

