<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchByFName.aspx.cs" Inherits="AddressBook.SearchByFName" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Browse</title>
    <meta charset="utf-8"/>
    <link rel="stylesheet" type="text/css" href="MyStyleSheet.css"/>
    <style type="text/css">

    </style>
</head>
<body>
    <!--LOGO-->
    <h1 style ="font-size:36px"><a style="color: white; text-decoration: none;" href ="Menu.aspx">@</a></h1>
    <h1 style ="font-size:14px"><a style="color: white; text-decoration: none;" href ="Menu.aspx">Address Book</a></h1>
    <h1 style ="text-align: left">Browse</h1>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="Contacts">
            <Columns>
                <asp:BoundField DataField="FName" HeaderText="First Name" SortExpression="FName" />
                <asp:BoundField DataField="LName" HeaderText="Last Name" SortExpression="LName" />
                <asp:BoundField DataField="Tel" HeaderText="Phone Number" SortExpression="Tel" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                <asp:BoundField DataField="BDay" HeaderText="Birthday" SortExpression="BDay" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="Contacts" runat="server" ConnectionString="<%$ ConnectionStrings:ContactsConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ContactsConnectionString2.ProviderName %>" SelectCommand="SELECT [FName], [LName], [Tel], [Email], [Address], [BDay] FROM [Contacts]"></asp:SqlDataSource>
    </form>
</body>
</html>
