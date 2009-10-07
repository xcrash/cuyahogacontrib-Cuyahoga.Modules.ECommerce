<%@ Control Language="c#" AutoEventWireup="false" Codebehind="OrderViewComposite.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.OrderViewComposite" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="ecommerce" TagName="AddressView" Src="AddressView.ascx" %>
<%@ Register TagPrefix="ecommerce" TagName="OrderView" Src="OrderView.ascx" %>
<%@ Register TagPrefix="ecommerce" TagName="UserView" Src="UserDetailsView.ascx" %>
<div id="leftOrderDetails">
<ecommerce:userview runat="server" id="ctlUserView" />
<ecommerce:addressview runat="server" id="ctlAddressView" /></div>
<ecommerce:orderview runat="server" id="ctlOrderView" />