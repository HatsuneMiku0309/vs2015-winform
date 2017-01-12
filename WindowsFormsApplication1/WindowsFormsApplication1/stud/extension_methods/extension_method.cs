using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using char_extensionmethods;  //add new namespace

namespace WindowsFormsApplication1.stud.extension_methods
{
    public partial class extension_method : Form
    {
        public extension_method()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char sex = 'B';
            int charlen = sex.getlength();
            MessageBox.Show(sex+"字元長度:"+charlen.ToString());
        }
    }
}
