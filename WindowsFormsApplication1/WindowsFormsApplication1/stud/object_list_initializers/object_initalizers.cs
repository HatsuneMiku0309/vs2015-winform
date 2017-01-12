using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.stud.object_initalizers
{
    public partial class object_initalizers : Form
    {
        public object_initalizers()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            student dai = new student { name = "戴智宣", course = "C#", score = 100 };
            string msg = "";

            msg += "名子:" + dai.name + "\n";
            msg += "學習:" + dai.course + "\n";
            msg += "分數:" + dai.score + "\n";
            MessageBox.Show(msg, "class 初始設定");
        }

        private void object_initalizers_Load(object sender, EventArgs e)
        {
            button1.Text = "顯示";
        }
    }

    public class student
    {
        public string name
        {
            set;
            get;
        }

        public string course
        {
            set;
            get;
        }

        public int score
        {
            set;
            get;
        }
    }
}
