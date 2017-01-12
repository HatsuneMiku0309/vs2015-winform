using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studyCsharp_network
{
    interface Itest
    {
        void ggtest(string msg);
    }

    interface IPoint
    {
        // Property signatures:
        int x
        {
            get;
            set;
        }

        int y
        {
            get;
            set;
        }
    }
}
