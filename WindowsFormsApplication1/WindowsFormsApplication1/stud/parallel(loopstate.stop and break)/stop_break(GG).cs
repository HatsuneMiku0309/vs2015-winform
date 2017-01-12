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
    public partial class stop_break : Form
    {
        public stop_break()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sum = 0;
            string msg = "";
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Reset();
            sw.Start();
            object sync = new object();
            Parallel.For(1,1000,(i,Loopstate) =>
            {
                if(i== 255)
                    Loopstate.Break();
                lock(sync)
                {
                    sum += i;
                }
                string buf = string.Format("i={0},sum={1}\n",i,sum);
                msg += buf;
            });
            sw.Stop();
            string parallelfor = sw.Elapsed.TotalMilliseconds.ToString();
            richTextBox1.Text = string.Format("break執行毫秒:{0}. \n {1}",parallelfor,msg);
        }

        private void stop_break_Load(object sender, EventArgs e)
        {
            button1.Text = "break";
            button2.Text = "stop";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sum = 0;
            string msg = "";
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Reset();
            sw.Start();
            object sync = new object();
            Parallel.For(1, 1000, (i, Loopstate) =>
            {
                if (i == 255)
                    Loopstate.Stop();
                lock (sync)
                {
                    sum += i;
                }
                string buf = string.Format("i={0},sum={1}\n", i, sum);
                msg += buf;
            });
            sw.Stop();
            string parallelfor = sw.Elapsed.TotalMilliseconds.ToString();
            richTextBox1.Text = string.Format("stop執行毫秒:{0}. \n {1}", parallelfor, msg);
        }
    }
}
