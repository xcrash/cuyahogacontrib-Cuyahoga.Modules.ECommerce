<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductImages.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.ProductImages" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>

<asp:Repeater ID="rptImages" runat="server">
    <HeaderTemplate>
   <h3> Product Images</h3>
    </HeaderTemplate>
    <ItemTemplate>
        <img class="productimage" width="318" height="222" src="<%#((IImage) Container.DataItem).Url%>" alt="<%#((IImage) Container.DataItem).AltText%>" />
    </ItemTemplate>
    <FooterTemplate>
    </FooterTemplate>
</asp:Repeater>
