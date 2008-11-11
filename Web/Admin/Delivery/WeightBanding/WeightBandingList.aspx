<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeightBandingList.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Delivery.WeightBanding.WeightBandingList" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
     <p>
 <asp:HyperLink ID="hplDeliveryMainMenu" runat="server" Text="Delivery home page"></asp:HyperLink> &raquo;
 <asp:HyperLink ID="hplWeightBandingHomePage" runat="server" Text="Weight banding home page"></asp:HyperLink></p>
    <h1>Weight Banding List</h1>
    <div class="post">
        <p>Here you can view, add, delete and edit the weight bandings on a country and state basis.</p>
    </div>
    <div class="post">
   <p> <asp:DropDownList ID="ddlCountryList" runat="server">
    </asp:DropDownList>
    <asp:LinkButton ID="lbShowCountry" runat="server" Text="Select country to view"></asp:LinkButton></p>
    <asp:LinkButton ID="lbStateBandings" runat="server" Text="Show State Bandings"></asp:LinkButton>
 <asp:Repeater ID="rptCountryBandings" runat="server">
    <HeaderTemplate>
        <table>
        <tr>
            <th>Weight Band</th><th>Price</th><th></th><th></th>
        </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
           <td> <%#((CountryDeliveryWeight)Container.DataItem).WeightLevel%></td>  <td> <%#((CountryDeliveryWeight)Container.DataItem).Price%></td><td><asp:LinkButton ID="lbEditCountryBand"  CommandArgument="<%#((CountryDeliveryWeight)Container.DataItem).WeightLevel%>" CommandName="<%#((CountryDeliveryWeight)Container.DataItem).CountryCode%>" runat="server" Text="Edit"></asp:LinkButton></td>
           <td><asp:LinkButton  OnClientClick="javascript:if(!confirm('Delete this delivery band?'))return false;" ID="lbDeleteCountryBand"  CommandArgument="<%#((CountryDeliveryWeight)Container.DataItem).WeightLevel%>" CommandName="<%#((CountryDeliveryWeight)Container.DataItem).CountryCode%>" runat="server" Text="Delete"></asp:LinkButton></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
 </asp:Repeater>
<p> <asp:Label ID="lblDelete" runat="server"></asp:Label></p>
 <asp:Label ID="lblCountryHeading" runat="server"></asp:Label>
 
  <asp:Repeater ID="rptStateWeightBandings" runat="server">
    <HeaderTemplate>
        <table>
        <tr>
            <th>Weight Band</th><th>Price</th><th></th>
        </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
           <td> <%#((CountryDeliveryWeight)Container.DataItem).WeightLevel%></td>  <td> <%#((CountryDeliveryWeight)Container.DataItem).Price%></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
 </asp:Repeater>
 </div>
 <p><asp:HyperLink ID="hplAdd" runat="server" Text="Add weight band"></asp:HyperLink></p>

    </form>
</body>
</html>
