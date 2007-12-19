<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentEditor.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Controls.DocumentEditor" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce" %><%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<h2 class="tab">Documents</h2>
<asp:PlaceHolder ID="plhDocuments" runat="server"></asp:PlaceHolder>

<asp:Repeater ID="rptDocuments" runat="server" EnableViewState="true">
<headertemplate><h3>Current Documents</h3><table><tr><th>Document name</th><th>Delete? </th></tr></headertemplate>
			      <itemtemplate>
			      <tr><asp:TextBox ID="txtDocumentID" runat="server" Text="<%#((IRelatedDocument)Container.DataItem).DocumentID%>" visible="false"/>
			        <td><%#((Cuyahoga.Modules.ECommerce.Domain.Catalogue.RelatedDocument)Container.DataItem).Name%></td>
			        
			        			      <td>
							         <asp:CheckBox ID="chkRemove" EnableViewState="true"  runat="server" />
			     </td>
			        </td>
			      </tr>
			      </itemtemplate>
			      <footertemplate>
			      </table>
			      </footertemplate>
</asp:Repeater>
<p>To attach a new document to this product,you must upload it using the document manager.</p>
<asp:Repeater ID="rptNewDocuments" runat="server" EnableViewState="true">
<headertemplate><h3>New Documents</h3><table><tr><th>Document name</th><th>Add? </th></tr>
</headertemplate>
			      <itemtemplate>
			      <tr><asp:TextBox ID="txtDocumentID" runat="server" Text="<%#((Document)Container.DataItem).DocumentID%>" visible="false"/>
			        <td><%#((Document)Container.DataItem).DocumentName%></td>
			        
			        			      <td>
							         <asp:CheckBox ID="chkAdd" EnableViewState="true"  runat="server" />
			     </td>
			        </td>
			      </tr>
			      </itemtemplate>
			      <footertemplate>
			      </table>
			      </footertemplate>
</asp:Repeater>
