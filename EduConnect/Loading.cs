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
    public partial class Loading : Form
    {
        private Timer timer;

        public Loading()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 80;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            loadingbar.Increment(5);

            if (loadingbar.Value == loadingbar.Maximum)
            {
                timer.Stop();
                this.Hide();
                var window = new Login();
                window.Show();
            }
        }
    }
}
