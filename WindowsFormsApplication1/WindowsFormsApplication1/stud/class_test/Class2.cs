using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialClasses_ex
{
    partial class Human
    {
        public int Age
        {
            get;
            set;
        }

        public string ToWalk()
        {
            return "正在走路中...";
        }
    }
}
