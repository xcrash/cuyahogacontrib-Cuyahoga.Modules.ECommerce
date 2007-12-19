<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliveryMethods.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Delivery.DeliveryMethods" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Delivery Methods</h1>
        <div class="post">
            <p>This is a list of all avaliable delivery methods.</p>
        </div>
        
        <asp:Repeater ID="rptDeliveryMethods" runat="server">
            <HeaderTemplate>
                <table id="deliveryMethods">
                <tr>
                <th>Delivery Type</th><th>Status</th><th></th>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                 <tr>
                    <td><%#((DeliveryType)Container.DataItem).Name%></td>
                    <td><asp:Image ID="imgStatus" runat="server"></asp:Image></td>
                    <td><asp:HyperLink ID="hplEdit" runat="server" Text="Edit"></asp:HyperLink></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
