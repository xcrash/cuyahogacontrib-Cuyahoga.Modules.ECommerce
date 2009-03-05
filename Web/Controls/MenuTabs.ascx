<%@ Control Language="c#" AutoEventWireup="false" Codebehind="MenuTabs.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.MenuTabs" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<ul id="secnav">
			
			        
			<asp:repeater runat="server" id="rptMenu">
					<headertemplate></headertemplate>
					<itemtemplate> 
<%#(CurrentNode == ((ICategory)Container.DataItem).NodeID) ? ("<li id=\"" + ((ICategory)Container.DataItem).CssClass + "\"><a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICategory) Container.DataItem)  + "\" title=\"" + ((ICategory)Container.DataItem).Name + "\" class=\"active\">"  + ((ICategory)Container.DataItem).Name +  SetNode() + SetHadSubCategories(true) + "</a><ul>") : "" %>					
		
<%#(NodeHasNotBeenSet() && CurrentNode == ((ICategory)Container.DataItem).ParentNodeID && RealNode != ((ICategory)Container.DataItem).NodeID &&  CurrentNode != ((ICategory)Container.DataItem).NodeID) ? ("<li><a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICategory) Container.DataItem) + "\" title=\"" + ((ICategory)Container.DataItem).Name + "\">" +  SetHadSubCategories(true) +  ((ICategory)Container.DataItem).Name + SetNode() + "</a></li>") : "" %>

<%#(NodeHasNotBeenSet() && CurrentNode == ((ICategory)Container.DataItem).ParentNodeID && RealNode == ((ICategory)Container.DataItem).NodeID && CurrentNode != ((ICategory)Container.DataItem).NodeID) ? ("<li><a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICategory) Container.DataItem) + "\" class=\"active\" title=\"" + ((ICategory)Container.DataItem).Name + "\">" +  SetHadSubCategories(true) +  ((ICategory)Container.DataItem).Name + SetNode() + "</a></li>") : "" %>


<%#(NodeHasNotBeenSet() && CurrentNode != ((ICategory)Container.DataItem).ParentNodeID &&  CurrentNode != ((ICategory)Container.DataItem).NodeID && PreviousNode != ((ICategory)Container.DataItem).ParentNodeID  && !IsFirstNode()) ? ("</ul></li><li id=\"" + ((ICategory)Container.DataItem).CssClass + "\"><a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICategory) Container.DataItem) + SetNode()  + "\" title=\"" + ((ICategory)Container.DataItem).Name + "\">" + ((ICategory)Container.DataItem).Name +  SetHadSubCategories(false)  + "</a></li>") : "" %>

<%#(NodeHasNotBeenSet()) ? ("<li id=\"" + ((ICategory)Container.DataItem).CssClass + "\"><a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICategory) Container.DataItem) + "\" title=\"" + ((ICategory)Container.DataItem).Name + "\">" + ((ICategory)Container.DataItem).Name + SetHadSubCategories(false) + "</a></li>") : "" %>



<%# SetPrevious((ICategory) Container.DataItem) %>
</itemtemplate>


					<footertemplate>
					<%#(HadSubCategories()) ? ("</ul></li>") : "" %>
					<li id="menutermspace"><a href="http://www.termspace.com/" title="Specialist Accommodation">Specialist Accommodation</a></li>
					<li id="menuspecials"><a href="/specials.aspx" title="Special Promotions">Special Promotions</a></li>
					</footertemplate>
			</asp:repeater>
	
			
			
			
			

			
</ul>
	



			<img src="http://lc002/mj/Templates/MJHire/Images/one_stop_hire.gif" alt="One Stop Hire" style="margin-left: 68px;"/>
	

