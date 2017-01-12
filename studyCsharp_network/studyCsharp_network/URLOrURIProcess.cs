using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace MyUrlAndUriAndCookieProcess
{
    public partial class  UrlAndUriAndCookieProcess
    {
        public static void fEquals()
        {
            Uri uri1 = new Uri("http://www.yahoo.com/tw");
            Uri uri2 = new Uri("http://www.yahoo.com");

            if (uri1.Equals(uri2))
            {
                MessageBox.Show("相同");
            }
            else {
                MessageBox.Show("不相同");
            }

        }

        public static void fMakeRelativeUri()
        {
            try
            {
                Uri uri1 = new Uri("http://www.yahoo.com/tw");
                Uri uri2 = new Uri("http://www.yahoo.com");

                MessageBox.Show(uri1.MakeRelativeUri(uri2).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void UriProperty()
        {
            //Uri uri1 = new Uri("https://tw.yahoo.com/"); // http: 80 , https: 443
            //Uri uri1 = new Uri("http://114.35.57.190:889/sweet/"); // The port not default port

            Uri uri1 = new Uri("https://www.youtube.com/watch?v=A7wqt B4vX-o"); // 會將Escaped字元轉換ASCII編碼
            //Uri uri1 = new Uri("https://www.youtube.com/watch?v=A7wqt B4vX-o",true); // 不會將Escaped字元轉換ASCII編碼
            string Result = "";
            string HostNameType = "";

            Result += "AbsolutePath(URI相對路徑): " + uri1.AbsolutePath + "\r\n";
            Result += "AbsoluteUri(URI絕對路徑): " + uri1.AbsoluteUri + "\r\n";
            Result += "Authority(主機名稱、IP、Port): " + uri1.Authority + "\r\n";
            Result += "Host(主機名稱、IP): " + uri1.Host + "\r\n";
            Result += "Port(Port): " + uri1.Port + "\r\n";
            Result += "LocalPath(本機路徑): " + uri1.LocalPath + "\r\n";
            Result += "IsDefaultPort(預設port): " + uri1.IsDefaultPort + "\r\n"; // 80 及 443 皆為default
            Result += "IsFile(檔案Uri): " + uri1.IsFile + "\r\n";
            Result += "PathAndQuery(問號分隔AbsolutePath及Query): " + uri1.PathAndQuery + "\r\n";
            Result += "Query(資源參數): " + uri1.Query + "\r\n";
            Result += "Scheme(通訊協定): " + uri1.Scheme + "\r\n";
            Result += "UserEscaped(使用逸出): " + uri1.UserEscaped + "\r\n"; // 於Uri物件創立時 dontEscape 為 true 時為 treu，否則為false
            Result += "UserInfo: " + uri1.UserInfo + "\r\n";

            switch (uri1.HostNameType) {
                case UriHostNameType.Basic:
                    HostNameType = "Basic";
                    break;
                case UriHostNameType.Dns:
                    HostNameType = "Dns";
                    break;
                case UriHostNameType.IPv4:
                    HostNameType = "IPv4";
                    break;
                case UriHostNameType.IPv6:
                    HostNameType = "IPv6";
                    break;
                case UriHostNameType.Unknown:
                    HostNameType = "Unknown";
                    break;
                default:
                    HostNameType = "例外";
                    break;
            }

            //Result += "HostNameType: " + uri1.HostNameType.ToString() + "\r\n";
            Result += "HostNameType: " + HostNameType + "\r\n";

            MessageBox.Show(Result);

        }

        public static void fCookie()
        {
            Cookie cookie = new Cookie("name", "HatsuneMiku");
            MessageBox.Show(cookie.Name);
        }
    }
}
