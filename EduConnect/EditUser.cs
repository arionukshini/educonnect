using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace EduConnect
{
    public partial class EditUser : Form
    {
        private int ID;
        private string oldEmail;

        public EditUser(int ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void editusersaveBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");

            string name = editusernameBox.Text;
            string email = edituseremailBox.Text.ToLower();
            string password = edituserpasswordBox.Text;

            string gender = editusergenderMaleRB.Checked ? "Male" : "Female";

            string date = edituserdatePicker.Value.ToString("yyyy-MM-dd");
            string position = edituserpositionCombo.SelectedItem?.ToString();

            List<string> selectedInterests = new List<string>();

            if (edituserinterestsMathCheck.Checked)
                selectedInterests.Add("Math");

            if (edituserinterestsScienceCheck.Checked)
                selectedInterests.Add("Science");

            if (edituserinterestsSportsCheck.Checked)
                selectedInterests.Add("Sports");

            // bashkimi i interesave ne nje string, nese asnje interest nuk plotesohet atehere stringu do te jete "None"
            string interests = selectedInterests.Any() ? string.Join(", ", selectedInterests) : "None";

            // shikon nese fushat tek regjistrimi jane te zbrazura (pjease e interesave eshte opsionale) dhe e ruan rezultatin (True ose False) tek emptyCheck
            bool emptyCheck = string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(position);

            if (emptyCheck)
            {
                MessageBox.Show("Please fill in all the fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!IsValidEmail(email))
            {
                MessageBox.Show("Please put in a valid email!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (IsEmailInUse(email, con))
            {
                MessageBox.Show("Email is already in use!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!(gender == "Male" || gender == "Female"))
            {
                MessageBox.Show("You must put either Male or Female in the gender section!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // perditesimi i informatave
                SqlCommand cmdEdit = new SqlCommand(@"UPDATE [dbo].[users] " +
                                      "SET [Name] = '" + name + "', " +
                                          "[Email] = '" + email + "', " +
                                          "[Password] = '" + password + "', " +
                                          "[Gender] = '" + gender + "', " +
                                          "[Birthday] = '" + date + "', " +
                                          "[Position] = '" + position + "', " +
                                          "[Interests] = '" + interests + "' " +
                                      "WHERE [ID] = " + ID, con);

                con.Open();
                cmdEdit.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("User was edited successfully!", "Edit Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private bool IsValidEmail(string email)
        {
            string emailText = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // perdorimi i regex per validimin e imelles
            return Regex.IsMatch(email, emailText);
        }

        // funksion per te kontrolluar nese imella eshte ne perdorim
        private bool IsEmailInUse(string email, SqlConnection con)
        {
            if (email == oldEmail)
            {
                return false;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM users WHERE Email = '" + email + "'", con);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();

                return count > 0;
            }
        }

        private void EditUser_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");

            string query = "Select * FROM [educonnect].[dbo].[users] Where ID ='" + ID + "'";
            edituseridBox.Text = ID.ToString();

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string userEmail = reader["Email"].ToString();
                string userName = reader["Name"].ToString();
                string userPassword = reader["Password"].ToString();
                string userGender = reader["Gender"].ToString();
                string userPosition = reader["Position"].ToString();
                string userBirthday = reader["Birthday"].ToString();
                string userInterests = reader["Interests"].ToString();

                this.oldEmail = userEmail;

                editusernameBox.Text = userName;
                edituseremailBox.Text = userEmail;
                edituserpasswordBox.Text = userPassword;
                edituserpositionCombo.SelectedItem = userPosition;
                edituserdatePicker.Text = userBirthday;

                if (userGender == "Male")
                {
                    editusergenderMaleRB.Checked = true;
                    editusergenderFemaleRB.Checked = false;
                }
                else if (userGender == "Female")
                {
                    editusergenderMaleRB.Checked = false;
                    editusergenderFemaleRB.Checked = true;
                }

                // ndarja e interesave dhe ruajtja e tyre ne nje array
                string[] interestsArray = userInterests.Split(',');

                // loop e cila merr vlerat e array dhe ben loop per secilen
                foreach (string interest in interestsArray)
                {
                    switch (interest.Trim())
                    {
                        case "Math":
                            edituserinterestsMathCheck.Checked = true;
                            break;

                        case "Sports":
                            edituserinterestsSportsCheck.Checked = true;
                            break;

                        case "Science":
                            edituserinterestsScienceCheck.Checked = true;
                            break;

                        default:
                            break;
                    }
                }
            }
            con.Close();
        }

        private void edituserpasswordBox_MouseHover(object sender, EventArgs e)
        {
            edituserpasswordBox.UseSystemPasswordChar = false;
        }

        private void edituserpasswordBox_MouseLeave(object sender, EventArgs e)
        {
            edituserpasswordBox.UseSystemPasswordChar = true;
        }
    }
}
