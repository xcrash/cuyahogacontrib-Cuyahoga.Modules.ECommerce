<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Skus.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.Skus" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>

<asp:Repeater ID="rptDocuments" runat="server">
    <HeaderTemplate>
        <h3>Item Codes</h3>
    </HeaderTemplate>
    <ItemTemplate>
      <h4> <%#((ISku)Container.DataItem).Code%></h4>
    </ItemTemplate>
    <FooterTemplate>
    </FooterTemplate>
</asp:Repeater>
