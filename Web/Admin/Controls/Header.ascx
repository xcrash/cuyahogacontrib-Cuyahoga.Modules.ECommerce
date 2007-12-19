<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Header.ascx.cs" Inherits="Cuyahoga.Web.Admin.Controls.Header" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import Namespace="Cuyahoga.Web.Util" %>
<div id="header">
    <div id="header-content">
        <h1 id="logo"><a href="default.aspx" title="">igentics<span class="gray">CMS</span></a></h1>
        <h2 id="slogan">websites made simple</h2>
        <div class="menu">
            <!-- Menu Tabs -->
            <ul>
                <li><asp:HyperLink ID="hplHome" runat="server"></asp:HyperLink></li>
                <li><asp:Literal ID="litCms" runat="server"></asp:Literal>CMS<!--[if IE 7]><!--></a><!--<![endif]-->
                <!--[if lte IE 6]><table><tr><td><![endif]-->
                <ul>
                    <li><a href="<%=UrlHelper.GetSiteUrl()%>/admin/Templates.aspx"> Manage Templates</a></li>
                    <li><a href="<%=UrlHelper.GetSiteUrl()%>/admin/SiteEdit.aspx?SiteId=-1">Add New Site</a></li>
                    <li><a href="<%=UrlHelper.GetSiteUrl()%>/admin/Sections.aspx">Manage Standalone Sections</a></li>
                </ul>
                <!--[if lte IE 6]></td></tr></table></a><![endif]-->
                </li>

                <li><asp:Literal ID="litConfig" runat="server"></asp:Literal>Configuration<!--[if IE 7]><!--></a><!--<![endif]-->
                <!--[if lte IE 6]><table><tr><td><![endif]-->
                <ul>
                    <li><a href="<%=UrlHelper.GetSiteUrl()%>/admin/Modules.aspx">Modules</a></li>
                    <li><a href="<%=UrlHelper.GetSiteUrl()%>/admin/Users.aspx" >Manage Users</a></li>
                    <li><a href="<%=UrlHelper.GetSiteUrl()%>/admin/Roles.aspx">Manage Roles</a></li>
                    <li><a href="<%=UrlHelper.GetSiteUrl()%>/admin/RebuildIndex.aspx" >Rebuild Search Index</a></li>
                </ul>
                <!--[if lte IE 6]></td></tr></table></a><![endif]--></li>

                <li><asp:Literal ID="litECommerce" runat="server"></asp:Literal>ECommerce<!--[if IE 7]><!--></a><!--<![endif]-->
                <!--[if lte IE 6]><table><tr><td><![endif]-->
                <ul>
                    <li><a href="<%=UrlHelper.GetSiteUrl()%>/Modules/ECommerce/Admin/Catalogue/CatalogueBrowser.aspx?NodeId=19&SectionId=41">Catalogue Manager</a></li>
                    <li><a href="<%=UrlHelper.GetSiteUrl()%>/Modules/ECommerce/Admin/DeliveryManager.aspx?NodeId=19&SectionId=41" >Delivery Manager</a></li>
                    <li><a href="<%=UrlHelper.GetSiteUrl()%>/Modules/ECommerce/Admin/Orders/Orders.aspx?NodeId=19&SectionId=41" >Order Manager</a></li>
                </ul>
                <!--[if lte IE 6]></td></tr></table></a><![endif]--></li>        			
            </ul>
        </div>
    </div>
</div>