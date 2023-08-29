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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void saveandclose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public string NameBoxText
        {
            get { return nameBox.Text; }
        }
    }
}
