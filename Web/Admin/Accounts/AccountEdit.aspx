<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountEdit.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.AccountEdit" %>
<%@ Register TagPrefix="address" TagName="edit" Src="../Controls/AddressEdit.ascx" %>
<%@ Register TagPrefix="user" TagName="e" Src="../Controls/UserDetailsEdit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
 <form id="form1" runat="server">
 <h1>Account Edit</h1>
    <div class="post">
      <p>From here you edit an online store account.</p>
     </div>	

    <table>
    <address:edit id="addressEditor" runat="server"></address:edit>
    <user:e id="userDetailsEditor" runat="server"></user:e>
 </table>
        
<asp:LinkButton ID="lnkSave" text="save" runat="server" />
    <asp:Label ID="lblSaveMessage" runat="server"/>	
    </form>
</body>
</html>
