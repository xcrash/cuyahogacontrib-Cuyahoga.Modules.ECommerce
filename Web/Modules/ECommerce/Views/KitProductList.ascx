<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KitProductList.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Albion.Web.Views.KitProductList" %>
<div id="onecolumn"><asp:Repeater ID="rptKits" runat="server">

<asp:repeater runat="server" id="rptProducts" EnableViewState="true">
<headertemplate>



</headertemplate>
<itemtemplate>
   <asp:PlaceHolder ID="plhNewItemFooter" runat="server">
	    </tbody>
        </table>
        </div>
    </asp:PlaceHolder>
   <asp:PlaceHolder ID="plhNewItemHeader" runat="server">
   <div class="productimage">

				<asp:Image ID="imgProduct" runat="server" Width="100px" Height="100px" />

			</div>
    <div id="category">
	<h3><asp:Literal ID="litProductName" runat="server"></asp:Literal></h3>
	<h4>DESCRIPTION</h4>
	<asp:Literal ID="litDescription" runat="server"></asp:Literal>					
	<table class="productattributes">
		<tbody>
		<tr>
		    <td><h4><asp:Literal ID="litFinish" runat="server" Visible="true"></asp:Literal></h4></td>
		    <td><h4>Code</h4></td>
		    <td><h4>Price</h4></td>
		    <td></td>
		</tr>
	<tr>
	</asp:PlaceHolder>
		<td><%# GetAttrVal((IProductSummary)Container.DataItem, "finish_type")%></td>
		<td><%# ((IProductSummary)Container.DataItem).ItemCode %></td>
		<td><a href="<%# UrlHelper.GetProductUrl((IProductSummary) Container.DataItem)%>"><%#((IProductSummary)Container.DataItem).Description%></a></td>
		<td><asp:TextBox ID="txtProdID" runat="server" Visible="false" Text="<%#((IProductSummary) Container.DataItem).ProductID%>" /><asp:LinkButton ID="LinkButton1" runat="server" OnClick="AddToBasket" Text="Add to basket" /></td>
	</tr>
	 
</itemtemplate>
<footertemplate>
	 </tbody>
        </table>
</div>
</footertemplate>
</asp:repeater>

</div>