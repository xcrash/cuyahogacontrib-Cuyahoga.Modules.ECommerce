<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SynonymEditor.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Controls.SynonymEditor" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce" %><%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<h2 class="tab">Synonyms</h2>


<asp:Repeater ID="rptSynonym" runat="server" EnableViewState="true">
<headertemplate><table><tr><th>Alternative Words</th><th>Remove?</th></tr></headertemplate>
			      <itemtemplate>
			    	 <tr>
			
			        <td>
			        <asp:TextBox ID="txtAlternativePhrase" runat="server" Text="<%#((ProductSynonym)Container.DataItem).AlternativePhrase%>" />
			        </td>
			        <td>
			        <asp:CheckBox ID="chkRemove" EnableViewState="true"  runat="server" />
			        </td>
			        </tr>
			      </itemtemplate>
			      <footertemplate>
			      </table>
			      </footertemplate>
</asp:Repeater>

<asp:LinkButton ID="lnkAddSynonym" runat="server"  Text="Add Synonym"/>
<br/>
<asp:PlaceHolder ID="plhSynonymAdditions" runat="server" EnableViewState="true"></asp:PlaceHolder>
