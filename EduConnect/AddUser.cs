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
    public partial class AddUser : Form
    {
        private string position;
        public AddUser(string position)
        {
            InitializeComponent();
            this.position = position;
        }

        private void addusersaveBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");

            string name = addusernameBox.Text;
            string email = adduseremailBox.Text.ToLower();
            string password = adduserpasswordBox.Text;
            string gender = addusergenderMaleRB.Checked ? "Male" : "Female";
            string date = adduserdatePicker.Value.ToString("yyyy-MM-dd");

            // marrja e interesave
            List<string> selectedInterests = new List<string>();

            if (adduserinterestsMathCheck.Checked)
                selectedInterests.Add("Math");

            if (adduserinterestsScienceCheck.Checked)
                selectedInterests.Add("Science");

            if (adduserinterestsSportsCheck.Checked)
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
                MessageBox.Show("You added a " + position.ToLower() + " successfuly!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
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

        private void AddUser_Load(object sender, EventArgs e)
        {
            adduserpositionBox.Text = position;
        }

        private void adduserpasswordBox_MouseHover(object sender, EventArgs e)
        {
            adduserpasswordBox.UseSystemPasswordChar = false;
        }

        private void adduserpasswordBox_MouseLeave(object sender, EventArgs e)
        {
            adduserpasswordBox.UseSystemPasswordChar = true;
        }
    }
}
