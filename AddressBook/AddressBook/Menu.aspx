<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="AddressBook.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menu</title>
    <meta charset="utf-8"/>
    <link rel="stylesheet" type="text/css" href="MyStyleSheet.css"/>
</head>
<body>
    <!--LOGO-->
    <h1 style="font-size:60px">@</h1>
    <h1 style="font-size:40px">Address Book</h1>

    <form id="form1" runat="server">
    <!--BUTTON LAYOUT-->
    <div style="height: 269px">
        <table id = "menutable" style=" text-align:center; width:50%;">
            <tr><td><asp:button id="Button1" class="mybtn" Text="Add Contact" runat="server" OnClick="addbtn_Click"></asp:button></td><td><asp:button id="searchbtn" class ="mybtn" Text="Search" runat="server" OnClick="searchbtn_Click"></asp:button></td><td><asp:button id="Button2" class="mybtn" Text="Browse" runat="server" OnClick="browsebtn_Click"></asp:button></td></tr>
            <tr ><td></td><td><asp:HyperLink id="searchlist1" href="SearchByFName.aspx" runat="server" style="color: white; text-decoration: none;" Visible="False">By First Name</asp:HyperLink></td><td></td></tr>
			<tr><td></td><td><asp:HyperLink id="searchlist2" href="SearchByLName.aspx" runat="server" style="color: white; text-decoration: none;" Visible="False">By Last Name</asp:HyperLink></td><td></td></tr>
			<tr><td></td><td><asp:HyperLink id="searchlist3" href="SearchByTel.aspx" runat="server" style="color: white; text-decoration: none;" Visible="False">By Phone Number</asp:HyperLink></td><td></td></tr>
        </table> 
    </div>
    </form>        
</body>
</html>
