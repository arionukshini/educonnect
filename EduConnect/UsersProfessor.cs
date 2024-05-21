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
    public partial class UsersProfessor : Form
    {
        private string userName;
        private string userPosition;

        public UsersProfessor(string userName, string userPosition)
        {
            InitializeComponent();
            this.userName = userName;
            this.userPosition = userPosition;
        }

        private void UsersProfessor_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void UsersProfessor_Load(object sender, EventArgs e)
        {
            this.usersTableAdapter2.OnlyStudents2(this.educonnectDataSetProf1.users);
            this.usersTableAdapter3.OnlyProfessors2(this.educonnectDataSetProf2.users);
            if (userPosition == "Professor")
            {
                userspusername.Text = "Prof. " + userName;
            }
            else
            {
                userspusername.Text = userName;
            }
        }

        private void usersplogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var window = new Login();
                window.Show();
            }
        }

        private void usersphome_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Home(userName, userPosition);
            window.Show();
        }

        private void userspprofile_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Profile(userName, userPosition);
            window.Show();
        }

        // fshirja e nje apo me shume kolonave nga datagridview per studentet
        private void userspdeleteBtn_Click(object sender, EventArgs e)
        {
            if (userspstudentsView.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected user(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    List<int> selectedUserIDs = new List<int>();

                    foreach (DataGridViewRow selectedRow in userspstudentsView.SelectedRows)
                    {
                        int userID = Convert.ToInt32(selectedRow.Cells["iDDataGridViewTextBoxColumn"].Value);
                        selectedUserIDs.Add(userID);
                    }

                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");
                    con.Open();

                    foreach (int userID in selectedUserIDs)
                    {
                        SqlCommand cmdGradeExists = new SqlCommand("SELECT COUNT(*) FROM grades WHERE ID = " + userID, con);
                        int countGrade = (int)cmdGradeExists.ExecuteScalar();
                        con.Close();
                        bool deleteGrade = countGrade > 0;

                        con.Open();
                        if (deleteGrade)
                        {
                            SqlCommand cmdDelete = new SqlCommand("DELETE FROM [dbo].[users] WHERE [ID] = '" + userID + "'", con);
                            cmdDelete.ExecuteNonQuery();
                            SqlCommand cmdDeleteGrade = new SqlCommand("DELETE FROM [dbo].[grades] WHERE [ID] = '" + userID + "'", con);
                            cmdDeleteGrade.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmdDelete = new SqlCommand("DELETE FROM [dbo].[users] WHERE [ID] = '" + userID + "'", con);
                            cmdDelete.ExecuteNonQuery();
                        }
                    }
                    con.Close();

                    MessageBox.Show("Selected user(s) were deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("Please choose at least one user from the list above!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // rifreskimi i databazes
            this.usersTableAdapter2.OnlyStudents2(this.educonnectDataSetProf1.users);
        }

        // fshirja e nje apo me shume kolonave nga datagridview per profesoret
        private void userspdeleteBtn2_Click(object sender, EventArgs e)
        {
            if (userspprofessorsView.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected user(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    List<int> selectedUserIDs = new List<int>();

                    int currentUserID = GetCurrentUserID();

                    foreach (DataGridViewRow selectedRow in userspprofessorsView.SelectedRows)
                    {
                        int userID = Convert.ToInt32(selectedRow.Cells["dataGridViewTextBoxColumn1"].Value);

                        if (userID != currentUserID)
                        {
                            selectedUserIDs.Add(userID);
                        }
                        else
                        {
                            MessageBox.Show("You cannot delete yourself.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    foreach (DataGridViewRow selectedRow in userspprofessorsView.SelectedRows)
                    {
                        int userID = Convert.ToInt32(selectedRow.Cells["dataGridViewTextBoxColumn1"].Value);
                        selectedUserIDs.Add(userID);
                    }

                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");
                    con.Open();

                    foreach (int userID in selectedUserIDs)
                    {
                        SqlCommand cmdDelete = new SqlCommand("DELETE FROM [dbo].[users] WHERE [ID] = '" + userID + "'", con);
                        cmdDelete.ExecuteNonQuery();
                    }
                    con.Close();

                    MessageBox.Show("Selected user(s) were deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("Please choose at least one user from the list above!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // rifreskimi i databazes
            this.usersTableAdapter3.OnlyProfessors2(this.educonnectDataSetProf2.users);
        }

        // funksion qe jep id e perdoruesit
        private int GetCurrentUserID()
        {
            int currentuserID = 0;

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");
            con.Open();

            string queryID = "SELECT ID FROM [educonnect].[dbo].[users] WHERE Name ='" + userName + "' AND Position ='" + userPosition + "'";
            SqlCommand cmdID = new SqlCommand(queryID, con);

            SqlDataReader reader = cmdID.ExecuteReader();

            if (reader.Read())
            {
                currentuserID = int.Parse(reader["ID"].ToString());
            }

            con.Close();

            return currentuserID;
        }

        // editimi i nje kolone nga datagridview per studentet
        private void userspeditBtn_Click(object sender, EventArgs e)
        {
            if (userspstudentsView.SelectedRows.Count == 1)
            {
                int selectedIndex = userspstudentsView.SelectedRows[0].Index;
                int userId = Convert.ToInt32(userspstudentsView.Rows[selectedIndex].Cells["iDDataGridViewTextBoxColumn"].Value);

                var window = new EditUser(userId);
                if (window.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
            else if (userspstudentsView.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can only edit one user at a time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("You need to select one user before you edit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // rifreskimi i databazes
            this.usersTableAdapter2.OnlyStudents2(this.educonnectDataSetProf1.users);
        }

        // rifreskimi i databazave cdo here qe ndrohhet tab
        private void userspTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.usersTableAdapter2.OnlyStudents2(this.educonnectDataSetProf1.users);
            this.usersTableAdapter3.OnlyProfessors2(this.educonnectDataSetProf2.users);
        }

        // editimi i nje kolone nga datagridview per profesoret
        private void userspeditBtn2_Click(object sender, EventArgs e)
        {
            if (userspprofessorsView.SelectedRows.Count == 1)
            {
                int selectedIndex = userspprofessorsView.SelectedRows[0].Index;
                int userId = Convert.ToInt32(userspprofessorsView.Rows[selectedIndex].Cells["dataGridViewTextBoxColumn1"].Value);

                int currentUserID = GetCurrentUserID();

                if (userId != currentUserID)
                {
                    var window = new EditUser(userId);
                    if (window.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
                else
                {
                    MessageBox.Show("You can edit your info in the profile menu!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (userspprofessorsView.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can only edit one user at a time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("You need to select one user before you edit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // rifreskimi i databazes
            this.usersTableAdapter3.OnlyProfessors2(this.educonnectDataSetProf2.users);
        }

        // adicionimi i nje kolone ne datagridview per studentet
        private void userspaddBtn_Click(object sender, EventArgs e)
        {
            var window = new AddUser("Student");
            if (window.ShowDialog() == DialogResult.OK)
            {

            }

            this.usersTableAdapter2.OnlyStudents2(this.educonnectDataSetProf1.users);
        }

        // adicionimi i nje kolone ne datagridview per profesoret
        private void userspaddBtn2_Click(object sender, EventArgs e)
        {
            var window = new AddUser("Professor");
            if (window.ShowDialog() == DialogResult.OK)
            {

            }

            this.usersTableAdapter3.OnlyProfessors2(this.educonnectDataSetProf2.users);
        }

        private void userspschedule_Click(object sender, EventArgs e)
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

        private void userspgrades_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Grades(userName, userPosition);
            window.Show();
        }
    }
}
