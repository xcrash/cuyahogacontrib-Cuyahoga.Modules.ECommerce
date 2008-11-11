<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountManager.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Accounts.AccountManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Account Manager</h1>
    <div class="post">
      <p>From here you can update, add and search all of your online store accounts.</p>
     </div>	
     
    		<p>	<asp:HyperLink ID="hplListAccount" runat="server" Text="List Accounts"></asp:HyperLink></p>	
			<p>	<asp:HyperLink ID="hplSearchAccount" runat="server" Text="Search Accounts"></asp:HyperLink></p>	
			<p>	<asp:HyperLink ID="hplAddAccount" runat="server" Text="Add Account"></asp:HyperLink></p>	
    </form>
</body>
</html>
