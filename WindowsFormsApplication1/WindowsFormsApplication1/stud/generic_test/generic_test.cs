using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.stud.generic_test
{
    public partial class generic_test : Form
    {
        public generic_test()
        {
            InitializeComponent();
        }

        private void generic_test_Load(object sender, EventArgs e)
        {
            button1.Text = "generic_void";
            button2.Text = "generic_class";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            genericmethod<string>("My string");
            genericmethod<int>(100);
 
        }

        public void genericmethod<T>(T arg)
        {
            if (arg.GetType().ToString() == "System.String")
            {
                MessageBox.Show(arg.ToString(), "generic to string");
            }
            else
            {
                MessageBox.Show(arg.ToString(), "generic to not string");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            swapclass<int> sc1 = new swapclass<int>();
            swapclass<double> sc2 = new swapclass<double>();

            string msg = "";
            msg += "原先:N1 = 10 , N2 = 20 \n";
            int n1 = 10;
            int n2 = 20;

            sc1.swap(ref n1, ref n2);
            msg += string.Format("交換後N1 = {0} , N2 = {1}\n", n1, n2);

            msg += "交換前 d1 = 1.25 , d2 = 6.75\n";
            double d1 =1.25;
            double d2 =6.75;
            sc2.swap(ref d1, ref d2);
            msg += string.Format("交換後d1 = {0} , d2 = {1}\n", d1, d2);

            MessageBox.Show(msg, "generic class");
        }
    }

    public class swapclass<T>
    {
        public void swap<T>(ref T p, ref T s)
        {
            T temp;
            temp = p;
            p = s;
            s = temp;
        }
    }
}
