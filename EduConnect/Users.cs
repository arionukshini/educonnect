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
    public partial class Users : Form
    {
        private string userName;
        private string userPosition;

        public Users(string userName, string userPosition)
        {
            InitializeComponent();
            this.userName = userName;
            this.userPosition = userPosition;
        }

        private void Users_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            this.usersTableAdapter.OnlyStudents1(this.educonnectDataSet.users);
            this.usersTableAdapter1.OnlyProfessors1(this.educonnectDataSet2.users);
            if (userPosition == "Professor")
            {
                usersusername.Text = "Prof. " + userName;
            }
            else
            {
                usersusername.Text = userName;
            }
        }

        private void userslogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var window = new Login();
                window.Show();
            }
        }

        private void usershome_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Home(userName, userPosition);
            window.Show();
        }

        private void usersprofile_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Profile(userName, userPosition);
            window.Show();
        }

        private void usersschedule_Click(object sender, EventArgs e)
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

        private void usersgrades_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Grades(userName, userPosition);
            window.Show();
        }
    }
}
