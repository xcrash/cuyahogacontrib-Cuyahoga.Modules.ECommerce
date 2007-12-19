<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ModuleAdminTemplate.ascx.cs" Inherits="Cuyahoga.Web.Controls.ModuleAdminTemplate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="header" Src="../Admin/Controls/Header.ascx" %>
<%@ Import Namespace="Cuyahoga.Web.Util" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title><asp:literal id="PageTitle" runat="server"></asp:literal></title>
		<link id="CssStyleSheet" rel="stylesheet" type="text/css" runat="server" />
		<script type="text/javascript" src="<%=UrlHelper.GetSiteUrl()%>/Admin/tabber.js"></script>
	</head>
	<body>
		<form id="Frm" method="post" enctype="multipart/form-data" runat="server">

	<div id="wrap">

<uc1:header id="header" runat="server"></uc1:header>

<!-- content-wrap starts here -->
	<div id="content-wrap"><div id="content">

			<div id="sidebar" >

<div class="sidebox">

	<h1><img id="p_Nav_i1" src="<%=UrlHelper.GetSiteUrl()%>/Admin/Images/home.gif" alt="Home" align="left" style="border-width:0px;" />Sites</h1>
	<div class="site"><img src="<%=UrlHelper.GetSiteUrl()%>/Admin/Images/internet.gif" alt="Site" align="left" style="border-width:0px;" /><a class="nodeLink" href="<%=UrlHelper.GetSiteUrl()%>/Admin/SiteEdit.aspx?SiteId=2">Albion (<%=UrlHelper.GetSiteUrl()%>)</a></div><div class="node" style="padding-left: 20px"><img src="<%=UrlHelper.GetSiteUrl()%>/Admin/Images/home.gif" alt="Node" align="left" style="border-width:0px;" /><a class="nodeLink" href="<%=UrlHelper.GetSiteUrl()%>/Admin/NodeEdit.aspx?NodeId=17">Home (en-GB)</a></div><br/><div class="node"><img src="<%=UrlHelper.GetSiteUrl()%>/Admin/Images/new.gif" alt="New Node" align="left" style="border-width:0px;" /><a class="nodeLink" href="<%=UrlHelper.GetSiteUrl()%>/Admin/NodeEdit.aspx?SiteId=2&amp;NodeId=-1">Create a site for a different culture</a></div><br/>
	
</div>
			
<div class="sideboxHeader"><h1 class="clear">Useful links</h1></div>

			<div class="sidebox">


				<ul class="sidemenu">

						<li>    </li>
						<li><a href="<%=UrlHelper.GetSiteUrl()%>/Admin/Documentation.aspx">Documentation</a></li>
						<li><a href="http://support.igentics.com">Support Website</a></li>
						<li><a href="<%=UrlHelper.GetSiteUrl()%>/Admin/Design.aspx">Design Guidlines</a></li>

						<li><asp:linkbutton id="lbtLogout" runat="server">Log out</asp:linkbutton></li>
					</ul>

			</div>
			<div class="sideboxHeader"><h1>Help</h1></div>
				<div class="sidebox">

					<p>Help information will be injected into this div </p>

				</div>


		</div>

		<div id="main">
 

        	
			<div id="MessageBox" class="messagebox" runat="server" visible="false" enableviewstate="false"></div>
			<asp:placeholder id="PageContent" runat="server"></asp:placeholder>



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