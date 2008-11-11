<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BaseProductEditor.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.Controls.BaseProductEditor" %>
<%@ Register TagPrefix="fckeditorv2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>	

<table id="catalogue">
		
    <tbody>

        <asp:TextBox ID="txtProductID" Visible="false" runat="server" Width="200"></asp:TextBox>

        <tr>
            <td>Item Code</td>
            <td>
                <asp:TextBox ID="txtItemCode" runat="server" Width="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCode" runat="server" Enabled="true" ControlToValidate="txtItemCode" Text="Please enter a valid item code" ErrorMessage="Please enter a valid item code"></asp:RequiredFieldValidator>
            </td>	
        </tr>

        <tr>
            <td>Product Name</td>
            <td>
                <asp:TextBox ID="txtProductName" runat="server" Width="400"></asp:TextBox>	
                <asp:RequiredFieldValidator ID="rfvName" runat="server" Enabled="true" ControlToValidate="txtProductName" Text="Please enter a valid name" ErrorMessage="Please enter a valid name"></asp:RequiredFieldValidator>
            </td>	
        </tr>

        <tr>
            <td>Product Description</td>
            <td><fckeditorv2:fckeditor id="fckDescription"  runat="server" height="150px" width="650px" ></fckeditorv2:fckeditor></td>
        </tr>

        <tr>
            <td>Product Family</td>
            <td>
                <asp:TextBox ID="txtProductFamily" runat="server" Width="100"/>
            </td>	
        </tr>

        <tr>
            <td>Finish</td>
            <td>
                <asp:TextBox ID="txtFinish" runat="server" Width="200"/>
            </td>	
        </tr>

        <tr>
            <td>Weight(kg)</td>
            <td>
                <asp:TextBox ID="txtWeight" runat="server" Width="100" />
                <asp:RequiredFieldValidator runat="server" Enabled="true" ControlToValidate="txtWeight" Text="Please enter a valid weight" ErrorMessage="Please enter a valid weight"></asp:RequiredFieldValidator>
            </td>	
        </tr>

        <tr>
            <td>Base Price</td>
            <td>
                <asp:TextBox ID="txtPrice" runat="server" Width="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvprice" runat="server" Enabled="true" ControlToValidate="txtPrice" Text="Please enter a valid price" ErrorMessage="Please enter a valid price"></asp:RequiredFieldValidator>
            </td>	
        </tr>

        <tr>
            <td>Base Price Description</td>
            <td><asp:TextBox ID="txtPriceDescription" runat="server" Width="100"></asp:TextBox></td>
        </tr>

        <tr>
            <td>Is Kit?</td>
            <td><asp:CheckBox ID="chkIsKit" runat="server" Width="200" AutoPostBack="true"/></td>
        </tr>

        <asp:Panel ID="pnlKit" runat="server" Visible="false">
        <tr>
            <td>Short Description</td>
            <td><fckeditorv2:fckeditor id="fckShortDescription"  runat="server" height="150px" width="650px" ></fckeditorv2:fckeditor></td>
        </tr>
        
        <tr>
            <td>Features</td>
            <td><fckeditorv2:fckeditor id="fckFeatures"  runat="server" height="150px" width="650px" ></fckeditorv2:fckeditor></td>
        </tr>

        <tr>
            <td>Kit Comprises</td>
            <td><fckeditorv2:fckeditor id="fckKitComprises"  runat="server" height="150px" width="650px" ></fckeditorv2:fckeditor></td>
        </tr>
        </asp:Panel>

    </tbody>
</table>