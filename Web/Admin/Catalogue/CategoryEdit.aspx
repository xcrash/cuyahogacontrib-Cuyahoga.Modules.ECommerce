<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryEdit.aspx.cs" ValidateRequest="false" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.CategoryEdit" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Util" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<%@ Register TagPrefix="breadcrumb" TagName="menu" Src="../Controls/BreadCrumb.ascx" %>
<%@ Register TagPrefix="guild"  Namespace="Guild.WebControls" Assembly="Cuyahoga.Modules.Ecommerce" %>
<%@ Register TagPrefix="fckeditorv2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
  <form id="form1" runat="server">
     
     <h1>Category Editor</h1>
         
       <div class="post">
            <p>In this section you can edit a category in your catalogue. You can also add links to other pages in your website, to show up at he bottom of the category. You may also attach a kit to the category.</p>
        </div>
        <breadcrumb:menu runat="server" id="ctlBreadCrumb" SetLinkOnCurrentNode="false"/>
			<div class="tabber">
			<div class="tabbertab">
			<h2>Main</h2>       
            <table>          
                <tr>
                     <td><asp:Label ID="lblName" runat="server" Text="Category name"></asp:Label> </td>
                     <td>
                     <asp:TextBox ID="txtCategoryName" runat="server" Columns="60" />
                     <asp:RequiredFieldValidator ID="rfvName" runat="server" Enabled="true" ControlToValidate="txtCategoryName" Text="Please enter a valid Category Name"></asp:RequiredFieldValidator>
                     </td>
                </tr>   
                <tr>
                    <td><asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label></td>
                    <td>
                         <fckeditorv2:fckeditor id="fckDescription"  runat="server" height="150px" width="650px" ></fckeditorv2:fckeditor>
                        <asp:RequiredFieldValidator ID="rfvDesctiption" runat="server" Enabled="true" ControlToValidate="fckDescription" Text="Please enter a valid description"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
            	    <td><asp:Label ID="lblImage" runat="server" Text="Image"></asp:Label></td>
 		            <td>
 		               <guild:WebImageMaker id="wim" runat="server" 
                            CancelButtonText="Cancel" ConfirmButtonText="OK" UploadButtonText="Upload"
                            ImageWidth="130" ImageHeight="*"
                            ImageUrl="<%#catImageUrl %>"
                            Format="jpg" Quality="Medium" HandlerPath="~/WebImageMakerHandler.ashx"  />
         		    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblCss" runat="server" Text="Css Class"></asp:Label></td>
                    <td><asp:TextBox ID="txtCss" runat="server" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblPriceChangePercent" runat="server" Text="Price Change %"></asp:Label></td>
                    <td><asp:TextBox ID="txtPriceChangePercent" runat="server" /></td>
                </tr>
             </table>
             
             
             <asp:Label ID="lblSavingMessage" runat="server"></asp:Label>
             <asp:LinkButton ID="lnkSave" runat="server" Text="save"></asp:LinkButton>
            </div>

			<div class="tabbertab">             
             <h2>Links</h2>
            <div class="post">					
                    <table class="tbl">
                        <tr>
                            <th>Title</th>
                            <th>Page</th>
                            <th>Image</th>
                            <th>&nbsp;</th>
                        </tr>
                        <asp:Repeater ID="rptPages" runat="server">
                        <ItemTemplate>
                        <tr>
                            <td><%# ((CategoryLink) Container.DataItem).Title %></td>
                            <td><%# ((CategoryLink) Container.DataItem).Node.Title %></td>
                            <td><img src="<%# WebHelper.GetImagePathWeb() + ((CategoryLink)Container.DataItem).ImageUrl%>" /></td>
							<td><asp:linkbutton id="lbtRemove" runat="server" causesvalidation="False" commandname="Remove" commandargument='<%# DataBinder.Eval(Container.DataItem, "_CategoryLinkID") %>'>Remove</asp:linkbutton></td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                        <tr>
                            <td><asp:TextBox ID="txtTitle" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlNodeList" runat="server" /></td>
                            <td>
                                <guild:WebImageMaker id="wimLinkImage" runat="server" 
	    		                     CancelButtonText="Cancel" ConfirmButtonText="OK" UploadButtonText="Upload" 
                                     ImageWidth="95" ImageHeight="105"
	                                 Format="jpg" Quality="Medium" HandlerPath="~/WebImageMakerHandler.ashx" />
	                        </td>
                            <td><asp:LinkButton ID="btnAddPage" Text="Add Page" runat="server" /></td>
                        </tr>
                    </table>
				</div>				
            </div>
    </form>
</body>
</html>