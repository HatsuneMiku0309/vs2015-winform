using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using studyCsharp_network;

namespace MyIPAndDNSProcess
{
    //public partial class IPProcess // partial class is not need New Class
    public partial class IPAndDNSProcess // static class is not need New Class
    {
        public static void StringIP()
        {
            IPAddress ipAddr1 = IPAddress.Parse("127.0.0.1");
            IPAddress ipAddr2 = IPAddress.Parse("127.0.0.2");

            if (ipAddr1.Equals(ipAddr2))
            {
                MessageBox.Show("test");
            }
            else {
                MessageBox.Show("error");
            }
        }

        public static void BytesIP()
        {
            //IPAddress ipAddr1 = IPAddress.Parse("127.0.0.1"); // IPv4
            IPAddress ipAddr1 = IPAddress.Parse("::1"); // IPv6
            byte[] bytes = ipAddr1.GetAddressBytes();

            for (int i = 0; i < bytes.Length; i++)
            {
                Console.WriteLine(bytes[i]);
            }
        }

        public static void IsLoopback()
        {
            IPAddress ipAddr1 = IPAddress.Parse("::1"); // IPv6

            if (IPAddress.IsLoopback(ipAddr1) && ipAddr1.AddressFamily == AddressFamily.InterNetwork)
            {
                MessageBox.Show("IPv4: is loopback");
            }
            else if (IPAddress.IsLoopback(ipAddr1) && ipAddr1.AddressFamily == AddressFamily.InterNetworkV6)
            {
                MessageBox.Show("IPv6: is loopback");
            }
            else {
                MessageBox.Show("not any");
            }
        }

        public static void fIPEndPoint()
        {
            try
            {
                IPAddress ipAddr1 = IPAddress.Parse("127.0.0.1");
                IPEndPoint IPEndPoint = new IPEndPoint(ipAddr1, Int32.Parse("80"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void fSocketAddress()
        {
            IPAddress ipAddr1 = IPAddress.Parse("127.0.0.1");
            IPAddress ipAddr2 = IPAddress.Parse("::1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr1, 80);

            SocketAddress SocketAddr = ipEndPoint.Serialize();

            MessageBox.Show(SocketAddr.ToString());
        }

        public static void fGetHostByAddress()
        {
            try
            {
                // DNS 設定於 C:\Windows\System32\drivers\etc
                IPAddress IPAddr1 = IPAddress.Parse("192.168.10.100"); // 127.0.0.1 (由於DNS都指於127.0.0.1，故192.168.10.100不會有Aliases)
                IPHostEntry IPHost = Dns.GetHostByAddress(IPAddr1); // 此方法於 .net 2.0 後被不建議使用

                string IPAddr = "";
                string DNS = "";
                string HostName = IPHost.HostName;
                string Result = "";

                foreach (var item in IPHost.AddressList)
                {
                    IPAddr += item.ToString() + "\n\r";
                }

                foreach (var item in IPHost.Aliases)
                {
                    DNS += item.ToString() + "\n\r";
                }

                Result = "我的IP位置: " + IPAddr + "\n\r" + "我的網域名稱(DNS): \n\r" + DNS + "\n\r" + "我的電腦名稱: " + HostName;

                MessageBox.Show(Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public static void fGetHostByEntry()
        {
            try
            {
                IPAddress IPAddr1 = IPAddress.Parse("175.99.86.191"); // 127.0.0.1 此查詢不管怎樣Aliases都為空
                IPHostEntry IPHost = Dns.GetHostEntry(IPAddr1);
                string MyHostName = Dns.GetHostName();
                //IPHostEntry IPHost = Dns.GetHostEntry(MyHostName);
                //IPHostEntry IPHost = Dns.GetHostEntry("tw.yahoo.com");

                string IPAddr = "";
                string DNS = "";
                string HostName = IPHost.HostName;
                string Result = "";

                foreach (var item in IPHost.AddressList)
                {
                    IPAddr += item.ToString() + "\n\r";
                }

                foreach (var item in IPHost.Aliases)
                {
                    DNS += item.ToString() + "\n\r";
                }

                Result = "我的IP位置: " + IPAddr + "\n\r" + "我的網域名稱(DNS): \n\r" + DNS + "\n\r" + "我的電腦名稱: " + HostName;

                MessageBox.Show(Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public static void fResolve(string hostName)
        {
            try
            {
                IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
                //IPHostEntry IPHost = Dns.GetHostEntry(hostName);
                IPHostEntry IPHost = Dns.Resolve(hostName);

                string IPAddr = "";
                string DNS = ""; // 永久為空
                string HostName = IPHost.HostName;
                string Result = "";

                foreach (var item in IPHost.AddressList)
                {
                    IPAddr += item.ToString() + "\n\r";
                }

                foreach (var item in IPHost.Aliases)
                {
                    DNS += item.ToString() + "\n\r";
                }

                Result = "我的IP位置: " + IPAddr + "\n\r" + "我的網域名稱(DNS): \n\r" + DNS + "\n\r" + "我的電腦名稱: " + HostName;
                
                MessageBox.Show(Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        /*
            非同步需新增一個"線程(Thread)" 去處理。
        */
        public static System.Threading.ManualResetEvent allDone = null;

        public void fBeginResolve(string HostName)
        {
            try
            {
                allDone = new System.Threading.ManualResetEvent(false);

                // 建立 State 物件
                RequestState State = new RequestState();

                // 開始ansyc 解析 DNS
                // 定義Callback 方法為 ResponseCallback
                IAsyncResult Result = Dns.BeginResolve(HostName, new AsyncCallback(ResponseCallback), State);

                
                int i = 0;
                while (State.host == null)
                {
                    Console.WriteLine(i.ToString());
                    ++i;
                }

                // Wait until asynchronous call completes.
                //allDone.WaitOne();
                

                string IPAddr = "";
                string DNS = ""; // 永久為空
                string hostName = State.host.HostName;
                string result = "";

                foreach (var item in State.host.AddressList)
                {
                    IPAddr += item.ToString() + "\n\r";
                }

                foreach (var item in State.host.Aliases)
                {
                    DNS += item.ToString() + "\n\r";
                }
                
                result = "我的IP位置: " + IPAddr + "\n\r" + "我的網域名稱(DNS): \n\r" + DNS + "\n\r" + "我的電腦名稱: " + HostName;
                
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void ResponseCallback(IAsyncResult Result)
        {
            try
            {
                RequestState State = (RequestState)Result.AsyncState;

                //非同步解析 DNS 資訊
                State.host = Dns.EndResolve(Result);
                allDone.Set();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }

    // 非同步取得主機資訊
    class RequestState
    {
        public IPHostEntry host;

        public RequestState()
        {
            host = null;
        }
    }


}
