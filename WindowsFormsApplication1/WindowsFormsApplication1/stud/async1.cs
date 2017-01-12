using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading;

namespace WindowsFormsApplication1.async
{
    public partial class async1 : Form
    {
        public async1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            int contentcount = await GetWebAsync();

            richTextBox1.Text += string.Format("\r\n網頁資料或許完畢，下載字元數共:{0} 個.\r\n", contentcount);

        }

        async Task<int> GetWebAsync()
        {
            HttpClient client = new HttpClient();
            var URL = (textBox1.Text == "") ? " http://www.google.com.tw" : textBox1.Text;
            
            Task<string> getStringTask = client.GetStringAsync(URL);
            
            NoWaitingWork();
            
            
            string urlChars = await getStringTask;

            return urlChars.Length;
        }

        private void NoWaitingWork()
        {
            richTextBox1.Text += "非同步工作進行中...\r\n";
        }
   
    }
}
