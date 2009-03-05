<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProductList.ascx.cs"  EnableViewState="True" Inherits="Cuyahoga.Modules.ECommerce.Web.Controls.ProductList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"  %>
<%@ Import Namespace="Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces" %>
<%@ Register TagPrefix="ctl" Namespace="Koutny.WebControls" Assembly="Cuyahoga.Modules.ECommerce" %>
<div class="col-left">
    
    
     <div class="head">
          <h3 class="headerBackGround">Compare Products</h3>
      </div>
      <div class="sideBox">
          <p>You have no items to compare.</p>
      </div>
    
      <div class="head">
          <h3 class="headerBackGround">Shop by </h3>
      </div>
      <div class="sideBox">
            <p>Category</p>
       </div>
       <div class="sideBox">
            <p>Price</p>
       </div>

</div>
<div class="col-main">
<asp:Panel ID="pnlCatImage" runat="server" Visible="false">
	    <div id="images">
		    <img src="<%=BannerImageUrl%>" alt="<%=CatImageAltText%>" Width="500px"/>
	    </div>
	</asp:Panel>
    <h2><asp:Literal ID="litCategoryName" runat="server"></asp:Literal></h2>
    <asp:Literal ID="litCategoryDescription" runat="server"></asp:Literal>
    
    <div class="sideBox">
        <table id="listOptions">
       <tr>
 
       <td width="200px"> <p>Page</p> <asp:DropDownList ID="ddlPage" EnableViewState="true" runat="server" AutoPostBack="True"> </asp:DropDownList></td>
      
       <td width="200px"><p>Results  per Page</p> <asp:DropDownList EnableViewState="true" ID="ddlResultsPerPage" runat="server" AutoPostBack="True">
       <asp:ListItem Text="5"  Value="5"></asp:ListItem>
       <asp:ListItem Text="10" Selected="True"  Value="10"></asp:ListItem>
       <asp:ListItem Text="25" Value="25"></asp:ListItem>
       <asp:ListItem Text="50"  Value="50"></asp:ListItem>
       </asp:DropDownList> </td>
       <td  width="200px"> <p>Sort by </p><asp:DropDownList ID="ddlSortBy" EnableViewState="true" runat="server" AutoPostBack="True">
       <asp:ListItem Text="Price - Lowest First"  Value="PriceLowest"></asp:ListItem>
       <asp:ListItem Text="Price - Highest First"  Value="PriceHighest"></asp:ListItem>
       <asp:ListItem Text="Product Name" Selected="True" Value="ProductName"></asp:ListItem>

       </asp:DropDownList></td>
       <td width="200px"><p> Display Mode </p><asp:DropDownList  EnableViewState="true" ID="ddlDisplayMode" runat="server" AutoPostBack="True">
       <asp:ListItem Text="List"  Value="List"></asp:ListItem>
       <asp:ListItem Text="Grid" Selected="true" Value="Grid"></asp:ListItem>
       </asp:DropDownList></td>
       </tr> 
        </table>
    </div>


<p>Showing items <asp:Literal ID="litPaginationFrom" runat="server"></asp:Literal> - <asp:Literal  ID="litPaginationTo" runat="server"></asp:Literal> of <asp:Literal ID="litProductCount" runat="server"></asp:Literal></p>
			
<asp:repeater runat="server" id="rptProductsList" EnableViewState="true">
  <headertemplate>
  <table id="productList">
  <tr>
    <th  width="125px"></th>
    <th  width="200px">Product Name</th>
    <th  width="250px">Description</th>
    <th  width="100px">Price</th>
    <th  width="100px"></th>
  </tr>
  </headertemplate>
    <itemtemplate>
<tr>
    <td width="100px"> 
        <asp:HyperLink runat="server" ID="hplImagePopUp">
         <asp:Image Width="100px" ID="imgProduct" runat="server" Visible="false"   />
         </asp:HyperLink>
    </td>
   <td width="100px">
       
             <asp:HyperLink ID="hplProductName" runat="server" Visible="false" />
        
    </td>
   <td width="375px">
        <asp:Literal ID="litDescription" runat="server" />
   </td>

   <td width="50px">
        <asp:Literal ID="litPrice" runat="server"/>
   </td>
   <td width="200px">
        <asp:TextBox ID="txtProdID" runat="server" Visible="false" Text="<%#((IProductSummary) Container.DataItem).ProductID%>" />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="AddToBasket" Text="Add to basket" CssClass="basket" />
   </td>
</tr>           
    </itemtemplate>
    <footertemplate>
        
    </table>

    </footertemplate>
</asp:repeater>

<asp:repeater runat="server" id="rptProductsGrid" EnableViewState="true">
  <headertemplate>
  <table id="productGrid"><tr>
  </headertemplate>
    <itemtemplate>

    <td width="20%"> 
        <asp:HyperLink runat="server" ID="hplImagePopUp">
         <asp:Image Width="100px" ID="imgProduct" runat="server" Visible="false"   />
         </asp:HyperLink>
  
       
             <asp:HyperLink ID="hplProductName" runat="server" Visible="false" />
        
   
        <asp:Literal ID="litDescription" runat="server" />
  

   
        <p><asp:Literal ID="litPrice" runat="server"/></p>
   
        <asp:TextBox ID="txtProdID" runat="server" Visible="false" Text="<%#((IProductSummary) Container.DataItem).ProductID%>" />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="AddToBasket" Text="Add to basket" CssClass="basket" />
   </td>
    <asp:Literal id="rowHeader" runat="server"><tr></asp:Literal>
  <asp:Literal runat="server" ID="rowFooter"></tr></asp:Literal>         
    </itemtemplate>
    <footertemplate>
        </tr>
    </table>

    </footertemplate>
</asp:repeater>


</div>