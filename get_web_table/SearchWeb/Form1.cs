using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

namespace SearchWeb
{
    public partial class Form1 : Form
    {
        int type = 0;
        int webtype = 1; //1=google 2=yahoo
        int searchtype = 1;
        int stop = 0;
        int index = 1;  //is page
        string[] substr;
        string nowwwstr = "";
        int button_pn;

        int searchcount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Height = this.Height;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            textBox1.Visible = false;
            //label4.Visible = false;
            label3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            if (searchcount < 100)  //從資料庫撈取總數
            {
                if (int.Parse(textBox4.Text) * 1000 < 10000)
                {
                    MessageBox.Show("時間設定請勿小於10秒");
                }
                else
                {
                    timer2.Interval = int.Parse(textBox4.Text) * 1000;
                    substr = textBox3.Text.Split('.');
                    for (int i = 1; i < substr.Length; i++)
                    {
                        if (i == 1)
                        {
                            nowwwstr += substr[i];
                        }
                        else
                        {
                            nowwwstr += "." + substr[i];
                        }
                    }

                    if (textBox2.Text != "" && textBox3.Text != "" && webtype == 1)
                    {
                        index = 1;
                        type = 1;
                        searchtype = 1;
                        stop = 0;
                        webBrowser1.Navigate("https://www.google.com.tw/");
                    }
                    else if (textBox2.Text != "" && textBox3.Text != "" && webtype == 2)
                    {
                        webtype = 2;
                        index = 1;
                        type = 1;
                        searchtype = 1;
                        button_pn = 0;
                        stop = 0;
                        webBrowser1.Navigate("https://tw.yahoo.com/");
                    }
                    else
                    {
                        MessageBox.Show("搜尋欄位及條件不可為空");
                    }
                    searchcount++;
                }
            }
            else
            {
                timer2.Enabled = true;
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (type == 1)
            {
                button6_Click(this, e);
            }
            if (button_pn == 1)
            {
                button_pn = 0;
                button4_Click(this, e);
            }
            
        }

        private void SaveData(string web_page,string web_item,string web_type,string web_search,string web_condition)
        {
            string strconn = ConfigurationManager.ConnectionStrings["SearchWeb.Properties.Settings.websearchConnectionString"].ToString();
            SqlCommand cmd;

            SqlConnection sqlconn = new SqlConnection(strconn);
            sqlconn.Open();

            string sql = "insert into [websearch].[dbo].[search] (web_page,web_item,web_type,web_search,web_condition) values (@web_page,@web_item,@web_type,@web_search,@web_condition)";
            cmd = new SqlCommand(sql, sqlconn);
            cmd.Parameters.Add("@web_page", SqlDbType.NChar).Value = web_page;
            cmd.Parameters.Add("@web_item", SqlDbType.NChar).Value = web_item;
            cmd.Parameters.Add("@web_type", SqlDbType.NChar).Value = web_type;
            cmd.Parameters.Add("@web_search", SqlDbType.NChar).Value = web_search;
            cmd.Parameters.Add("@web_condition", SqlDbType.NChar).Value = web_condition;
            cmd.ExecuteNonQuery();

            sqlconn.Close();
            sqlconn.Dispose();
            cmd.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HtmlElement element;
            if (webtype == 1)
            {
                element = webBrowser1.Document.GetElementById("tsf");
                element.InvokeMember("submit");
                timer1.Interval = 2000;
            }
            else if (webtype == 2)
            {
                element = webBrowser1.Document.GetElementById("UHSearch");
                element.InvokeMember("submit");
            }
            timer1.Enabled = true;

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            int item = 0;  //第一項
            HtmlElementCollection elementlist;
            if (type == 0 && webtype == 1 && stop != 1)
            {
                elementlist = webBrowser1.Document.GetElementsByTagName("div");
                foreach (HtmlElement curhtml in elementlist)
                {
                    await Task.Delay(100);
                    label4.Text = curhtml.GetAttribute("classname").ToString();
                    if (curhtml.GetAttribute("classname").ToString() == "f kv _SWb")
                    {
                        item++;
                        //richTextBox1.AppendText(item.ToString()+" "+ curhtml.GetAttribute("classname").ToString() +"\n");
                        //label4.Text = item.ToString();
                        HtmlElement citehtml = curhtml.GetElementsByTagName("cite")[0];
                        label3.Text = item.ToString();
                        if (citehtml.GetAttribute("classname").ToString() == "_Rm")
                        {
                            if ((citehtml.InnerHtml.IndexOf(nowwwstr) >= 0) || (citehtml.InnerHtml.IndexOf(textBox3.Text) >= 0))
                            {
                                stop = 1; 
                                textBox1.Text = citehtml.InnerHtml;
                                SaveData(index.ToString(), item.ToString(), "google", textBox2.Text, textBox3.Text);
                                nowwwstr = "";
                                break;
                            }
                        }
                    }
                }
            }
            else if (type == 0 && webtype == 2 && stop != 1)
            {
                elementlist = webBrowser1.Document.GetElementsByTagName("ol");
                foreach (HtmlElement curhtml in elementlist)
                {
                    await Task.Delay(100);
                    if (curhtml.GetAttribute("classname").ToString() == " reg searchCenterMiddle")
                    {
                        HtmlElementCollection lihtmllist = curhtml.GetElementsByTagName("li");
                        foreach (HtmlElement lihtml in lihtmllist)
                        {
                            await Task.Delay(100);
                            item++;
                            HtmlElementCollection spanlist = lihtml.GetElementsByTagName("span");
                            foreach (HtmlElement spanhtml in spanlist)
                            {
                                if (spanhtml.GetAttribute("classname").ToString() == " fz-ms fw-m fc-12th wr-bw")
                                {
                                    if (spanhtml.InnerHtml.IndexOf(textBox3.Text) >= 0 || spanhtml.InnerHtml.IndexOf(nowwwstr) >= 0)
                                    {
                                        stop = 1;
                                        SaveData(index.ToString(), item.ToString(), "yahoo", textBox2.Text, textBox3.Text);
                                        nowwwstr = "";
                                        break;
                                    }
                                }
                            }
                            if (stop == 1)
                            {
                                break;
                            }
                        }
                    }
                    if (stop == 1)
                    {
                        break;
                    }
                }
            }

            if (webtype == 1 && stop != 1 && (index < 10))
            {   
                index++;
                await Task.Delay(200);
                button5_Click(this, e);
            }
            else if (webtype == 1 && stop != 1 && index >= 10)
            {
                webtype = 2;
                nowwwstr = "";
                button1_Click(this, e);
            }
            else if (webtype == 2 && stop != 1 && index < 10)
            {
                index++;
                await Task.Delay(200);
                button5_Click(this, e);
            }
            else if (webtype == 2 && stop != 1 && index >= 10)
            {
                stop = 1;
                webtype = 1;
                timer2.Enabled = true;
            }
            if ((stop == 1) && (webtype==1))
            {
                webtype = 2;
                button1_Click(this, e);
            }else if ((stop == 1) && (webtype==2))
            {
                webtype = 1;
                nowwwstr = "";
                timer2.Enabled = true;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            HtmlElementCollection elementlist;
            button_pn = 1;
            if (webtype == 1)
            {
                elementlist = webBrowser1.Document.GetElementsByTagName("a");
                foreach (HtmlElement curhtml in elementlist)
                {
                    if (curhtml.GetAttribute("id").ToString() == "pnnext")
                    {
                        curhtml.InvokeMember("click");
                        break;
                    }
                }
            }
            else if (webtype == 2)
            {
                elementlist = webBrowser1.Document.GetElementsByTagName("a");
                foreach (HtmlElement curhtml in elementlist)
                {
                    if (curhtml.GetAttribute("classname").ToString() == "next")
                    {
                        curhtml.InvokeMember("click");
                        break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://my-user-agent.com/");
            type = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (searchtype == 1)
            {
                type = 0;
                timer1.Enabled = false;
                searchtype = 2;
                button3_Click(this, e);
            }
            else if (searchtype == 2)
            {
                timer1.Enabled = false;
                button4_Click(this, e);
            }      
        }

        private void button6_Click(object sender, EventArgs e)
        {
            type = 0;
            if (webtype == 1)
            {
                HtmlElement element;
                element = webBrowser1.Document.GetElementById("lst-ib");
                element.InnerText = textBox2.Text;
                searchtype = 1;
                timer1.Interval = 1000;
            }
            else if (webtype == 2)
            {
                button8_Click(this, e);
                timer1.Interval = 10000;
            }
            timer1.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://tw.yahoo.com/");
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            HtmlElement element;
            element = webBrowser1.Document.GetElementById("UHSearchBox");
            element.InnerText = textBox2.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            HtmlElementCollection elementlist;
            elementlist = webBrowser1.Document.GetElementsByTagName("a");
            foreach (HtmlElement curhtml in elementlist)
            {
                if (curhtml.GetAttribute("classname").ToString() == "next")
                {
                    curhtml.InvokeMember("click");
                    break;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            HtmlElement element;
            element = webBrowser1.Document.GetElementById("UHSearch");
            element.InvokeMember("submit");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            searchcount = 0;
            button1_Click(sender, e);
            timer2.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
