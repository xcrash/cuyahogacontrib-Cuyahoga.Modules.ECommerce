<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration.aspx.cs" Inherits="Cuyahoga.Web.Admin.Administration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
     
<div class="navsection">
	<h3>
		<asp:image imageurl="../Images/modules.gif" runat="server" imagealign="left" id="i3" alternatetext="Modules"></asp:image>
		Modules</h3>
	<asp:hyperlink id="hplModules" navigateurl="../Modules.aspx" runat="server">Manage modules</asp:hyperlink>
</div>
<br/>
<div class="navsection">
	<h3>
		<asp:image imageurl="../Images/docs.gif" runat="server" imagealign="left" id="i4" alternatetext="Templates"></asp:image>
		Templates</h3>
	<asp:hyperlink id="hplTemplates" navigateurl="../Templates.aspx" runat="server">Manage templates</asp:hyperlink>
</div>
<br/>
<div class="navsection">
	<h3>
		<asp:image imageurl="../Images/user.gif" runat="server" imagealign="left" id="i5" alternatetext="Users"></asp:image>
		Users
	</h3>
	<asp:hyperlink id="hplUsers" navigateurl="../Users.aspx" runat="server">Manage users</asp:hyperlink>
</div>
<br/>
<div class="navsection">
	<h3>
		<asp:image imageurl="../Images/users.gif" runat="server" imagealign="left" id="i6" alternatetext="Roles"></asp:image>
		Roles
	</h3>
	<asp:hyperlink id="hplRoles" navigateurl="../Roles.aspx" runat="server">Manage roles</asp:hyperlink>
</div>
<br/>
<div class="navsection">
	<h3>
		<asp:image imageurl="../Images/search.gif" runat="server" imagealign="left" id="i7" alternatetext="FullText index"></asp:image>
		Search
	</h3>
	<asp:hyperlink id="hplRebuild" navigateurl="../RebuildIndex.aspx" runat="server">Rebuild fulltext index</asp:hyperlink>
</div>

    </form>
</body>
</html>
