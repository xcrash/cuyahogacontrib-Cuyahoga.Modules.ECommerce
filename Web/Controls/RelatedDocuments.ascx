<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelatedDocuments.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.RelatedDocuments" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>

<asp:Repeater ID="rptDocuments" runat="server">
    <HeaderTemplate>
        <h3>Related Documents</h3>
    </HeaderTemplate>
    <ItemTemplate>
      <h4> <%#((IRelatedDocument) Container.DataItem).Name%></h4>
       
       <a href="<%#((IRelatedDocument) Container.DataItem).Url%>">Download</a>
        
       <p><%#((IRelatedDocument) Container.DataItem).Description%></p>
    </ItemTemplate>
    <FooterTemplate>
    </FooterTemplate>
</asp:Repeater>
