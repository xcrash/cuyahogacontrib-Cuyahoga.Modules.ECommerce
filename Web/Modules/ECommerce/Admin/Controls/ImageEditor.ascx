<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageEditor.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Controls.ImageEditor"  %>
<%@ Register TagPrefix="guild"  Namespace="Guild.WebControls" %>
<h2 class="tab">Images</h2>
<table>
<tr><th>Image</th><th>Remove?</th></tr>
<asp:PlaceHolder ID="plhImages" runat="server" />
<asp:PlaceHolder ID="plhImagesAdditions" runat="server" EnableViewState="true" />
</table>
<asp:LinkButton id="lnkAddImages" runat="server"  text="Add Image"/>