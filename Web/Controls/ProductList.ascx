<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProductList.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.ProductList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>

<asp:repeater runat="server" id="rptProducts" EnableViewState="true">
  <headertemplate><table id="productList"></headertemplate>
    <itemtemplate>

   <tr<%#(ItemClass != "") ? (" class=\"" + ItemClass + "\"") : ""%>>


    <td>
        <asp:Image ID="imgProduct" runat="server" Visible="false" />
    </td>


    <td><asp:Literal ID="litProductName" runat="server" Visible="false" /><asp:HyperLink ID="hplProductName" runat="server" Visible="false" /></td>
 
    <td><asp:Literal ID="litDescription" runat="server" /></td>

    
    
         
                
                <td ><%# ((IProductSummary)Container.DataItem).ItemCode %></td>
                <td><asp:Literal ID="litPrice" runat="server"/></td>
                <td><asp:TextBox ID="txtProdID" runat="server" Visible="false" Text="<%#((IProductSummary) Container.DataItem).ProductID%>" /><asp:LinkButton runat="server" OnClick="AddToBasket" Text="Add to basket" CssClass="basket" /></td>
            </tr>
    </itemtemplate>
    <footertemplate>
  </table>
    </footertemplate>
</asp:repeater>