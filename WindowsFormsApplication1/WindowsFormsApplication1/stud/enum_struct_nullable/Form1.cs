using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.stud.enum_struct_nullable
{
    public enum student : byte
    {
        miku = 1 ,
        luka = 2 ,
        lin = 3
    }

    public struct student1  //need public , default private
    {
        public string name;
        public int number;
        public string teacher;
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = " enum ";
            button2.Text = " sturct";
            button3.Text = " nullable";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            byte Num = 0;
            if (comboBox1.Text != "")
            {
                Num = byte.Parse(comboBox1.Text);
            }
            //MessageBox.Show(Num.ToString());

            switch (Num)
            {
                case (byte)student.miku:
                    MessageBox.Show(student.miku.ToString());
                    break;
                case (byte)student.luka:
                    MessageBox.Show(student.luka.ToString());
                    break;
                case (byte)student.lin:
                    MessageBox.Show("lin");
                    break;
                default:
                    MessageBox.Show("The value not in range");
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            student1 st;
            st.name = textBox1.Text;
            st.number = int.Parse(textBox2.Text);
            st.teacher = textBox3.Text;

            MessageBox.Show("name:" + st.name + "\nnumber:" + st.number + "\nteacher:" + st.teacher);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Nullable<int> myname = null;
            // int myname = null 
            /* 此宣告在compile就會error*/
            if (myname==null)
            {
                MessageBox.Show("myname is null");
            }
            else
            {
                MessageBox.Show(myname.ToString());
            }
        }

    }
}
