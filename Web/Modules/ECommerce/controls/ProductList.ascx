<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProductList.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Albion.Web.Controls.ProductList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>

<asp:repeater runat="server" id="rptProducts" EnableViewState="true">
  <headertemplate></headertemplate>
    <itemtemplate>
<asp:PlaceHolder ID="plhNewItemFooter" runat="server">
        </tbody>
    </table>
    </div>
</div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plhNewItemHeader" runat="server">

<div class="category<%#(Container.ItemIndex == 0) ? "first" : ""%>">

    <div class="productimage">
        <asp:Image ID="imgProduct" runat="server" Visible="false" />
    </div>

    <div class="categorydata">
    <asp:Literal ID="litProductLine" runat="server" Visible="false" />
    <h3><asp:Literal ID="litProductName" runat="server" Visible="false" /><asp:HyperLink ID="hplProductName" runat="server" Visible="false" /></h3>
    <h4>DESCRIPTION</h4>
    <asp:Literal ID="litDescription" runat="server" />
    <asp:Literal ID="litKitFeatures" runat="server" Visible="false" />
    <asp:Literal ID="litKitComprises" runat="server" Visible="false" />    
    <table class="productattributes">
        <tbody>
            <tr>
                <th class="finish">Finish</th>
                <th>Code</th>
                <th>Price</th>
                <th></th>
            </tr>
</asp:PlaceHolder>
    
    
            <tr<%#(ItemClass != "") ? (" class=\"" + ItemClass + "\"") : ""%>>
                <td class="finish"><%# GetAttrVal((IProductSummary)Container.DataItem, "finish_type")%></td>
                <td ><%# ((IProductSummary)Container.DataItem).ItemCode %></td>
                <td><asp:Literal ID="litPrice" runat="server"/></td>
                <td><asp:TextBox ID="txtProdID" runat="server" Visible="false" Text="<%#((IProductSummary) Container.DataItem).ProductID%>" /><asp:LinkButton runat="server" OnClick="AddToBasket" Text="Add to basket" CssClass="basket" /></td>
            </tr>
    </itemtemplate>
    <footertemplate>
        </tbody>
    </table>
    </div>
</div>
    </footertemplate>
</asp:repeater>