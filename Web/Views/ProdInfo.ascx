<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProdInfo.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Views.ProdInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="false" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue" %>
<%@ Register tagPrefix="lc" Tagname="breadcrumb" src="..\Controls\BreadCrumbTrail.ascx"%>
<%@ Register tagPrefix="lc" Tagname="ProductList" src="..\Controls\ProductList.ascx"%>
<%@ Register tagPrefix="lc" Tagname="RelatedDocuments" src="..\Controls\RelatedDocuments.ascx"%>
<%@ Register tagPrefix="lc" Tagname="RelatedProducts" src="..\Controls\RelatedProducts.ascx"%>
<%@ Register tagPrefix="lc" Tagname="Skus" src="..\Controls\Skus.ascx"%>
<%@ Register tagPrefix="lc" Tagname="ProductImages" src="..\Controls\ProductImages.ascx"%>
<%@ Register tagPrefix="lc" Tagname="Attributes" src="..\Controls\Attributes.ascx"%>
<%@ Register tagPrefix="lc" Tagname="Details" src="..\Controls\ProductDetails.ascx"%>


<lc:breadcrumb runat="server" id="ctlBreadCrumb" />

<div id="productLeft">

<lc:ProductImages runat="server" id="ctlImages" />

<div class="tab-container" id="container1">
	<ul id="sections">
		<li><a href="#" onClick="return showPane('pane1', this)" id="tab1">Product Attributes</a></li>
		<li><a href="#" onClick="return showPane('pane2', this)" id="tab2">Related Products</a></li>
		<li><a href="#" onClick="return showPane('pane4', this)" id="tab4">Related Documents</a></li>
	</ul>
	<div class="rounded">
		<div class="tab-panes">
			<div id="pane1">
				<h2>Configure Product</h2>
				      <div class="openbox list">
                         <lc:Attributes runat="server" id="ctlAttributes" />
                     </div>
			</div>
			<div id="pane2">
				<h2>Related Products</h2>
				<div class="openbox list">
				<lc:RelatedProducts runat="server" id="ctlRelatedProducts" />
				</div>
			</div>
			<div id="pane4">
				<h2>Related Documents</h2>
				<div class="openbox list">
				
				<lc:RelatedDocuments runat="server" id="ctlDocuments" />
				</div>
			</div> 
			
			
		</div>
		
	</div>
</div>

<div class="mainbox feature clear">
<div class="boxhead"><h2>Features</h2></div>
		
			
		<div class="boxbody">
	<%=prodView.ProductDetails.Features%>
	
	</div></div>
</div>
	
	
	
	
	<div id="productDetails">
    <lc:Details runat="server" id="ctlDetails" />
    
 <div id="buy" class="mainbox feature form">
	
		<div class="boxhead"><h2>Ordering Instructions</h2></div>
		
			
		<div class="boxbody">
	
			<table width="500px">					
				<colgroup>

				<col class="partno"/>
				<col class="stock"/>
				<col class="price"/>
				<col class="quantity"/>
				<col class="basket"/>
				</colgroup>						
			<thead>
			<tr>
				<th class="first" align="left" width="150px">Item</th>

				<th colspan="2" align="left" width="50px">Availability</th>
				<th colspan="2" align="left" width="100px">Qty.</th>
			</tr>
			</thead>
			<tbody>
			<tr>
				<td class="first"><h5><%=prodView.ProductDetails.ItemCode%></h5></td>

				
				<td><asp:Literal runat="server" ID="litStocked"></asp:Literal></td>
				<td></td>
				<td>
				<asp:TextBox ID="txtProdID" runat="server" Visible="false" />
    
				<asp:TextBox ID="txtQuantity" Text="1" runat="server"  MaxLength="12" CssClass="quantity" /> </td>
					<td align="right" style="background-color:White;"> <asp:LinkButton ID="LinkButton1" runat="server" OnClick="AddToBasket" Text="Add to basket" CssClass="addbasket" /> </td></td>
				
				
			</tr>
			</tbody>
			</table>
		</div>

		
		
	</div>
    

</div>



<div class="clear" />