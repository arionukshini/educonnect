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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            // shfaq nja messagebox qe e lejon perdoruesin te konfirmoj se a don me mbyll aplikacionin
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // rezultati i messagebox
            if (result == DialogResult.No)
            {
                // nese perdoruesi klikon "jo" atehere programi nuk do te mbyllet
                e.Cancel = true;
            }
            // nese perdoruesi klikon "po" atehere programi do te mbyllet
        }

        private void loginsignupBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new SignUp();
            window.Show();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");

            string email = loginemailBox.Text.Trim().ToLower();
            string password = loginpasswordBox.Text.Trim();

            string query = "Select * FROM [educonnect].[dbo].[users] Where Email ='" + email + "' AND Password ='" + password + "'";
           
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dtb = new DataTable();

            sda.Fill(dtb);

            if (email.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("Please fill in the email and password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!IsValidEmail(email))
            {
                MessageBox.Show("Please put in a valid email!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dtb.Rows.Count == 1)
            {
                // ruajta e emrit te perdoruesit per te perdorur ne faqen Home
                string query2 = "SELECT Name, Position FROM [educonnect].[dbo].[users] WHERE Email ='" + email + "' AND Password ='" + password + "'";
                SqlCommand cmd = new SqlCommand(query2, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string userName = reader["Name"].ToString();
                    string userPosition = reader["Position"].ToString();

                    this.Hide();
                    var window = new Home(userName, userPosition);
                    window.Show();
                }

                con.Close();
            }
            else
            {
                MessageBox.Show("The email or the password you put in is wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // funksion per validimin e imelles
        private bool IsValidEmail(string email)
        {
            string emailText = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // perdorimi i regex per validimin e imelles
            return Regex.IsMatch(email, emailText);
        }

        private void loginpasswordBox_MouseHover(object sender, EventArgs e)
        {
            loginpasswordBox.UseSystemPasswordChar = false;
        }

        private void loginpasswordBox_MouseLeave(object sender, EventArgs e)
        {
            loginpasswordBox.UseSystemPasswordChar = true;
        }
    }
}
