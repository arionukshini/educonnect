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
    public partial class Profile : Form
    {
        private string userName;
        private string userPosition;
        private string oldEmail;

        public Profile(string userName, string userPosition)
        {
            InitializeComponent();
            this.userName = userName;
            this.userPosition = userPosition;
        }

        private void profilelogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var window = new Login();
                window.Show();
            }
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");

            string query = "Select * FROM [educonnect].[dbo].[users] Where Name ='" + userName + "' AND Position ='" + userPosition + "'";

            if (userPosition == "Professor")
            {
                profileusername.Text = "Prof. " + userName;
                profileusername2.Text = "Prof. " + userName;
            }
            else
            {
                profileusername.Text = userName;
                profileusername2.Text = userName;
            }

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string userID = reader["ID"].ToString();
                string userEmail = reader["Email"].ToString();
                string userPassword = reader["Password"].ToString();
                string userGender = reader["Gender"].ToString();
                string userBirthday = reader["Birthday"].ToString();
                string userInterests = reader["Interests"].ToString();

                this.oldEmail = userEmail;

                profileID.Text = "ID: " + userID;
                profilenameBox.Text = userName;
                profileemailBox.Text = userEmail;
                profilepasswordBox.Text = userPassword;
                profilegenderBox.Text = userGender;
                profilepositionBox.Text = userPosition;
                profiledatePicker.Text = userBirthday;

                // ndarja e interesave dhe ruajtja e tyre ne nje array
                string[] interestsArray = userInterests.Split(',');

                // loop e cila merr vlerat e array dhe ben loop per secilen
                foreach (string interest in interestsArray)
                {
                    switch (interest.Trim())
                    {
                        case "Math":
                            profileinterestsMathCheck.Checked = true;
                            break;

                        case "Sports":
                            profileinterestsSportsCheck.Checked = true;
                            break;

                        case "Science":
                            profileinterestsScienceCheck.Checked = true;
                            break;

                        default:
                            break;
                    }
                }
            }
            con.Close();

        }

        // funksion per validimin e imelles
        private bool IsValidEmail(string email)
        {
            string emailText = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // perdorimi i regex per validimin e imelles
            return Regex.IsMatch(email, emailText);
        }

        private void profilehome_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Home(userName, userPosition);
            window.Show();
        }

        private void Profile_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void profileeditBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");

            int ID = int.Parse(profileID.Text.Split(':')[1]?.Trim());
            string name = profilenameBox.Text;
            userName = name;
            string email = profileemailBox.Text.ToLower();
            string password = profilepasswordBox.Text;
            string gender = profilegenderBox.Text;
            string date = profiledatePicker.Value.ToString("yyyy-MM-dd");

            // marrja e interesave
            List<string> selectedInterests = new List<string>();

            if (profileinterestsMathCheck.Checked)
                selectedInterests.Add("Math");

            if (profileinterestsScienceCheck.Checked)
                selectedInterests.Add("Science");

            if (profileinterestsSportsCheck.Checked)
                selectedInterests.Add("Sports");

            // bashkimi i interesave ne nje string, nese asnje interest nuk plotesohet atehere stringu do te jete "None"
            string interests = selectedInterests.Any() ? string.Join(", ", selectedInterests) : "None";

            // shikon nese fushat tek regjistrimi jane te zbrazura (pjease e interesave eshte opsionale) dhe e ruan rezultatin (True ose False) tek emptyCheck
            bool emptyCheck = string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(gender);

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
                                          "[Interests] = '" + interests + "' " +
                                      "WHERE [ID] = " + ID, con);

                con.Open();
                cmdEdit.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (userPosition == "Professor")
            {
                profileusername.Text = "Prof. " + userName;
                profileusername2.Text = "Prof. " + userName;
            }
            else
            {
                profileusername.Text = userName;
                profileusername2.Text = userName;
            }
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

        private void profiledeleteBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");

            int ID = int.Parse(profileID.Text.Split(':')[1]?.Trim());
            DialogResult result = MessageBox.Show("Are you sure you want to delete your account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                SqlCommand cmdDelete = new SqlCommand("DELETE FROM [dbo].[users] WHERE [ID] = " + ID, con);

                con.Open();
                cmdDelete.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Account deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                var window = new Login();
                window.Show();
            }
        }

        private void profileschedule_Click(object sender, EventArgs e)
        {
            if (userPosition == "Professor")
            {
                this.Hide();
                var window = new ScheduleProfessor(userName, userPosition);
                window.Show();
            }
            else
            {
                this.Hide();
                var window = new Schedule(userName, userPosition);
                window.Show();
            }
        }

        private void profilegrades_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Grades(userName, userPosition);
            window.Show();
        }

        private void profileusers_Click(object sender, EventArgs e)
        {
            if (userPosition == "Professor")
            {
                this.Hide();
                var window = new UsersProfessor(userName, userPosition);
                window.Show();
            }
            else
            {
                this.Hide();
                var window = new Users(userName, userPosition);
                window.Show();
            }
        }

        private void profilepasswordBox_MouseHover(object sender, EventArgs e)
        {
            profilepasswordBox.UseSystemPasswordChar = false;
        }

        private void profilepasswordBox_MouseLeave(object sender, EventArgs e)
        {
            profilepasswordBox.UseSystemPasswordChar = true;
        }
    }
}
