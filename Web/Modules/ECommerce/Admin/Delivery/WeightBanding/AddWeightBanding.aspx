<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddWeightBanding.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Delivery.WeightBanding.AddWeightBanding" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
  <p>
 <asp:HyperLink ID="hplDeliveryMainMenu" runat="server" Text="Delivery home page"></asp:HyperLink> &raquo;
 <asp:HyperLink ID="hplWeightBandingHomePage" runat="server" Text="Weight banding home page"></asp:HyperLink>
 &raquo;
 <asp:HyperLink ID="hplWeightBandingList" runat="server" Text="Weight banding list"></asp:HyperLink>
 </p>
    <h1>Add Delivery Band</h1>
    <div class="post">
     <p>   Here you can add a new  weight band  for the <asp:Label ID="lblCountry" runat="server"></asp:Label> market.</p>
    </div>
  
   <asp:PlaceHolder ID="plhAdd" runat="server">
    <table>
    
    <tr>
        <td>Price:</td><td><asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td>
    </tr>
    
        <tr>
        <td>Weight:</td><td><asp:TextBox ID="txtWeight" runat="server"></asp:TextBox></td>
    </tr>

   <tr><td><asp:LinkButton ID="lbSave" runat="server" Text="Save"></asp:LinkButton></td></tr>
   </table>
   <br />
   </asp:PlaceHolder>
    <p><asp:Label ID="lblMessage" runat="server"></asp:Label></p>
    </form>
</body>
</html>
