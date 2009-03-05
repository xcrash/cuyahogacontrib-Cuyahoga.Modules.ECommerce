<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.Login" %>

    <div class="new-users">
            <div class="content">
            <h3>New Customers</h3>
            <p>By creating an account with our store, you will be able to move through the checkout process faster, store multiple shipping addresses, view and track your orders in your account and more.</p>
            </div>
        <div class="button-set">
           <asp:HyperLink ID="btnRegister" CssClass="updateButtonRight" NavigateUrl="~/Register.aspx"  Text="Register An Account" runat="server"></asp:HyperLink> 
        </div>
    </div>
    
    
    <div class="registered-users">
         <div class="content">
         <asp:label id="lblError" runat="server" enableviewstate="False" visible="False" cssclass="validator"></asp:label>
            <h3>Registered Customers</h3>
            <p>If you have an account with us, please log in.</p>

            <ul class="form-list">
                    <li>
                        <label for="email">Email Address <span class="required">*</span></label><br />
                       <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                    </li>
                    <li>
                        <label for="pass">Password <span class="required">*</span></label><br />
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                   </li>
                </ul>
                <p class="required">* Required Fields</p>   
                </div>
                 <div class="button-set">
                   <asp:HyperLink ID="btnResetPassword" CssClass="updateButtonLeft" NavigateUrl="~/ResetPassword.aspx" Text="Forgot Your Password?" runat="server"></asp:HyperLink> 
                    <asp:LinkButton ID="btnLogin" CssClass="updateButtonRight" OnClick="PerformLogin" Text="Login" runat="server"></asp:LinkButton> 

                </div>
   </div>

