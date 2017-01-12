using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices; //使用 VB.app namespace  //my

namespace WindowsFormsApplication1.stud.references_VB_my_class
{
    public partial class VB_my_class : Form
    {
        public VB_my_class()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User myuser = new User();
            myuser.InitializeWithWindowsUser();
            var indexpath = myuser.Name.IndexOf(@"\");  //擷取字串 "\" 符號 位置
                          //myuser.name.tostring(); 
            MessageBox.Show(indexpath.ToString(), "使用者名稱");
            string username = myuser.Name.Substring(indexpath+1); //字串擷取起始點(取得indexOF)為該"\"符號起始點+1可捨去
            MessageBox.Show(username, "使用者名稱");
        }

        private void VB_my_class_Load(object sender, EventArgs e)
        {
            button1.Text = "my取得使用者名稱";

        }

    }
}
