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
using System.Text.RegularExpressions; // per perdorimin e Regex

namespace EduConnect
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void signupbackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Login();
            window.Show();
        }

        private void SignUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void signupBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");

            string name = signupnameBox.Text;
            string email = signupemailBox.Text.ToLower();
            string password = signuppasswordBox.Text;
            string rpassword = signuprpasswordBox.Text;

            string gender = signupgenderMaleRB.Checked ? "Male" : "Female";

            string date = signupdatePicker.Value.ToString("yyyy-MM-dd");
            string position = signuppositionCombo.SelectedItem?.ToString();

            // marrja e interesave
            List<string> selectedInterests = new List<string>();

                if (signupinterestsMathCheck.Checked)
                    selectedInterests.Add("Math");

                if (signupinterestsScienceCheck.Checked)
                    selectedInterests.Add("Science");

                if (signupinterestsSportsCheck.Checked)
                    selectedInterests.Add("Sports");

            // bashkimi i interesave ne nje string, nese asnje interest nuk plotesohet atehere stringu do te jete "None"
            string interests = selectedInterests.Any() ? string.Join(", ", selectedInterests) : "None";

            // shikon nese fushat tek regjistrimi jane te zbrazura (pjease e interesave eshte opsionale) dhe e ruan rezultatin (True ose False) tek emptyCheck
            bool emptyCheck = string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(rpassword) || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(position);

            // shikon se a shte password e ngjashme me retype password
            bool equalPasswords = rpassword.Equals(password);

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
            else if (!equalPasswords)
            {
                MessageBox.Show("You passwords you put in don't match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[users]
                ([Name]
                ,[Email]
                ,[Password]
                ,[Gender]
                ,[Birthday]
                ,[Position]
                ,[Interests])
                VALUES
                ('" + name + "', '" + email + "', '" + password + "', '" + gender + "', '" + date + "', '" + position + "', '" + interests + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Congrats, you signed up successfuly!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // funksion per validimin e imelles
        private bool IsValidEmail(string email)
        {
            string emailText = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // perdorimi i regex per validimin e imelles
            return Regex.IsMatch(email, emailText);
        }

        // funksion per te kontrolluar nese imella eshte ne perdorim
        private bool IsEmailInUse(string email, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM users WHERE Email = '" + email + "'", con);

            con.Open();
            int count = (int)cmd.ExecuteScalar();
            con.Close();

            return count > 0;
        }

        private void signuplinklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://arra-academy.com";
            System.Diagnostics.Process.Start(url); // e hap linkun ne browser
        }

        private void signuppasswordBox_MouseHover(object sender, EventArgs e)
        {
            signuppasswordBox.UseSystemPasswordChar = false;
        }

        private void signuppasswordBox_MouseLeave(object sender, EventArgs e)
        {
            signuppasswordBox.UseSystemPasswordChar = true;
        }

        private void signuprpasswordBox_MouseHover(object sender, EventArgs e)
        {
            signuprpasswordBox.UseSystemPasswordChar = false;
        }

        private void signuprpasswordBox_MouseLeave(object sender, EventArgs e)
        {
            signuprpasswordBox.UseSystemPasswordChar = true;
        }
    }
}
