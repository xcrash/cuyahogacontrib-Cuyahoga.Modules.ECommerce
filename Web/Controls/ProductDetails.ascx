<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetails.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.ProductDetails" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue" %>
<asp:Repeater ID="rptProductDetails" runat="server">
    <HeaderTemplate></HeaderTemplate>
    <ItemTemplate>

        <h3>
            <%# ((IProductSummary)Container.DataItem).Name%>
        </h3>
        <p><strong>Avaliability:</strong>  
        
        <asp:Image ID="imgStockedIndicator" runat="server" /> <asp:Literal ID="litStocked" runat="server"></asp:Literal>
        </p>
        <p><strong>Item Code:</strong>  <%# ((IProductSummary)Container.DataItem).ItemCode %></p> 
       
        
       <strong>Short Summary:</strong> <%# ((IProductSummary)Container.DataItem).ShortDescription%>
       
        <strong>Extended Description:</strong> <%# ((IProductSummary)Container.DataItem).Description%>
        
      <h3> <asp:Literal ID="litPrice" runat="server"/> </h3>
 
        
       
         
      
        

         
    </ItemTemplate>
</asp:Repeater>