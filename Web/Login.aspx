<%@ Page language="c#" Codebehind="Login.aspx.cs" AutoEventWireup="false" Inherits="Cuyahoga.Web.Admin.Login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Login</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" type="text/css" href="Admin/Css/Admin.css">
	</head>
	<body ms_positioning="FlowLayout">

		<form id="Form1" method="post" runat="server">
		

<div id="wrap">
			



<div id="header"><div id="header-content">

		<h1 id="logo"><a href="default.aspx" title="">igentics<span class="gray">CMS</span></a></h1>
		<h2 id="slogan">websites made simple</h2>




	</div></div>







		




<!-- content-wrap starts here -->
	<div id="content-wrap"><div id="content">

			<div id="sidebar" >

			
				  




		</div>

		<div id="main">
           
			
	
			
				<table width="100%">
					<tr>
						<td></td>
						<td><asp:label id="lblError" runat="server" enableviewstate="False" visible="False" cssclass="validator"></asp:label></td></tr>
					<tr>
						<td style="width:90px">User</td>
						<td><asp:textbox id="txtUsername" runat="server" width="140px"></asp:textbox></td>
					</tr>
					<tr>
						<td>Password</td>
						<td><asp:textbox id="txtPassword" runat="server" textmode="Password" width="140px"></asp:textbox></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:button id="btnLogin" runat="server" text="Login"></asp:button></td>
					</tr>
				</table>
				
		



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
