<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductEdit.aspx.cs" Inherits="Cuyahoga.Modules.ECommerce.Web.Admin.ProductEdit"  validateRequest=false  %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce" %><%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain" %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<%@ Register Src="../controls/SKUEditor.ascx" TagName="editor" TagPrefix="sku" %>
<%@ Register tagPrefix="baseProduct" Tagname="editor" src="../controls/BaseProductEditor.ascx"%>
<%@ Register tagPrefix="attribute" Tagname="editor" src="../controls/AttributeEditor.ascx"%>
<%@ Register tagPrefix="crosssell" Tagname="editor" src="../controls/CrossSellEditor.ascx"%>
<%@ Register TagPrefix="breadcrumb" TagName="menu" Src="../Controls/BreadCrumb.ascx" %>
<%@ Register tagPrefix="synonym" Tagname="editor" src="../controls/SynonymEditor.ascx"%>
<%@ Register tagPrefix="image" Tagname="editor" src="../controls/ImageEditor.ascx"%>
<%@ Register tagPrefix="document" Tagname="editor" src="../controls/DocumentEditor.ascx"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Product Edit</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Product Editor</h1>

        <div class="post"> From here you can edit the details of your product.</div>
        <breadcrumb:menu runat="server" id="ctlBreadCrumb"/>

        <h1>Product Editor</h1>

        <div id="tabPane1" class="tab-pane">
			<div class="tabber">
			
			<div class="tabbertab">
            <div class="tab-page" id="tabPage1">
                <h2 class="tab">General</h2>
                <baseProduct:editor runat="server" ID="productEditor"/>
                <sku:editor runat="server" ID="skuEditor"/>
            </div>
            </div>

			<div class="tabbertab">
            <div class="tab-page" id="tabPage3">
                <attribute:editor runat="server" ID="attributeEditor"/>
            </div>
            </div>

			<div class="tabbertab">
            <div class="tab-page" id="tabPage4">
                <image:editor runat="server" ID="imageEditor" ImageWidth="130" ImageHeight="100"/>
            </div>
            </div>

			<div class="tabbertab">
            <div class="tab-page" id="tabPage6">
                <synonym:editor runat="server" ID="synonymEditor"/>
            </div>
            </div>

			<div class="tabbertab">
            <div class="tab-page" id="tabPage7">
                <document:editor runat="server" ID="documentEditor"/>
            </div>
            </div>

			<div class="tabbertab">
            <div class="tab-page" id="tabPage5">
                <h2 class="tab">Related Products</h2>
                <crosssell:editor runat="server" ID="crossSellEditor"/>
            </div>            
            </div>
            </div>

        </div>

        <div class="save">
            <p>
                <br/>
                <asp:Button ID="btnSave" runat="server" /> 
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" /> 
                <asp:Label ID="lblSave" runat="server"/>
                <asp:ValidationSummary ID="summary" runat="server" Enabled="true" Visible="true" HeaderText="You must enter a value in the following fields:" DisplayMode="BulletList" EnableClientScript="true" />
            </p>	
        </div> 

    </form>
</body>
</html>