using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.stud.iterator_test
{
    public partial class iterator_test : Form
    {
        public iterator_test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyBook bookcollections = new MyBook();
            string msg = "";

            string[] gg = { "0", "1", "2", "3" };

            //foreach (var book in gg)
            foreach (var book in bookcollections)
            {
                msg += book + "\n";
            }
            MessageBox.Show(msg, "iterator測試(yield追蹤)");
        }
    }

    public class MyBook : System.Collections.IEnumerable
    {
        public System.Collections.IEnumerator GetEnumerator()
        {
            yield return "1. gg1";
            yield return "2. gg2";
            yield return "3. gg3";
            yield return "4. gg4";
        }
    }
}
