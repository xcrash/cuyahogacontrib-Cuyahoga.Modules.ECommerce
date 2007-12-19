<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpSellEditor.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Controls.UpSellEditor" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce" %><%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<h3>Up Sell Products </h3>
<asp:Repeater ID="rptUpSell" runat="server" EnableViewState="true">
<headertemplate><table><tr><th>Part Number</th><th>Remove?</th></tr></headertemplate>
			      <itemtemplate>
			    
			      <tr>
			      <td><asp:TextBox ID="txtPartNumber" runat="server" text="<%#((IRelatedProducts)Container.DataItem).AccessoryPartNo%>"/></td>
		
			      <td>
			         <asp:CheckBox ID="chkRemove"  runat="server" />
			     </td>
			      </tr>
			      </itemtemplate>
			      <footertemplate></table></footertemplate>
</asp:Repeater>
<br/><br/>
<asp:PlaceHolder ID="plhUpSellAdditions" runat="server"></asp:PlaceHolder>
<br/><br/>
<asp:LinkButton ID="lnkAddUpSell" runat="server" text="Add Up Sell"></asp:LinkButton>


</table>
