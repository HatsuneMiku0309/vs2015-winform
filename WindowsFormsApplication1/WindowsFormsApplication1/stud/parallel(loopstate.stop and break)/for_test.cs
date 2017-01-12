using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.stud
{
    public partial class for_test : Form
    {
        public for_test()
        {
            InitializeComponent();
            button1.Text = "for";
            button2.Text = "parallel.for";
        }

        private void for_test_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sum = 0;
            
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Reset();
            sw.Start();
            object sync = new object();
            Parallel.For(1, 10001, new ParallelOptions { MaxDegreeOfParallelism = 4 }, (i) =>
            {
                lock (sync)
                {
                    sum += i;
                }
                
            });
            sw.Stop();
            string parallelfor = sw.Elapsed.TotalMilliseconds.ToString();
            MessageBox.Show(string.Format("平行for:{0}. \n {1}", parallelfor, sum.ToString()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
                        int sum = 0;
            string msg = "";
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            sw.Reset();
            sw.Start();
            for (int i = 1; i <= 10000; i++)
            {
                sum += i;
                string buf = string.Format("i={0},sum={1}", i, sum);
                msg += buf;
            }
            sw.Stop();
            string forresult = sw.Elapsed.TotalMilliseconds.ToString();
            MessageBox.Show(string.Format("for:{0}毫秒.\n {1}",forresult,sum.ToString()));
        }
    }
}
