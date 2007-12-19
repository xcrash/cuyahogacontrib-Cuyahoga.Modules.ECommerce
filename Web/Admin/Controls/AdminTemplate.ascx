<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AdminTemplate.ascx.cs" Inherits="Cuyahoga.Web.Admin.Controls.AdminTemplate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Navigation" Src="Navigation.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title><asp:literal id="PageTitle" runat="server"></asp:literal></title>
		<link id="CssStyleSheet" rel="stylesheet" type="text/css" runat="server" />
		<script type="text/javascript" src="tabber.js"></script>
	</head>
	<body>
		<form id="Frm" method="post" runat="server" enctype="multipart/form-data">
		<div id="wrap">
			<uc1:header id="Header" runat="server"></uc1:header>

<!-- content-wrap starts here -->
	<div id="content-wrap"><div id="content">

			<div id="sidebar" >
            <uc1:Navigation id="Nav" runat="server"></uc1:navigation>
            <div class="sideboxHeader"><h1 class="clear">Useful links</h1></div>
            <div class="sidebox">
                <ul class="sidemenu">
	                <li><asp:hyperlink id="hplSite" CssClass="top" runat="server">View the current site</asp:hyperlink>    </li>
	                <li><a href="Documentation.aspx">Documentation</a></li>
	                <li><a href="http://support.igentics.com">Support Website</a></li>
	                <li><a href="Design.aspx">Design Guidlines</a></li>
	                <li><asp:linkbutton id="lbtLogout" runat="server">Log out</asp:linkbutton></li>
                </ul>

			</div>
			<div class="sideboxHeader"><h1>Help</h1></div>
				<div class="sidebox">
					<p>Help information will be injected into this div </p>
				</div>
		</div>

		<div id="main">
           	<h1><asp:literal id="PageTitleLabel" runat="server" /></h1>
			<div class="post">		
				<div id="MessageBox" class="messagebox" runat="server" visible="false" enableviewstate="false"></div>
				<asp:placeholder id="PageContent" runat="server"></asp:placeholder>				
			</div>
		</div>

	<!-- content-wrap ends here -->
	</div></div>

<!-- footer starts here -->
<div id="footer">

    <div id="footer-content">
		<div class="col2 float-right">
		<p>
		&copy; copyright <%=DateTime.Now.Year%> <strong>Igentics</strong></p>
		</div>

    </div>  </div>
<!-- footer ends here -->

<!-- wrap ends here -->
</div>

		</form>

	</body>
</html>