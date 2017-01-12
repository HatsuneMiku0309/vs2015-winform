using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class TestProject : Form
    {
        public TestProject()
        {
            InitializeComponent();
        }

        private void TestProject_Load(object sender, EventArgs e)
        {
            contextMenuStrip1.Items.Add("關閉檔案");
            notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;


            //  menustrip抓取一階item and Add newItem 
           
            ToolStripMenuItem tsmil1_5 = new ToolStripMenuItem("結束", null, new EventHandler(tsmil1_5_Click));
            //tsmil1_5.Checked = true;
            ToolStripMenuItem tsmil1 = (ToolStripMenuItem)menuStrip1.Items[0];
            tsmil1.DropDownItems.Add(tsmil1_5);

            //再menu內新增ProgressBar
            //ProgressBar只能通過程式碼新增
            //進階項目的item都可以通過ToolStripXXXXXX物件實作塞入容器
            //MenuStrip , ContextMenuStrip , StatusStrip
            /*
            ToolStripProgressBar tsmil1_6 = new ToolStripProgressBar("test");
            tsmil1.DropDownItems.Add(tsmil1_6);
            */

            //抓取一階所有item
            /*
            int test = 0;
            foreach (var items in menuStrip1.Items)
            //foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                ToolStripMenuItem item = (ToolStripMenuItem)items;
                if (item.Text=="檔案")
                {
                    item.DropDownItems.Add(tsmil1_5);
                    test++;
                }
            }
            */


            //抓取一階所有item及二階所有item (排除ToolStripSeparator)
            /*
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                for(int i = 0 ; i < item.DropDownItems.Count ; i++){
                    object obj = item.DropDownItems[i];
                    if(obj.GetType() != typeof(ToolStripSeparator)){
                        continue;
                    }
                }
            }
            */
        }

        private void tsmil1_5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            string test = "123456";

            notifyIcon1.BalloonTipTitle = "test";
            notifyIcon1.BalloonTipText = test;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.Text = "GGG";
            notifyIcon1.ShowBalloonTip(5000);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            MessageBox.Show(e.ClickedItem.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmil4 = new ToolStripMenuItem("新增的");

            ToolStripMenuItem tsmil4_1 = new ToolStripMenuItem("新增的1");
            ToolStripMenuItem tsmil4_2 = new ToolStripMenuItem("新增的2");

            menuStrip1.Items.Add(tsmil4);

            tsmil4.DropDownItems.AddRange(new ToolStripMenuItem[]{ tsmil4_1 , tsmil4_2});
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                menuStrip1.Items.RemoveAt(3);
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string msg = "";
            foreach (ToolStripMenuItem items in menuStrip1.Items) {
                msg = msg + items.Text + " : ";
                foreach (ToolStripItem item in items.DropDownItems)
                {
                    msg = msg + item.Text + " ";
                }
                msg += "\n\r";
            }
            MessageBox.Show(msg);
        }

    }
}
