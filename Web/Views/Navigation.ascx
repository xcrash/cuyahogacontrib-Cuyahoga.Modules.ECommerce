<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigation.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Views.Navigatyion" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>


			
			        
			<asp:repeater runat="server" id="rptMenu">
					<headertemplate></headertemplate>
					<itemtemplate> 
<li id="<%#((ICatalogueNode)Container.DataItem).CssClass %>"><a href="/6/section.aspx<%# UrlHelper.GetMenuUrl(((ICatalogueNode) Container.DataItem).NodeID)%> " title=" <%# ((ICatalogueNode)Container.DataItem).Name %>"> <%# ((ICatalogueNode)Container.DataItem).Name %></a></li>			
		

</itemtemplate>


					<footertemplate>
					
					</footertemplate>
			</asp:repeater>
	
			
			
			
			

			
