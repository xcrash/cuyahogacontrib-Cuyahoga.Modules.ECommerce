<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageEditor.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Controls.ImageEditor"  %>
<%@ Register TagPrefix="guild"  Namespace="Guild.WebControls" %>
<h2 class="tab">Images</h2>
<table>
<tr><th>Image</th><th>Remove?</th></tr>
<asp:PlaceHolder ID="plhImages" runat="server" />
<asp:PlaceHolder ID="plhImagesAdditions" runat="server" EnableViewState="true" />
</table>
<asp:LinkButton id="lnkAddImages" runat="server"  text="Add Image"/> 
<table>
<tr><th>Large Image</th><th>Remove?</th></tr>
<asp:PlaceHolder ID="plhLargeImages" runat="server" />
<asp:PlaceHolder ID="plhLargeImagesAdditions" runat="server" EnableViewState="true" />
</table>
 <asp:LinkButton id="lnkAddLargeImages" runat="server"  text="Add Large Image"/>