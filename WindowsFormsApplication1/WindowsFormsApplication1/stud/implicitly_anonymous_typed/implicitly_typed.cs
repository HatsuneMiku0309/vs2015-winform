using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.stud.implicitly_typed
{
    public partial class implicitly_typed : Form
    {
        public implicitly_typed()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox33.Text=="")
            {
                MessageBox.Show("請輸入關鍵字");
                textBox33.Focus();
            }
            else
            {
                string[] students = { "dai", "chan", "lin", "miku" };
                //此處 var 編譯器會轉為 IEnumerable<int> 型別
                var stdlinq =
                    from student in students
                    where student.IndexOf(textBox33.Text) != -1
                    select student;

                string msg = "";
                int count = 0;
                
                Object sync = new Object();

               
                Parallel.ForEach(stdlinq, new ParallelOptions { MaxDegreeOfParallelism = 4 }, (people) =>
                {

                    lock (sync)
                    {
                        msg += people+"\n";
                        count++;
                    }

                });
                
                /*
                foreach (var people in stdlinq)
                {
                    msg += people + "\n";
                    count++;
                }
                */

                if (count == 0)
                {
                    MessageBox.Show("no data");
                }
                else
                {
                    MessageBox.Show("共找到" + count.ToString() + "筆資料:\n" + msg);
                }

            }

        }
    }
}
