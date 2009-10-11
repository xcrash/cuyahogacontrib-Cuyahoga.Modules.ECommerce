<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BasketSummary.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Views.BasketSummary" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util.Interfaces" %>
<div class="head">
        <h3 class="headerBackGround">Basket</h3>
    </div>
    <div class="sideBox">
<p><a href="<%=AppPath%>Basket.aspx">View Basket</a>:</p>
<asp:Panel ID="pnlDetails" runat="server" Visible="false">
<p>Total items: <%=ItemCount%></p>
<p>Total price: <asp:literal id="litBasketTotal" runat="server" /></p>
</asp:Panel>
<asp:Panel ID="pnlEmpty" runat="server"><p>Your shopping basket is empty</p></asp:Panel>
</div>