using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; //add namespace 為了 message_box

namespace partialmethods_ex
{
    partial class sporcar
    {
        partial void getcarname()
        {
            MessageBox.Show("test");
        }
    }
}
