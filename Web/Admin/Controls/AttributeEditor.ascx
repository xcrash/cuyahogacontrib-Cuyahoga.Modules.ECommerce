<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttributeEditor.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Controls.AttributeEditor" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<%@ Register TagPrefix="Web" Namespace="Cuyahoga.Modules.ECommerce.Core" Assembly="Cuyahoga.Modules.ECommerce" %>
<h2 class="tab">Attributes</h2>
<table>
<asp:PlaceHolder ID="plhAttributeEditor" runat="server" EnableViewState="true" />
</table>