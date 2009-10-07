<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MarketSelector.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Views.MarketSelector" %>
<div class="head">
        <h3 class="headerBackGround">Store Configuration</h3>
    </div>
    <div class="sideBox">
       <label for="ddlCurrency">Select Market</label> <asp:DropDownList ID="ddlMarket" AutoPostBack="true" runat="server"></asp:DropDownList>
       <br /><label for="ddlCurrency">Select Language</label> <asp:DropDownList ID="ddlLanguage" AutoPostBack="true" runat="server"></asp:DropDownList>
       <br /><label for="ddlCurrency">Select Currency</label> <asp:DropDownList ID="ddlCurrency" AutoPostBack="true" runat="server"></asp:DropDownList>

    </div>