using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sales_Management
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
            {
                this.Hide();
                Frm_Login frm = new Frm_Login();
                frm.ShowDialog();
            }
            else
            {
                progressBar1.Value++;
                progressBar1.Refresh();
            }
        }
    }
}
