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
    public partial class Grades : Form
    {
        private string userName;
        private string userPosition;

        public Grades(string userName, string userPosition)
        {
            InitializeComponent();
            this.userName = userName;
            this.userPosition = userPosition;
        }

        private void Grades_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Grades_Load(object sender, EventArgs e)
        {
            this.gradesTableAdapter.Fill(this.educonnectDataSetGrades.grades);
            if (userPosition == "Professor")
            {
                gradesusername.Text = "Prof. " + userName;
                gradesidBox.ReadOnly = false;
                gradesgradeBox.ReadOnly = false;
                gradessubjectCombo.Enabled = true;
                gradessaveBtn.Enabled = true;
            }
            else
            {
                gradesusername.Text = userName;
            }
        }

        private void gradeslogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var window = new Login();
                window.Show();
            }
        }

        private void gradesprofile_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Profile(userName, userPosition);
            window.Show();
        }

        private void gradesusers_Click(object sender, EventArgs e)
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

        private void gradesschedule_Click(object sender, EventArgs e)
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

        private void gradeshome_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new Home(userName, userPosition);
            window.Show();
        }

        private void gradessaveBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-08JQ02D\SQLEXPRESS; Initial Catalog=educonnect; Integrated Security=true");

            string id = gradesidBox.Text;
            string subject = gradessubjectCombo.SelectedItem?.ToString();
            string grade = gradesgradeBox.Text;

            bool emptyCheck = string.IsNullOrEmpty(id) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(grade);

            if (!emptyCheck) // kushtezimi se a jane fushat e zbrazura, nese jane te mbushura vazhdojme me kod, nese jo paraqesim messagebox
            {
                int parsedId;
                bool idParsed = int.TryParse(id, out parsedId);

                if (idParsed && parsedId > 0) {
                    int parsedGrade;

                    bool gradeParsed = int.TryParse(grade, out parsedGrade);
                    bool gradeCheck = parsedGrade > 0 && parsedGrade < 6;

                    if (gradeParsed && gradeCheck) // kushtezimi se a eshte nota numer dhe a eshte me e vogel se 0 dhe me e madhe se 6, nese po vazdhojme me kod, nese jo paraqesim messagebox
                    {
                        string position = "Student";
                        SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM users WHERE ID = '" + parsedId + "' AND Position = '" + position + "'", con);

                        con.Open();
                        int count = (int)cmdCheck.ExecuteScalar();
                        con.Close();

                        bool validID = count > 0;

                        if (validID) // kushtezimi se a ekziston ni user me kete id, nese ekziston ni user vazhdojme me kod, nese jo paraqesim messagebox
                        {
                            SqlCommand cmdExists = new SqlCommand("SELECT COUNT(*) FROM grades WHERE ID = " + parsedId, con);
                            con.Open();
                            int count2 = (int)cmdExists.ExecuteScalar();
                            con.Close();

                            bool editMode = count2 > 0;

                            if (!editMode) // kushtezimi se a ekziston nje id me note ne ate lende, nese jo e shtojme, nese po e editojme, ne kete rast bejme shtim
                            {
                                SqlCommand cmdName = new SqlCommand("SELECT Name FROM users WHERE ID = '" + parsedId + "' AND Position = '" + position + "'", con);

                                con.Open();
                                string name = (string)cmdName.ExecuteScalar();
                                con.Close();

                                SqlCommand cmdAdd = new SqlCommand(@"INSERT INTO [dbo].[grades]
                                     ([ID], [Name], [" + subject + @"])
                                     VALUES
                                     ('" + parsedId + "', '" + name + "', '" + parsedGrade + "')", con);
                                con.Open();
                                cmdAdd.ExecuteNonQuery();
                                con.Close();
                                MessageBox.Show("You added " + name + "'s grade successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                SqlCommand cmdName = new SqlCommand("SELECT Name FROM users WHERE ID = '" + parsedId + "' AND Position = '" + position + "'", con);

                                con.Open();
                                string name = (string)cmdName.ExecuteScalar();
                                con.Close();

                                SqlCommand cmdEdit = new SqlCommand(@"UPDATE [dbo].[grades] " +
                                          "SET [" + subject + @"] = '" + parsedGrade + "' " +
                                          "WHERE [ID] = " + parsedId, con);

                                con.Open();
                                cmdEdit.ExecuteNonQuery();
                                con.Close();

                                MessageBox.Show("You added " + name + "'s grade successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Either this user doesn't exist or they are not a student!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The grade has to be an integer between 1 and 5!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please input a valid integer as the ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all the fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // rifreskimi i databazes
            this.gradesTableAdapter.Fill(this.educonnectDataSetGrades.grades);
        }
    }
}
