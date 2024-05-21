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

namespace EduConnect
{
    public partial class ScheduleProfessor : Form
    {
        private string userName;
        private string userPosition;

        public ScheduleProfessor(string userName, string userPosition)
        {
            InitializeComponent();
            this.userName = userName;
            this.userPosition = userPosition;
        }

        private void ScheduleProfessor_Load(object sender, EventArgs e)
        {
            this.scheduleTableAdapter.Fill(this.educonnectDataSetSchedule.schedule);

            if (userPosition == "Professor")
            {
                schedulepusername.Text = "Prof. " + userName;
            }
            else
            {
                schedulepusername.Text = userName;
            }
        }

        private void ScheduleProfessor_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void scheduleplogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var window = new Login();
                window.Show();
            }
        }

        private void schedulepprofile_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Profile(userName, userPosition);
            window.Show();
        }

        private void schedulephome_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Home(userName, userPosition);
            window.Show();
        }

        private void schedulepusers_Click(object sender, EventArgs e)
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

        private void schedulepgrades_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Grades(userName, userPosition);
            window.Show();
        }

        private void schedulepsaveBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");

            string period = schedulepperiodBox.Text;
            string day = schedulepdayCombo.SelectedItem?.ToString();
            string subject = schedulepsubjectCombo.SelectedItem?.ToString();

            bool emptyCheck = string.IsNullOrEmpty(period) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(day);

            if (subject == "None")
            {
                subject = null;
            }
            else
            {
                subject = schedulepsubjectCombo.SelectedItem?.ToString();
            }

            if (!emptyCheck)
            {
                int parsedPeriod;
                bool periodParsed = int.TryParse(period, out parsedPeriod);

                if (periodParsed && parsedPeriod > 0 && parsedPeriod < 6)
                {
                    SqlCommand cmdEdit = new SqlCommand(@"UPDATE [dbo].[schedule] " +
                                  "SET [" + day + @"] = '" + subject + "' " +
                                  "WHERE [Period] = " + parsedPeriod, con);

                    con.Open();
                    cmdEdit.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Schedule updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please input a valid integer as the period, withing the 1 to 5 range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all the fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // rifreskimi i databazes
            this.scheduleTableAdapter.Fill(this.educonnectDataSetSchedule.schedule);
        }
    }
}
