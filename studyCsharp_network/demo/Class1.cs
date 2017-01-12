using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo2
{
    public partial class Class1
    {
        public TextBox textbox_form1 = new TextBox();

        public void textbox()
        {
            demo.Form1 test = new demo.Form1();
            textbox_form1.Text = "ggtest";
        }
    }
}
