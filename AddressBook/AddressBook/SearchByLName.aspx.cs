using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.OleDb;
using System.Drawing;
using System.Web.UI;

namespace AddressBook
{
    public partial class SearchByLName : Page
    {
        public static int index = 0;

        public static List<Contact> contactsList = new List<Contact>();
        public static List<Contact> matchingcontacts = new List<Contact>();

        OleDbConnection connection;
        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = " + @"Contacts.mdb";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Connection to DB
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
                ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Error: " + ex + "');", true);
            }
        }

        ////////////
        ///SEARCH///
        ////////////
        protected void Searchbtn_Click(object sender, EventArgs e)
        {
            contactsList.Clear();//Clears the list because the DB will be read again
            contactsList.TrimExcess();//Trims the list because there can be less contacts in the next search
            matchingcontacts.Clear();//Clears the list so the new contacts can be added
            matchingcontacts.TrimExcess();//Trims the list because there can be less matches in the next search. We need to know the exact number of matches because it will be used as a flag for the visibility of the buttin 'Next Contact'
            if (!(searchlname.Text.Length >= 0 && searchlname.Text.Trim().Length == 0))//Ensures that the textbox is not null and is clear of spaces
            {
                OleDbDataReader reader = null;
                try//Tries to connect to the DB and retrieve the info of every contact in the DB
                {
                    connection.Open();
                    string query = "SELECT [FName], [LName], [Tel], [Email], [Address], [BDay] FROM [Contacts];";
                    OleDbCommand cmd = new OleDbCommand(query, connection);

                    reader = cmd.ExecuteReader();

                    while (reader.Read())//For every contact in the DB
                    {
                        Contact contact = new Contact(reader["FName"].ToString(), reader["LName"].ToString(), reader["Tel"].ToString(), reader["Email"].ToString(), reader["Address"].ToString(), DateTime.Parse(reader["BDay"].ToString()).ToShortDateString());//An instance of the class Contact is created
                        contactsList.Add(contact);//And then this instance is added to the contactsList
                    }

                    //Now the list contains all the contacts of the DB
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Error occured during the search');", true);//Notification for any error occurs while connecting and retrieving data from the DB
                }
                finally//After all the DB is read
                {
                    if (reader != null)
                        reader.Close();
                    if (connection != null)
                        connection.Close();

                    index = 0;

                    //Searches all the contacts, and picks those that their last name matches with the user's input. The contacts picked are added to a new list
                    foreach (Contact contact in contactsList)
                        if (contact.Clname == searchlname.Text)
                            matchingcontacts.Add(contact);


                    if (matchingcontacts.Count > 0)//If there is at least 1 match, it fills the fields with the info of the first contact in the matching list. Also, the edit/delete/next buttons become visible
                    {
                        fname.Text = matchingcontacts[index].Cfname;
                        lname.Text = matchingcontacts[index].Clname;
                        tel.Text = matchingcontacts[index].Ctel;
                        email.Text = matchingcontacts[index].Cemail;
                        address.Text = matchingcontacts[index].Caddress;
                        bday.Text = matchingcontacts[index].Cbday;
                        editbtn.Visible = true;
                        deletebtn.Visible = true;
                        if (matchingcontacts.Count > 1)
                        {
                            nextcontact.Visible = true;
                            index++;
                        }
                        else
                            nextcontact.Visible = false;
                    }
                    else//If there are no matches, a notification informs the user that the contact is was not found                    
                        ScriptManager.RegisterStartupScript(this, GetType(), "notfound", "alert('Contact not found');", true);
                }
            }
        }

        private static bool edit_mode = false;

        //////////
        ///EDIT///
        //////////
        protected void Edit_Click(object sender, EventArgs e)//Two modes are available. Either Edit mode (edit_mode == true), either View Mode (edit_mode == false). The following lines make adjaustments to the UI (visibility of the Save Changes Button and the textboxes above) for each case
        {
            if (edit_mode)
            {
                fnametxtbox.Visible = false;
                lnametxtbox.Visible = false;
                teltxtbox.Visible = false;
                emailtxtbox.Visible = false;
                addresstxtbox.Visible = false;
                bdaytxtbox.Visible = false;
                savechangesbtn.Visible = false;
                edit_mode = false;
                editbtn.BackColor = Color.CadetBlue;

            }
            else
            {
                editbtn.BackColor = Color.Aquamarine;
                fnametxtbox.Visible = true;
                lnametxtbox.Visible = true;
                teltxtbox.Visible = true;
                emailtxtbox.Visible = true;
                addresstxtbox.Visible = true;
                bdaytxtbox.Visible = true;
                savechangesbtn.Visible = true;

                fnametxtbox.Text = fname.Text;
                lnametxtbox.Text = lname.Text;
                teltxtbox.Text = tel.Text;
                emailtxtbox.Text = email.Text;
                addresstxtbox.Text = address.Text;
                bdaytxtbox.Text = bday.Text;
                edit_mode = true;
            }
        }

        //////////////////
        ///SAVE CHANGES///
        //////////////////
        protected void savechanges_Click(object sender, EventArgs e)
        {
            //Validity checks for all textboxes
            if (!(fnametxtbox.Text.Length >= 0 && fnametxtbox.Text.Trim().Length == 0))
            {
                if (!(lnametxtbox.Text.Length >= 0 && lnametxtbox.Text.Trim().Length == 0))
                {
                    if (!(teltxtbox.Text.Length >= 0 && teltxtbox.Text.Trim().Length == 0) && teltxtbox.Text.Length <= 10)
                    {                        
                        bool valideimail = new EmailAddressAttribute().IsValid(emailtxtbox.Text);
                        if (valideimail)
                        {
                            if (!(addresstxtbox.Text.Length >= 0 && addresstxtbox.Text.Trim().Length == 0))
                            {                                
                                if (DateTime.TryParse(bdaytxtbox.Text, out DateTime dt))
                                {
                                    connection.Open();
                                    string query;
                                    OleDbCommand command;

                                    //Search if a contact with the same phone number exists
                                    OleDbDataReader reader;
                                    query = "SELECT [FName], [Tel] FROM [Contacts] WHERE Tel = '" + teltxtbox.Text + "';";
                                    OleDbCommand cmd = new OleDbCommand(query, connection);
                                    reader = cmd.ExecuteReader();
                                    string existingtel = "";
                                    while (reader.Read())
                                    {
                                        existingtel = reader["Tel"].ToString();
                                    }
                                    if (teltxtbox.Text == existingtel && teltxtbox.Text != tel.Text)//If there is a contact with the same phone number, send notification
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "exists", "alert('Contact with phone number " + teltxtbox.Text + " already exists');", true);
                                        connection.Close();
                                    }
                                    else
                                    {
                                        //After we ensure the textbox values are valid, and there is no contact with the same phone numver as the new one, the old contact is deleted from the database
                                        query = "DELETE FROM [Contacts] WHERE Tel = '" + tel.Text + "';";
                                        command = new OleDbCommand(query, connection);
                                        command.ExecuteNonQuery();

                                        //Insert the new contact, with the updated info to the DB
                                        query = "Insert into Contacts(FName,LName,Tel,Email,Address,BDay) " + "values ('" + fnametxtbox.Text + "','" + lnametxtbox.Text + "','" + teltxtbox.Text + "','" + emailtxtbox.Text + "','" + addresstxtbox.Text + "','" + DateTime.Parse(bdaytxtbox.Text) + "')";
                                        command = new OleDbCommand(query, connection);
                                        command.ExecuteNonQuery();
                                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Edit completed!');window.location.href='Menu.aspx';", true);
                                        connection.Close();

                                        foreach (Contact contact in contactsList)
                                        {
                                            if (contact.Ctel == tel.Text)
                                            {
                                                contact.Cfname = fnametxtbox.Text;
                                                contact.Clname = lnametxtbox.Text;
                                                contact.Ctel = teltxtbox.Text;
                                                contact.Cemail = emailtxtbox.Text;
                                                contact.Caddress = addresstxtbox.Text;
                                                contact.Cbday = bdaytxtbox.Text;
                                            }
                                        }
                                    }
                                }
                                else
                                    ClientScript.RegisterStartupScript(this.GetType(), "invaliddate", "alert('Invalid Date');", true);
                            }
                            else
                                ClientScript.RegisterStartupScript(this.GetType(), "invalidaddress", "alert('Invalid Address');", true);
                        }
                        else
                            ClientScript.RegisterStartupScript(this.GetType(), "invalidemail", "alert('Invalid Email');", true);
                    }
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "invalidtel", "alert('Invalid Phone Number');", true);
                }
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "invalidlname", "alert('Invalid Last Name');", true);
            }
            else
                ClientScript.RegisterStartupScript(this.GetType(), "invalidfname", "alert('Invalid First Name');", true);
        }

        ////////////
        ///DELETE///
        ////////////
        protected void Delete_Click(object sender, EventArgs e)
        {
            connection.Open();
            string query = "DELETE FROM [Contacts] WHERE Tel = '" + tel.Text + "';";
            OleDbCommand command = new OleDbCommand(query, connection);
            command.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Contact deleted!');window.location.href='Menu.aspx';", true);//Notification for successful deletion and redirect
            connection.Close();
        }


        //////////////////
        ///NEXT CONTACT///
        //////////////////
        protected void nextcontact_Click(object sender, EventArgs e)
        {
            if (index + 1 <= matchingcontacts.Count)//If there is at least one contact left in the matching list
            {
                //Update the labels' text
                fname.Text = matchingcontacts[index].Cfname;
                lname.Text = matchingcontacts[index].Clname;
                tel.Text = matchingcontacts[index].Ctel;
                email.Text = matchingcontacts[index].Cemail;
                address.Text = matchingcontacts[index].Caddress;
                bday.Text = matchingcontacts[index].Cbday;

                if (edit_mode)//If Edit mode is on, the textboxes are updated too
                {
                    fnametxtbox.Text = fname.Text;
                    lnametxtbox.Text = lname.Text;
                    teltxtbox.Text = tel.Text;
                    emailtxtbox.Text = email.Text;
                    addresstxtbox.Text = address.Text;
                    bdaytxtbox.Text = bday.Text;
                }

                index++;//Index increased for the button to be ready for the next contact of the list when clicked

                if (index == matchingcontacts.Count)//If this was the last contact of the list, the index is set to 0 so the list can be shown again from the beggining
                    index = 0;
            }
        }
    }
}
