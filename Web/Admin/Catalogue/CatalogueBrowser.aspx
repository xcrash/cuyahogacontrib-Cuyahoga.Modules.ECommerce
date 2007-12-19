<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatalogueBrowser.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.CatalogueBrowser" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<%@ Import Namespace="Cuyahoga.Web.Util" %>
<%@ Register TagPrefix="breadcrumb" TagName="menu" Src="../Controls/BreadCrumb.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Ecommerce Module Administration</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Catalogue Browser</h1>
   	<div class="post">
   	    <p>From here you navigate through your product catalogue. </p>
   	    
   	    <h3>Edit a Category</h3>
   	    <p>To edit a categories properties, simply click the edit button next to the category name.</p>
   	    
   	    <h3>Add a Category</h3>
   	    <p>To add a category, click the 'Add a new subcategory here' link when you are residing in the category you wish to place it.</p>
   	    
   	    <h3>Add a Product</h3>
   	    <p>To add a product, click the 'Add new product' link when you are residing in the category you wish to place it.</p>
   	</div>
   	<asp:Panel runat="server" ID="pnlMessage" Visible="false" CssClass="message">
   	    <asp:Label ID="lblMessage" runat="server" />
   	</asp:Panel>
   
        <breadcrumb:menu runat="server" id="ctlBreadCrumb" SetLinkOnCurrentNode="false"/>

        <asp:repeater runat="server" id="rptCategories">
			<headertemplate>
			<table id="Table1">
				<colgroup>
					<col class="description"/>
					<col class="image"/>
				</colgroup>
				<tbody>
			</headertemplate>
			<itemtemplate>
				<tr<%#(Container.ItemIndex % 2 == 1) ? " class=\"even\"" : ""%>>
					<td>
						<h3><a href="CatalogueBrowser.aspx<%#GetBaseQueryString()%>&cat=<%#((ICatalogueNode)Container.DataItem).NodeID%>"><%#((ICatalogueNode)Container.DataItem).Name%></a></h3>
					</td>
                    <td><a href="CategoryEdit.aspx<%#GetBaseQueryString()%>&cat=<%#((ICatalogueNode)Container.DataItem).NodeID%>"><img src="../../images/folder_edit.png" alt="Edit Category"/></a></td>
                    <td><a href="CategoryDelete.aspx<%#GetBaseQueryString()%>&cat=<%#((ICatalogueNode)Container.DataItem).NodeID%>"><img src="../../images/folder_delete.png" alt="Delete Category"/></a></td>
                    <td><a href="CategoryMove.aspx<%#GetBaseQueryString()%>&cat=<%#((ICatalogueNode)Container.DataItem).NodeID%>&direction=Up"><img src="<%=Cuyahoga.Web.Util.UrlHelper.GetSiteUrl()%>/admin/images/arrow_up.png" border="0"/></a> <a href="CategoryMove.aspx<%#GetBaseQueryString()%>&cat=<%#((ICatalogueNode)Container.DataItem).NodeID%>&direction=Down"><img src="<%=Cuyahoga.Web.Util.UrlHelper.GetSiteUrl()%>/admin/images/arrow_down.png" border="0"/></a>
				</tr>
			</itemtemplate>
			<footertemplate>
				</tbody>
			</table>
			<%# (ShowAddCategory) ? ((IsRoot) ? ("<a href=\"CategoryEdit.aspx" + GetBaseQueryString() + "&parentCategory=" + CatID + "\">Add a new category here</a>") : "<a href=\"CategoryEdit.aspx" + GetBaseQueryString() + "&parentCategory=" + CatID + "\">Add a new subcategory here</a>") : ""%>
			</footertemplate>
			</asp:repeater>
			
			<asp:repeater runat="server" id="rptProducts">
			<headertemplate>
				<table id="catalogue">
					<tr>
					<td>Item Code</td>
					<td>Product Name</td>
					<td colspan="3">&nbsp;</td>
					</tr>
					<tbody></headertemplate>
			<itemtemplate>
				<tr<%#(Container.ItemIndex % 2 == 1) ? " class=\"even\"" : ""%>>
					<td><%# ((IProductSummary)Container.DataItem).ItemCode %></td>
					<td><%# ((IProductSummary)Container.DataItem).Name %></td>
				    <td><a href="ProductEdit.aspx<%#GetBaseQueryString()%>&pid=<%#((IProductSummary)Container.DataItem).ProductID%>">Edit Product</a></td>
				    <td><a href="ProductDelete.aspx<%#GetBaseQueryString()%>&pid=<%#((IProductSummary)Container.DataItem).ProductID%>">Delete Product</a></td>
				    <td><a href="ProductMove.aspx<%#GetBaseQueryString()%>&pid=<%#((IProductSummary)Container.DataItem).ProductID%>&direction=Up"><img src="<%=Cuyahoga.Web.Util.UrlHelper.GetSiteUrl()%>/admin/images/arrow_up.png" border="0"/></a> <a href="ProductMove.aspx<%#GetBaseQueryString()%>&pid=<%#((IProductSummary)Container.DataItem).ProductID%>&direction=Down"><img src="<%=Cuyahoga.Web.Util.UrlHelper.GetSiteUrl()%>/admin/images/arrow_down.png" border="0"/></a></td>
				</tr>
			</itemtemplate>
			<footertemplate>
				</tbody>
			</table><br/>
			<%# (ShowAddProduct) ? ("<a href=\"ProductEdit.aspx" + GetBaseQueryString() + "&cat=" + CatID + "\">Add new product</a>") : ""%>		
			</footertemplate>
			</asp:repeater>
   </form>
   </body>
</html>