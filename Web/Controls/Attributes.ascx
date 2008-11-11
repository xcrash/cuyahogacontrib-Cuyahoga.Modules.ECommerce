<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Attributes.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.Attributes" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>

<asp:repeater runat="server" id="rptCheckBoxAttributes">
	<headertemplate>				
	</headertemplate>
	<itemtemplate>	
	    <asp:Label ID="lblCheckBoxAttribute" runat="server" />
	    <asp:CheckBoxList ID="cblCheckBoxList" runat="server"> 
	    </asp:CheckBoxList>		
	</itemtemplate>
	<footertemplate>
	</footertemplate>
</asp:repeater>

<asp:repeater runat="server" id="rptTableAttributes">
	<headertemplate>				
	</headertemplate>
	<itemtemplate>	
	    <asp:Label ID="lblTableAttribute" runat="server" />
		<asp:DataList ID="dlTableAttribute" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
		    <HeaderTemplate>
		        
		    </HeaderTemplate>
		    <ItemTemplate>
		       <%#((IAttributeOption)Container.DataItem).PickListValue%>
		    </ItemTemplate>
		    <AlternatingItemTemplate>
	             <%#((IAttributeOption)Container.DataItem).PickListValue%>
    	   </AlternatingItemTemplate>
		</asp:DataList>
	</itemtemplate>
	<footertemplate>
	</footertemplate>
</asp:repeater>

<asp:repeater runat="server" id="rptLinkAttributes">
	<headertemplate>				
	</headertemplate>
	<itemtemplate>
	    <asp:HyperLink ID="hplLinkattribute" runat="server" />			
	</itemtemplate>
	<footertemplate>
	</footertemplate>
</asp:repeater>

<asp:repeater runat="server" id="rptImageAttributes">
	<headertemplate>				
	</headertemplate>
	<itemtemplate>			
	</itemtemplate>
	<footertemplate>
	</footertemplate>
</asp:repeater>
