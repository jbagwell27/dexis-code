using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winformTestbed
{
    public partial class Form1 : Form
    {
        
        string name;

        public Form1()
        {
            InitializeComponent();
        }

        private void launch2nd_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();

            name = form.NameBoxText;


        }

        private void saveInfoButton_Click(object sender, EventArgs e)
        {
            secondWindow.Text = name;
        }
    }
}
