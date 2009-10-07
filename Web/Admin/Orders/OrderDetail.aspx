<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.OrderDetail" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ecommerce" TagName="order" Src="../../Controls/OrderViewComposite.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Order Details</h1>
    <ecommerce:order runat="server" id="ctlOrderView" />
    <p><a href="Orders.aspx?NodeId=19&SectionId=41">&lt; Back</a></p>
    </form>
</body>
</html>