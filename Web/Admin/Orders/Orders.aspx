<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Orders" %>
<%@ Register TagPrefix="ctl" Namespace="Koutny.WebControls" Assembly="Cuyahoga.Modules.ECommerce" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
   <form id="form1" runat="server">
 
        <h1>Order Manager</h1>
        
           <div class="post">
          <p>  In this section, you can access all customer order related functions. These include the ability to review, edit, delete and search all orders</p>
           </div>
	           			<asp:Repeater ID="repItems" Runat="server">
				<HeaderTemplate>
					<table border="1" width="200">
						<tr>
							<th>Ordered Date</th>
						    <th>Order Reference</th>
						    <th>Email Address</th>
						    <th>Company Name</th>
						</tr>
				</HeaderTemplate>
				<ItemTemplate>
					<tr>
						<td><%# ((Basket)Container.DataItem).CreatedDate.ToString("d")%></td>
						<td><a href="OrderDetail.aspx<%#GetBaseQueryString()%>&orderID=<%# ((Basket)Container.DataItem).OrderHeader.OrderHeaderID%>"><%# ((Basket)Container.DataItem).OrderHeader.OrderHeaderID%></a></td>
						<td><%#((((Basket)Container.DataItem).UserDetails != null) ? new UserDecorator(((Basket)Container.DataItem).UserDetails) : ((Basket)Container.DataItem).AltUserDetails).EmailAddress %></td>
					    <td><%#(((Basket)Container.DataItem).AltUserDetails).CompanyName%></td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
			<tr>
				<td colspan="3" align="center">
					<ctl:RepeaterPager ID="repItemsPager" runat="server" RepeaterID="repItems" PageSize="50" OnPageIndexChanged="PageChanged" PagingType="VirtualItems">
						<NUMERICPAGERTEMPLATE>
							<asp:LinkButton id=lbNumericPager Runat="server" Text="<%# Container.Value %>" CommandName="Page">
							</asp:LinkButton>
						</NUMERICPAGERTEMPLATE>
						<SELECTEDNUMERICPAGERTEMPLATE>
							<%# Container.Value %>
						</SELECTEDNUMERICPAGERTEMPLATE>
						<EMPTYPAGERTEMPLATE>
							<asp:LinkButton id="lbEmptyPager" Runat="server" Text="..." CommandName="Page"></asp:LinkButton>
						</EMPTYPAGERTEMPLATE>
					</ctl:RepeaterPager>
				</td>
			</tr>
			</table>
		   
    </form>
</body>
</html>
