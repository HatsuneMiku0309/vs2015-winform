using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

using MyIPAndDNSProcess; // namesapce
using MyUrlAndUriAndCookieProcess; // namesapce
using MyWebRequestAndResponse; // namesapce


namespace studyCsharp_network
{
    partial class Main : Form
    {
        public Main()
        {  
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //IPAndDNSProcess IPAndDNS = new IPAndDNSProcess();
            // IPAndDNSProcess.IPProcess.StringIP(); // IPProcess namespace and static class is not need New Class

            //IPAndDNSProcess.StringIP(); // static class is not need New Class        
            //IPAndDNSProcess.BytesIP();
            //IPAndDNSProcess.IsLoopback();
            //IPAndDNSProcess.fIPEndPoint();
            //IPAndDNSProcess.fSocketAddress();

            /* 兩者差別於 
                GetHostByAddress 只能給與 IP 可查詢 所有IP 及 Aliases 及 主機名稱 
                GetHostByEntry 給予IP 或 主機名稱 或 Aliases 及可查詢 IP 及 Aliases 及 主機名稱
            */
            //IPAndDNSProcess.fGetHostByAddress();
            //IPAndDNSProcess.fGetHostByEntry();

            //IPAndDNSProcess.fResolve(textBox1.Text);
            //IPAndDNS.fBeginResolve(textBox1.Text);

            /*
                under is url and uri process
            */
            //UrlAndUriAndCookieProcess.fEquals();
            //UrlAndUriAndCookieProcess.fMakeRelativeUri();
            //UrlAndUriAndCookieProcess.UriProperty();
            //UrlAndUriAndCookieProcess.fCookie();

            /*
                under is request and response
            */
            //WebRequestAndResponse.fWebRequestCreate(textBox1.Text);
            //WebRequestAndResponse.fAnsycWebRequest(textBox1.Text);

            //WebClientTesting Client = new WebClientTesting();
            //WebClientTesting.fDownloadData(textBox1.Text);
            //Client.fDownloadFile(textBox1.Text);
            //WebClientTesting.fUploadData(textBox1.Text);
            //WebClientTesting.fUploadFile(textBox1.Text);
            WebClientTesting.fAsyncDownloadFile(this.progressBar1,textBox1.Text);
        }

    }


    /* 以下為測試interface用 */
    public class test : Itest
    {
        public void ggtest(string msg)
        {   
            throw new NotImplementedException();
        }
    }

    public class Point : IPoint
    {
        // Fields:
        private int _x;
        private int _y;

        // Constructor:
        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int x
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        public int y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }
        }
    }
}
