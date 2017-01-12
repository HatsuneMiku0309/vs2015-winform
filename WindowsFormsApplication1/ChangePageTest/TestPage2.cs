using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangePageTest
{
    public partial class TestPage2 : Form
    {
        public TestPage2()
        {
            InitializeComponent();
        }

        public TestPage2(string msg)
        {
            InitializeComponent();
            textBox1.Text = msg;
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
        }

        private void TestPage2_Load(object sender, EventArgs e)
        {

        }

        private string _truename = string.Empty;
        public string TextBoxMsg
        {
            set
            {
                textBox1.Text = value;
            }
            get
            {
                return textBox1.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
