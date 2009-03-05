<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigureWeightBanding.aspx.cs" Inherits=" Cuyahoga.Modules.ECommerce.Web.Admin.Delivery.ConfigureWeightBanding" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
 <asp:HyperLink ID="hplDeliveryMainMenu" runat="server" Text="Delivery home page"></asp:HyperLink></p>
        <h1>Weight banding based delivery configuration</h1>
        <div class="post">
            From here you can configure every aspect of weight banding based delivery.
        </div>
        
        <table id="weightBanding">
        <tr>
        <th>Delivery Method</th><th>Status</th><th></th>
        </tr>
        <tr>
            <td>Country based weight banding</td><td><asp:Image ID="imgCountryStatus" runat="server" /></td><td><asp:LinkButton ID="lbCountryStatus" runat="Server"></asp:LinkButton></td>
         </tr>
              <tr>
                 <td>State / county based weight banding</td><td><asp:Image ID="imgCountyStatus" runat="server" /></td><td><asp:LinkButton ID="lbCountyStatus" runat="Server"></asp:LinkButton></td>
     
              </tr>
       </table>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>    
               
        
        
 

			<p>You can also   <asp:HyperLink ID="hplView" runat="server" Text="view and edit"></asp:HyperLink> the weight bandings.</p>

    </form>
</body>
</html>
