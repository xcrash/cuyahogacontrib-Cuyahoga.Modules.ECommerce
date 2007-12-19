<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.AccountList" %>
<%@ Register TagPrefix="ctl" Namespace="Koutny.WebControls" Assembly="Cuyahoga.Modules.Ecommerce" %>

<%@ Import Namespace="Cuyahoga.Modules.ECommerce" %>
<%@ Import Namespace="Cuyahoga.Core.Domain" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
   <form id="form1" runat="server">
    

    
        <h1>Account List</h1>
        
          <div class="post">
      <p>This is a list all of your online store accounts.</p>
     </div>	
	           			<asp:Repeater ID="repItems" Runat="server">
				<HeaderTemplate>
					<table>
						<tr>
							<th>
								Username</th>
							<th>
								First Name</th>
								<th>
								Last Name</th>
								<tr>
				</HeaderTemplate>
				<ItemTemplate>
					<tr>
						<td><a href="accountEdit.aspx<%#GetBaseQueryString()%>&accountID=<%#((Cuyahoga.Core.Domain.User)Container.DataItem).Id%>"><%#((Cuyahoga.Core.Domain.User)Container.DataItem).Email%></a></td>
						<td><%#((Cuyahoga.Core.Domain.User)Container.DataItem).Email%></td>
						<td><%#((Cuyahoga.Core.Domain.User)Container.DataItem).FirstName%></td>
						<td><%#((Cuyahoga.Core.Domain.User)Container.DataItem).LastName%></td>
						
					</tr>
				</ItemTemplate>
			</asp:Repeater>
			<tr>
				<td colspan="2" align="center">
					<ctl:RepeaterPager ID="repItemsPager" runat="server" RepeaterID="repItems" PageSize="5" OnPageIndexChanged="PageChanged">
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
			</TABLE>
		   
    </form>
</body>
</html>
