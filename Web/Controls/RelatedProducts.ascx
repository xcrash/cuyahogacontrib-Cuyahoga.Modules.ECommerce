<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelatedProducts.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.RelatedProducts" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue" %>

			<asp:repeater runat="server" id="rptUpSell">
			    <headertemplate>
					    <ul>
			    </headertemplate>
			    <itemtemplate>
					    <li>
					        <a href="ss">aaaaaaa</a>
					    </li>
			    </itemtemplate>
			    <footertemplate>
			    </ul>
			    </footertemplate>
			</asp:repeater>
			
			<asp:repeater runat="server" id="rptCrossSell">
			    <headertemplate>
					    <ul>
			    </headertemplate>
			    <itemtemplate>
					    <li>
					    <a href="<%# UrlHelper.GetProductUrl(((IRelatedProducts)Container.DataItem).AccessoryPartNo)%>"><%#((IRelatedProducts)Container.DataItem).AccessoryName%></a>
					    </li>
			    </itemtemplate>
			    <footertemplate>
			    </ul>
			    </footertemplate>
			</asp:repeater>
			