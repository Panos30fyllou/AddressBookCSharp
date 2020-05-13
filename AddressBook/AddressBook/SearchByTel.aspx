<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchByTel.aspx.cs" Inherits="AddressBook.SearchByTel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search By Phone Number</title>
    <meta charset="utf-8"/>
    <link rel="stylesheet" type="text/css" href="MyStyleSheet.css"/>
    <style type="text/css">

    </style>
</head>
<body>
    <!--LOGO-->
    <h1 style ="font-size:36px"><a style="color: white; text-decoration: none;" href ="Menu.aspx">@</a></h1>
    <h1 style ="font-size:14px"><a style="color: white; text-decoration: none;" href ="Menu.aspx">Address Book</a></h1>
    <h1 style ="text-align: left">Search By Phone Number</h1>

    <!--SEARCH FIELD-->
    <form id="form1" runat="server">
        <table style =" width:50px">
            <tr><td><asp:Textbox id ="searchtel" style="width:100%" runat="server" required ="true"></asp:Textbox><asp:button Text="Search" class="mybtn" runat="server" id ="searchbtn" OnClick="Searchbtn_Click"></asp:button></td></tr>        
        </table>

        <table style="width: auto">
					<tr>
                        <td>
                            <label>First Name</label>
                        </td>
                        <td>
                            <asp:label ID="fname" runat="server"></asp:label>
                        </td>
                        <td>
                            <asp:TextBox ID ="fnametxtbox" runat="server" Visible="false"></asp:TextBox>
                        </td>
					</tr>
					<tr>
                        <td>
                            <label>Last Name</label>
                        </td>
                        <td>
                            <asp:label ID="lname" runat="server"></asp:label>
                        </td>
                        <td>
                            <asp:TextBox ID ="lnametxtbox" runat="server" Visible="false"></asp:TextBox>
                        </td>
					</tr>
                    <tr>
                        <td>
                            <label>Phone Number</label>
                        </td>
                        <td>
                            <asp:label ID="tel" runat="server"></asp:label>
                        </td>
                        <td>
                            <asp:TextBox ID ="teltxtbox" runat="server" Visible="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ControlToValidate="teltxtbox" ErrorMessage="Please Enter Numbers Only" ValidationExpression="^\d+$"  Display="Dynamic" SetFocusOnError="True"> </asp:RegularExpressionValidator>                       
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>email</label>
                        </td>
                        <td>
                            <asp:label ID="email" runat="server"></asp:label>
                        </td>
                        <td>
                            <asp:TextBox ID ="emailtxtbox" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Address</label>
                        </td>
                        <td>
                            <asp:label ID="address" runat="server"></asp:label>
                        </td>
                        <td>
                            <asp:TextBox ID ="addresstxtbox" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Date of Birth</label>
                        </td>
                        <td>
                            <asp:label ID="bday" runat="server"></asp:label>
                        </td>
                        <td>
                            <asp:TextBox ID ="bdaytxtbox" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>                        
		</table>
        <br />
        <asp:Button class="mybtn" ID="nextcontact" runat="server" Text="Next Contact" Visible="false" OnClick="nextcontact_Click" />
        <asp:Button class="mybtn" ID="editbtn" runat="server" OnClick="Edit_Click" Text="Edit"/>
        <asp:Button class="mybtn" ID="savechangesbtn" runat="server" OnClick="savechanges_Click" Text="Save Changes" Visible ="false"/>
        <asp:Button class="mybtn" ID="deletebtn" runat="server" OnClick="Delete_Click" Text="Delete"/>
    </form>
</body>
</html>
