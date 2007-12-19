<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Kits.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Albion.Web.Views.Kits" %>
<%@ Register tagPrefix="lc" Tagname="breadcrumb" src="..\Controls\BreadCrumbTrail.ascx"%>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain" %>
<div id="onecolumn"><h2>Kits</h2><asp:Repeater ID="rptKits" runat="server">
<ItemTemplate>
<div class="float">
<asp:HyperLink runat="server" ID="imgKit" /><br />
<p><asp:HyperLink ID="hplKit" runat="server" Width="150px" Height="150px" /></p>
</div>
</ItemTemplate>
</asp:Repeater>
</div>