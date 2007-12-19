<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentManager.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.DocumentManager" %>
<%@ Register TagPrefix="nav" TagName="menu" Src="Controls/Navigation.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
 
     <form id="form2" runat="server">
    <div>
    <nav:menu runat="server"/>
    <div id="contentpane">
    <div class="group">
		
		
	<h3>Upload a new document</h3>
    <asp:FileUpload ID="uploadControl" runat="server" /><br/> <asp:LinkButton ID="lnkUpload" Text="Upload" runat="server"></asp:LinkButton><br/>
    
 <asp:Label Id="lblSaveNew" runat="server"/>
   <hr/>
	<h3>Existing Documents</h3>
	
	<asp:repeater runat="server" id="rptDocuments"  EnableViewState="true">
			<headertemplate>
				<table>
				<tr><th> Name</th><th>Delete?</th></tr>
			</headertemplate>
			<itemtemplate>
			<asp:TextBox ID="txtDocumentID" runat="server" EnableViewState="true" text="<%#((Document) Container.DataItem).DocumentID%>" Visible="false" />
			
					<tr>
					<td><a href="<%#((Document) Container.DataItem).FilePath%>">
					<%#((Document) Container.DataItem).DocumentName%>
					</a> 
					</td>
					<td>
					<asp:CheckBox ID="chkDelete" EnableViewState="true" runat="server" />
					</td>
					</tr>
			</itemtemplate>
			<footertemplate>
				
				</table>
			</footertemplate>
	</asp:repeater>
	
	<br/><br/>
	<asp:LinkButton ID="lnkSave" text="Save Changes" runat="server" />
	<br/>
	 <asp:Label Id="lblSave" runat="server"/>
	</div>
   
    </div>
    </form>
</body>
</html>
