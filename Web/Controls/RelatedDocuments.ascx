<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelatedDocuments.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.RelatedDocuments" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>

<asp:Repeater ID="rptDocuments" runat="server">
    <HeaderTemplate>
    <ul>
    </HeaderTemplate>
    <ItemTemplate>
      <li> <a href="<%#((IRelatedDocument) Container.DataItem).Url%>"> <%#((IRelatedDocument) Container.DataItem).Name%></a></li>
    </ItemTemplate>
    <FooterTemplate>
    </ul>
    </FooterTemplate>
</asp:Repeater>
