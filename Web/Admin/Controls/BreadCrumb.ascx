<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BreadCrumb.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Controls.BreadCrumb" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<asp:repeater runat="server" id="rptBreadCrumb">
<headertemplate><p id="breadcrumb">&#187;&nbsp;<a href="CatalogueBrowser.aspx?NodeId=19&SectionId=41"><%=RootNodeName%></a></headertemplate>
<itemtemplate><%#(Container.ItemIndex == 0) ? ("") : (NodeSeperator + ((ShowCatItemLink(Container) ? ("<a href=\"CatalogueBrowser.aspx?NodeId=19&SectionId=41&cat=" + ((ICatalogueNode)Container.DataItem).NodeID + "\">") : "") + ((ICatalogueNode)Container.DataItem).Name + ((ShowCatItemLink(Container) ? "</a>" : ""))))%> </itemtemplate>
<footertemplate></p></footertemplate>
</asp:repeater>
<script runat="server">
    public bool ShowCatItemLink(System.Web.UI.WebControls.RepeaterItem item) {
        return (SetLinkOnCurrentNode || item.ItemIndex < (nodeCount - 1));
    }
</script>