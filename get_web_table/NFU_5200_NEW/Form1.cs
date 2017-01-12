using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NFU_5200_NEW.NFU_Service; // webservice
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NFU_5200_NEW
{
    public partial class Form1 : Form
    {

        // 搜尋未使用的訂單 return
        string orderData;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string s = new string[2];
            SimpleService svc = new SimpleService();
            string s = svc.gethelloworld("steve","gg");
            MessageBox.Show(s);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SimpleService s = new SimpleService();
            NFU_Service.MySoapObject2[] ms = new NFU_Service.MySoapObject2[2];
            for (int i = 0; i < ms.Length; i++)
            {            
                ms[i] = new NFU_Service.MySoapObject2();
                ms[i].Author2 = "中文";
                ms[i].Description2 = "The One that should be returned";
                ms[i].Name2 = i.ToString();
                ms[i].Text2 = "something something something " + i.ToString();
                ms[i].VoteCount2 = i * 2;
                ms[i].VoteTotal2 = (int)Math.Pow(ms[i].VoteCount2, 2);
            }

            NFU_Service.MySoapObject retn = s.ProcessMySoapObject(ms);
            // if output ArrayOfString
            // string[] retn2 = s.ProcessMySoapObject(ms);
            string output = "";
            output += retn.Author + "\t\t\r\n";
            output += retn.Description + "\t\t\r\n";
            output += retn.Name + "\t\t\r\n";
            output += retn.Text + "\t\t\r\n";
            output += retn.VoteCount.ToString() + "\t\t\r\n";
            output += retn.VoteTotal.ToString() + "\t\t\r\n";
            MessageBox.Show(output);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SimpleService svc = new SimpleService();
            string s = svc.testtest1("steve");
            MessageBox.Show(s);
        }

        // 搜尋未使用的訂單
        private void button4_Click(object sender, EventArgs e)
        {
            bool success = false;
            string message = "";
            SimpleService nfu_service = new SimpleService();
            orderData = nfu_service.OrderSearchNotUse(out success,out message);
            if (success)
            {
                /*
                Order m = JsonConvert.DeserializeObject<Order>(orderData); // 部分反序列化
                string name = m.id;
                MessageBox.Show(name);
                */

                JObject LinqSearch = JObject.Parse(orderData); // {} 物件json LINQ查詢

                
                MessageBox.Show(LinqSearch["id"].ToString());
            }
            else {
                MessageBox.Show(message);
            }
        }


    }

    public class Order
    {
        public int id { set; get; }
    }
}
