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
    public partial class Schedule : Form
    {
        private string userName;
        private string userPosition;

        public Schedule(string userName, string userPosition)
        {
            InitializeComponent();
            this.userName = userName;
            this.userPosition = userPosition;
        }

        private void Schedule_Load(object sender, EventArgs e)
        {
            this.scheduleTableAdapter.Fill(this.educonnectDataSetSchedule.schedule);

            if (userPosition == "Professor")
            {
                scheduleusername.Text = "Prof. " + userName;
            }
            else
            {
                scheduleusername.Text = userName;
            }
        }

        private void Schedule_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void schedulelogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var window = new Login();
                window.Show();
            }
        }

        private void scheduleprofile_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Profile(userName, userPosition);
            window.Show();
        }

        private void schedulehome_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Home(userName, userPosition);
            window.Show();
        }

        private void scheduleusers_Click(object sender, EventArgs e)
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

        private void schedulegrades_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Grades(userName, userPosition);
            window.Show();
        }
    }
}
