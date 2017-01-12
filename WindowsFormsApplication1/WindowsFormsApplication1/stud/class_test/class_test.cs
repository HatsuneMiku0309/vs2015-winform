using PartialClasses_ex;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.stud.class_test
{
    public partial class class_test : Form
    {
        public class_test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Human Reader = new Human();
            Reader.Name = "dai";
            Reader.Age = 15;
            string Action = Reader.ToWalk();
            MessageBox.Show(Action, Reader.Name +"-"+ Reader.Age);
        }
    }
}
