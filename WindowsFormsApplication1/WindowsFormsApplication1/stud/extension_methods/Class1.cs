using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace char_extensionmethods //namespace
{
    public static class mycharmethods
    {
        ///<summary>
        ///get char length
        ///</summary>
        ///<param name="c">字元參數</param>
        ///<returns>回傳整數值</returns>

        public static int getlength(this char c)
        {
            return c.ToString().Length;
        }
    }
}
