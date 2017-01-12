using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.async;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceReference1.WebServiceSoapClient ws = new ServiceReference1.WebServiceSoapClient();
            int tmp;
            tmp = ws.HelloWorld4(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            ws.Close();
            label1.Text = tmp.ToString();
            /*
            async1 aaa = new async1();

            aaa.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            aaa.Location = new System.Drawing.Point(this.Left + button1.Right, this.Top + button1.Bottom);
            aaa.ShowDialog();
            */
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
