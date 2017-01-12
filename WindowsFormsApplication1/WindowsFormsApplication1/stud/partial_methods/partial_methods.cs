using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using partialmethods_ex;
using WindowsFormsApplication1.stud.iterator_test;

namespace WindowsFormsApplication1.stud.partial_methods
{
    public partial class partial_methods : Form
    {
        public partial_methods()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sporcar sc = new sporcar();
            //sc.ggtest();
            sc.runpartialmethod();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            iterator_test.iterator_test aaa = new iterator_test.iterator_test();

            aaa.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            aaa.Location = new System.Drawing.Point(this.Left + button1.Right, this.Top + button1.Bottom);
            aaa.ShowDialog();
            
        }

        private void partial_methods_Load(object sender, EventArgs e)
        {
            button2.Text = "頁面跳轉:iterator_test";
        }
    }
}
