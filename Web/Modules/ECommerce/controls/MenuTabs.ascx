<%@ Control Language="c#" AutoEventWireup="false" Codebehind="MenuTabs.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.MenuTabs" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<ul id="secnav">
			
			        
			<asp:repeater runat="server" id="rptMenu">
					<headertemplate></headertemplate>
					<itemtemplate> 
<%#(CurrentNode == ((ICatalogueNode)Container.DataItem).NodeID) ? ("<li id=\"" + ((ICatalogueNode)Container.DataItem).CssClass + "\"><a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICatalogueNode) Container.DataItem)  + "\" title=\"" + ((ICatalogueNode)Container.DataItem).Name + "\" class=\"active\">"  + ((ICatalogueNode)Container.DataItem).Name +  SetNode() + SetHadSubCategories(true) + "</a><ul>") : "" %>					
		
<%#(NodeHasNotBeenSet() && CurrentNode == ((ICatalogueNode)Container.DataItem).ParentNodeID && RealNode != ((ICatalogueNode)Container.DataItem).NodeID &&  CurrentNode != ((ICatalogueNode)Container.DataItem).NodeID) ? ("<li><a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICatalogueNode) Container.DataItem) + "\" title=\"" + ((ICatalogueNode)Container.DataItem).Name + "\">" +  SetHadSubCategories(true) +  ((ICatalogueNode)Container.DataItem).Name + SetNode() + "</a></li>") : "" %>

<%#(NodeHasNotBeenSet() && CurrentNode == ((ICatalogueNode)Container.DataItem).ParentNodeID && RealNode == ((ICatalogueNode)Container.DataItem).NodeID && CurrentNode != ((ICatalogueNode)Container.DataItem).NodeID) ? ("<li><a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICatalogueNode) Container.DataItem) + "\" class=\"active\" title=\"" + ((ICatalogueNode)Container.DataItem).Name + "\">" +  SetHadSubCategories(true) +  ((ICatalogueNode)Container.DataItem).Name + SetNode() + "</a></li>") : "" %>


<%#(NodeHasNotBeenSet() && CurrentNode != ((ICatalogueNode)Container.DataItem).ParentNodeID &&  CurrentNode != ((ICatalogueNode)Container.DataItem).NodeID && PreviousNode != ((ICatalogueNode)Container.DataItem).ParentNodeID  && !IsFirstNode()) ? ("</ul></li><li id=\"" + ((ICatalogueNode)Container.DataItem).CssClass + "\"><a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICatalogueNode) Container.DataItem) + SetNode()  + "\" title=\"" + ((ICatalogueNode)Container.DataItem).Name + "\">" + ((ICatalogueNode)Container.DataItem).Name +  SetHadSubCategories(false)  + "</a></li>") : "" %>

<%#(NodeHasNotBeenSet()) ? ("<li id=\"" + ((ICatalogueNode)Container.DataItem).CssClass + "\"><a href=\"" + UrlHelper.GetCatalogueNodeUrl((ICatalogueNode) Container.DataItem) + "\" title=\"" + ((ICatalogueNode)Container.DataItem).Name + "\">" + ((ICatalogueNode)Container.DataItem).Name + SetHadSubCategories(false) + "</a></li>") : "" %>



<%# SetPrevious((ICatalogueNode) Container.DataItem) %>
</itemtemplate>


					<footertemplate>
					<%#(HadSubCategories()) ? ("</ul></li>") : "" %>
					<li id="menutermspace"><a href="http://www.termspace.com/" title="Specialist Accommodation">Specialist Accommodation</a></li>
					<li id="menuspecials"><a href="/specials.aspx" title="Special Promotions">Special Promotions</a></li>
					</footertemplate>
			</asp:repeater>
	
			
			
			
			

			
</ul>
	



			<img src="http://lc002/mj/Templates/MJHire/Images/one_stop_hire.gif" alt="One Stop Hire" style="margin-left: 68px;"/>
	

