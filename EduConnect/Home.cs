using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EduConnect
{
    public partial class Home : Form
    {
        private string userName;
        private string userPosition;

        public Home(string userName, string userPosition)
        {
            InitializeComponent();
            this.userName = userName;
            this.userPosition = userPosition;
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            if (userPosition == "Professor") {
                homeusername.Text = "Prof. " + userName;
            }
            else
            {
                homeusername.Text = userName;
            }
        }

        private void homelogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var window = new Login();
                window.Show();
            }
        }

        private void homeprofile_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Profile(userName, userPosition);
            window.Show();
        }

        private void homeschedule_Click(object sender, EventArgs e)
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

        private void homegrades_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Grades(userName, userPosition);
            window.Show();
        }

        private void homeusers_Click(object sender, EventArgs e)
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

        private void homevisitwebsite_Click(object sender, EventArgs e)
        {
            string url = "https://example.com";
            System.Diagnostics.Process.Start(url); // e hap linkun ne browser
        }
    }
}
