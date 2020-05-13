<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddContact.aspx.cs" Inherits="AddressBook.AddContact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Contact</title>
    <meta charset="utf-8"/>
    <link rel="stylesheet" type="text/css" href="MyStyleSheet.css"/>
</head>
<body>
    <h1 style ="font-size:36px"><a style="color: white; text-decoration: none;" href ="Menu.aspx">@</a></h1>
    <h1 style ="font-size:14px"><a style="color: white; text-decoration: none;" href ="Menu.aspx">Address Book</a></h1>
    <h1 style ="text-align: left">Add Contact</h1>
    <form id="form1" runat="server">
        <div id="addContactWrapper" style="height: 335px; width:40%">
            <table style="width: 330px">
					<tr><td><label>First Name</label></td><td>
                        <asp:TextBox Class= "txtbox" ID="fname" runat="server" required ="true"></asp:TextBox>
                        </td></tr>
					<tr><td><label>Last Name</label></td><td>
                        <asp:TextBox Class= "txtbox" ID="lname" runat="server" required ="true"></asp:TextBox>
                        </td></tr>
                    <tr><td><label>Phone Number</label></td><td>
                        <asp:TextBox Class= "txtbox" ID="tel" runat="server" required ="true"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ControlToValidate="tel" ErrorMessage="Please Enter Numbers Only" ValidationExpression="^\d+$"  Display="Dynamic" SetFocusOnError="True"> </asp:RegularExpressionValidator>
                        </td></tr>
                    <tr><td><label>email</label></td><td>
                        <asp:TextBox Class= "txtbox" ID="email" runat="server" required ="true"></asp:TextBox>
                        </td></tr>
                    <tr><td><label>Address</label></td><td>
                        <asp:TextBox Class= "txtbox" ID="address" runat="server" required ="true"></asp:TextBox>
                        </td></tr>
                    <tr><td><label>Date of Birth</label></td><td>
                        <asp:TextBox Class= "txtbox" ID="dateofbirth" textmode="Date" runat="server" required ="true"></asp:TextBox>
                        </td></tr>
                    <tr><td></td><td><asp:button Class= "mybtn" ID="save" runat="server" Text="Save" Width="130px" OnClick="save_Click"></asp:button></td></tr>            
			</table>
            <br />  


            
        </div>
    </form>
</body>
</html>
