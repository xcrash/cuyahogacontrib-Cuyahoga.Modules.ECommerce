<%@ Page language="c#" AutoEventWireup="false" Inherits="System.Web.UI.Page" %>
<%@ Register tagPrefix="cnt" Tagname="ctrl" src="Views/CreditCardPostBack.ascx" %>
<!doctype html public "-//w3c//dtd html 4.0 transitional//en" >
<html>
  <head>
    <title>CC</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
      <cnt:ctrl runat="server" id="ctlCnt" />
     </form>	
  </body>
</html>