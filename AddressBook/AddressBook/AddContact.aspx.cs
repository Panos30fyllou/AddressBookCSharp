using System;
using System.Web.UI;
using System.Data.OleDb;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AddressBook
{
    public partial class AddContact : Page
    {

        OleDbConnection connection;
        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = " + @"Contacts.mdb";

        protected void Page_Load(object sender, EventArgs e)
        {
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

        protected void save_Click(object sender, EventArgs e)
        {
            bool valideimail = new EmailAddressAttribute().IsValid(email.Text);            
            bool validphone;
            if (tel.Text.Length <= 10)
                validphone = true;
            else
                validphone = false;

            if (valideimail)
            {
                if (validphone)
                {
                    if (!(fname.Text.Length >= 0 && fname.Text.Trim().Length == 0))
                    {
                        if (!(lname.Text.Length >= 0 && lname.Text.Trim().Length == 0))
                        {
                            if (!(address.Text.Length >= 0 && address.Text.Trim().Length == 0))
                            {
                                //Search if a contact with the same phone number exists
                                OleDbDataReader reader;
                                connection.Open();
                                string query = "SELECT [FName], [Tel] FROM [Contacts] WHERE Tel = '" + tel.Text + "';";
                                OleDbCommand cmd = new OleDbCommand(query, connection);
                                reader = cmd.ExecuteReader();
                                string existingtel = "";
                                while (reader.Read())
                                {
                                    existingtel = reader["Tel"].ToString();
                                }
                                if (tel.Text == existingtel)//If there is a contact with the same phone number, send notification
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "exists", "alert('Contact with phone number " + tel.Text + " already exists');", true);
                                    connection.Close();
                                }
                                else//else insert the new contact to the database
                                {
                                    query = "Insert into Contacts(FName,LName,Tel,Email,Address,BDay) " + "values ('" + fname.Text + "','" + lname.Text + "','" + tel.Text + "','" + email.Text + "','" + address.Text + "','" + DateTime.Parse(dateofbirth.Text) + "')";
                                    OleDbCommand command = new OleDbCommand(query, connection);
                                    command.ExecuteNonQuery();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "mycontrol2", "alert('" + fname.Text + " added to your Contacts!');", true);//Notification for successfull insertion

                                    //Clear the fields
                                    fname.Text = null;
                                    lname.Text = null;
                                    tel.Text = null;
                                    email.Text = null;
                                    address.Text = null;
                                    dateofbirth.Text = null;
                                    connection.Close();
                                }
                            }
                            else
                                ClientScript.RegisterStartupScript(this.GetType(), "invalidaddress", "alert('Invalid address');", true);
                        }
                        else
                            ClientScript.RegisterStartupScript(this.GetType(), "invalidlname", "alert('Invalid Last Name');", true);
                    }
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "invalidfname", "alert('Invalid First Name');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "invalidphonenumber", "alert('Phone number is invalid');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "invalidemail", "alert('Email is invalid');", true);
        }
    }
}