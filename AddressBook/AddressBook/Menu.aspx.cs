using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Drawing;

///////////////////////////////////////////
///                 TEAM                ///
///  Παναγιώτης Τριανταφύλλου   Π18154  ///
///  Ισίδωρος Τσαλαπάτης        Π18155  ///
///  Μάριος Παπαγγελής          Π15109  ///
///////////////////////////////////////////

namespace AddressBook
{    public partial class WebForm1 : System.Web.UI.Page
    {
        static bool list_visible = false;
        OleDbConnection connection;
        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = " + @"Contacts.mdb";

        protected void Page_Load(object sender, EventArgs e)
        {
            list_visible = false;
            try
            {
                connection = new OleDbConnection
                {
                    ConnectionString = connectionString
                };
                connection.Open();
                connection.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "mycontrol2", "alert('" + ex + "');", true);
            }
        }

        protected void browsebtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Browse.aspx");
        }

        protected void addbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddContact.aspx");
        }

        protected void searchbtn_Click(object sender, EventArgs e)
        {

            if (!list_visible)
            {
                searchlist1.Visible = true;
                searchlist2.Visible = true;
                searchlist3.Visible = true;
                list_visible = true;
            }
            else
            {
                searchlist1.Visible = false;
                searchlist2.Visible = false;
                searchlist3.Visible = false;
                list_visible = false;
            }

        }
    }
}