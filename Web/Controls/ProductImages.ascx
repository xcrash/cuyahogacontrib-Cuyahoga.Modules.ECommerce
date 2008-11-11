<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductImages.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.ProductImages" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>

<div id="productImages">
      <div class="largeProductImage">
        <asp:HyperLink runat="server" ID="hplImagePopUp"><asp:Image ID="imgProduct" runat="server" Visible="false"   /></asp:HyperLink>
    </div>
    
    <div class="smallProductImage">
        <asp:HyperLink runat="server" ID="hplImagePopUp2"><asp:Image ID="imgProduct2" runat="server" Visible="false"   /></asp:HyperLink>
    </div>
    
    <div class="smallProductImage">
        <asp:HyperLink runat="server" ID="hplImagePopUp3"><asp:Image ID="imgProduct3" runat="server" Visible="false"   /></asp:HyperLink>
    </div>
</div>    
    

