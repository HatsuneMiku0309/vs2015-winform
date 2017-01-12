using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using mshtml;
using nfu_5200.NFU_Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace nfu_5200
{
    public partial class Form1 : Form
    {
        bool isData = false;

        // 搜尋未使用的訂單json資料
        string orderDataJson;

        // 登入失敗或取不到服務
        bool repeat = false;
        int CheckLoginMaxCount = 5;
        int CheckLoginCount = 0;

        int count = 0; //練習讀取順序塞入
        int testcount = 1; //考試填入順序塞入 (1~10)radio  (11~20)text
        string[] radio = new string[10];
        string[] radioans = new string[10];
        string[] textans = new string[10];
        string[] text = new string[10];

        int type = 0; //進行步驟
        int testtype = 0; //考試步驟

        int totalcoutn = 1;
        public Form1()
        {
            InitializeComponent();
        }

        // 禁入英文網頁
        private async void button1_Click(object sender, EventArgs e)
        {            
            webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_home.asp");
            if (!repeat)
            {
                isData = SearchOrderNotUse(); // 搜尋已繳費及未完成之訂單info帳號未使用及以驗證
            }

            //將帳密輸入
            HtmlElement element;
            HtmlElementCollection elementlist;
            try
            {
                if (isData)
                {
                    JObject LinqSearchOrder = JObject.Parse(orderDataJson); // {} 物件json LINQ查詢
                    await Task.Delay(1000);
                    element = this.webBrowser1.Document.All.GetElementsByName("cust_id")[0];
                    element.InnerText = LinqSearchOrder["nfu_acc"].ToString();

                    element = this.webBrowser1.Document.All.GetElementsByName("cust_pass")[0];
                    element.InnerText = LinqSearchOrder["nfu_pass"].ToString();

                    elementlist = webBrowser1.Document.GetElementsByTagName("input");
                    foreach (HtmlElement InputElement in elementlist)
                    {
                        //MessageBox.Show(InputElement.GetAttribute("type").ToString());
                        if (InputElement.GetAttribute("type").ToString() == "image")
                        {
                            await Task.Delay(1000);
                            InputElement.InvokeMember("click");
                            await Task.Delay(5000);
                            bool islogin = AccIsLogin();
                            if (islogin)
                            {
                                webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/index.asp?tid=1096");
                                await Task.Delay(5000);
                                element = this.webBrowser1.Document.GetElementsByTagName("a")[5];
                                element.InvokeMember("click");

                                if (checkBox1.Checked)
                                {
                                    await Task.Delay(5000);
                                    button2_Click(this, e);
                                }
                                //MessageBox.Show(element.GetAttribute("href"));
                            }

                            // 登入失敗之重複登入
                            if (repeat)
                            {
                                // 限定最多次數重登，超過者換下一位
                                if (CheckLoginCount < CheckLoginMaxCount)
                                {
                                    await Task.Delay(5000);
                                    button1_Click(this, e);
                                }
                                else {
                                    await Task.Delay(5000);
                                    repeat = false;
                                    button1_Click(this, e);
                                }
                            }
                        }
                    }
                }
                else {
                    await Task.Delay(1000);
                    button1_Click(this, e);
                }
            }
            catch(Exception ex)
            {                
                webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_home.asp");
                richTextBox1.AppendText("登入錯誤！");
                type = 0;
                count = 0;    

                if (isData)
                {
                    JObject LinqSearchOrder = JObject.Parse(orderDataJson); // {} 物件json LINQ查詢

                    //登入失敗,訂單狀態-1(失敗),單字量(0)
                    ChangeOrderInfoStatus(Int32.Parse(LinqSearchOrder["id"].ToString()), 0, -1, 0);

                    //order_log 紀錄登入失敗
                    OrderLogSave(Int32.Parse(LinqSearchOrder["order_id"].ToString()), Int32.Parse(LinqSearchOrder["id"].ToString()), "登入失敗");
                }
                
            }
        }

        // 確認登入
        private bool AccIsLogin () 
        {

            HtmlElementCollection elementlist;
            CheckLoginCount++;
            try
            {                
                JObject LinqSearchOrder = JObject.Parse(orderDataJson); // {} 物件json LINQ查詢

                elementlist = webBrowser1.Document.GetElementsByTagName("span");
                foreach (HtmlElement SpanElement in elementlist)
                {
                    if (SpanElement.GetAttribute("classname").ToString() == "cyan")
                    {
                        //order_log 紀錄登入成功
                        OrderLogSave(Int32.Parse(LinqSearchOrder["order_id"].ToString()), Int32.Parse(LinqSearchOrder["id"].ToString()), "登入成功");
                        richTextBox1.AppendText("登入檢查，登入成功！\n");
                        return true;
                    }
                }

                webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_home.asp");
                richTextBox1.AppendText("登入檢查，登入錯誤！\n");
                type = 0;
                count = 0;

                //登入失敗,訂單狀態-1(失敗),單字量(0)
                ChangeOrderInfoStatus(Int32.Parse(LinqSearchOrder["id"].ToString()), 0, -1, 0);

                //order_log 紀錄登入失敗
                OrderLogSave(Int32.Parse(LinqSearchOrder["order_id"].ToString()), Int32.Parse(LinqSearchOrder["id"].ToString()), "登入失敗(帳號或密碼錯誤)");

                return false;
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText("登入檢查，無法取得服務\n");
                webBrowser1.Navigate("http://140.130.28.17/logout.asp");
                repeat = true;
                return false;
            }
        }

        // 搜尋已繳費及未完成之訂單info帳號未使用及以驗證
        private bool SearchOrderNotUse()
        {
            bool success = false;
            string message = "";
            orderDataJson = "";
            try
            {
                SimpleService nfu_service = new SimpleService();
                orderDataJson = nfu_service.OrderSearchNotUse(out success, out message);
                if (success)
                {
                    JObject LinqSearchOrder = JObject.Parse(orderDataJson); // {} 物件json LINQ查詢
                    //MessageBox.Show(LinqSearchOrder["order_id"].ToString());
                    return true;
                }
                else {
                    richTextBox1.AppendText(message + "\n");
                    return false;
                }
            }
            catch(Exception ex)
            {
                richTextBox1.AppendText("無法取得【搜尋未使用訂單之帳號】服務\n");
                //MessageBox.Show("無法取得【搜尋未使用訂單之帳號】服務");
                return false;
            }
        }

        // 改變訂單使用狀態
        private void ChangeOrderInfoStatus(int id, int NowEnCount, int status, int IsCheck)
        {
            bool debug = false;
            string message = "";
            try
            {
                SimpleService nfu_service = new SimpleService();
                bool isSuccess = nfu_service.OrderChangeOrderInfoStatus(id, NowEnCount, status, IsCheck, out message);
                if (isSuccess)
                {
                    if (debug)
                    {
                        MessageBox.Show("已將訂單info之帳號改為失敗(-1)");
                    }
                }
                else {
                    if (debug)
                    {
                        MessageBox.Show(message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法取得【更改訂單狀態】服務");
            }
        }


        private void OrderLogSave(int order_id, int order_info_id, string message)
        {
            try
            {
                SimpleService nfu_service = new SimpleService();
                bool isSuccess = nfu_service.OrderLogSave(order_id, order_info_id, ref message);
                if (isSuccess)
                {
                    //MessageBox.Show("儲存log");
                    //throw new Exception("ggtest");
                }
                else {
                    MessageBox.Show(message);
                }
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText("無法取得【儲存log】服務\n");
                //MessageBox.Show("無法取得【儲存log】服務");
                //MessageBox.Show(ex.Message);
            }
        }



        private async void button2_Click(object sender, EventArgs e)
        {
            string Sentence = "";
            string Pattern = "[\\D \" \\s]";
            string ReplacePattern = "";
            string ss = "";
            await Task.Delay(3000);
            HtmlElement countelement;
            try
            {
                countelement = webBrowser1.Document.GetElementsByTagName("table")[5].GetElementsByTagName("td")[1];
                Sentence = countelement.InnerHtml;
                if (Regex.IsMatch(Sentence, Pattern))   // 是否有符合的字串
                {
                    string s = Regex.Replace(Sentence, Pattern, ReplacePattern);
                    ss = s.Substring(8);
                }
                if (int.Parse(ss) < int.Parse(textBox2.Text))
                {
                    //記錄現在的單字量
                    JObject LinqSearchOrder = JObject.Parse(orderDataJson); // {} 物件json LINQ查詢
                    ChangeOrderInfoStatus(Int32.Parse(LinqSearchOrder["id"].ToString()), int.Parse(ss), 1, Int32.Parse(LinqSearchOrder["IsCheck"].ToString()));
                    
                    HtmlElement element;
                    try
                    {
                        //element = webBrowser1.Document.GetElementsByTagName("table")[4].GetElementsByTagName("tbody")[0].GetElementsByTagName("tr")[5].GetElementsByTagName("td")[1].GetElementsByTagName("table")[0].GetElementsByTagName("tbody")[0].GetElementsByTagName("tr")[13].GetElementsByTagName("td")[1].GetElementsByTagName("a")[0];
                        //element.InvokeMember("click");
                        webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_practice.asp#jcx");
                        type = 1;
                        label3.Text = "今日次數 : " + totalcoutn.ToString();
                        totalcoutn++;
                        timer1.Interval = 1000;
                        timer1.Enabled = true;
                    }
                    catch
                    {
                        webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_home.asp");
                        richTextBox1.AppendText("進入練習錯誤\n");
                        type = 0;
                        timer1.Interval = 1000;
                        timer1.Enabled = true;
                    }
                }
                else
                {
                    // TODO: 進行下一個專案
                    this.Close();
                }
            }
            catch
            {
                webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_home.asp");
                richTextBox1.AppendText("次數讀取錯誤\n");
                type = 0;
                timer1.Interval = 1000;
                timer1.Enabled = true;
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            HtmlElement element;
            try
            {
                element = this.webBrowser1.Document.GetElementById("word_lab"); //radio題目
                radio[count] = element.InnerText.Replace(" ", "");

                element = this.webBrowser1.Document.GetElementsByTagName("table")[13].GetElementsByTagName("tr")[5].GetElementsByTagName("td")[1].GetElementsByTagName("strong")[0];
                radioans[count] = element.InnerText.Replace(" ", "").Replace("。", "").Replace("；", "").Replace("、", "");

                element = this.webBrowser1.Document.GetElementById("word_lab2").GetElementsByTagName("font")[0]; //填充答案
                textans[count] = element.InnerText.Trim();

                element = this.webBrowser1.Document.GetElementById("word_lab3"); //填充題目
                text[count] = element.InnerText.Replace(" ", "").Replace("。", "").Replace("，", "");
                //MessageBox.Show(radio[count] + " , " + radioans[count] + " , " + textans[count] + " , " + textans[count]);
                type = 2;
                timer1.Interval = 1000;
                timer1.Enabled = true;
                count++;
            }
            catch
            {
                webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_home.asp");
                richTextBox1.AppendText("取得答案錯誤\n");
                type = 0;
                count = 0;
                timer1.Interval = 1000;
                timer1.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] strt = new string[10];
            for (int i = 0; i < 10; i++)
            {
                strt[i] = radio[i] + "," + radioans[i] + "," + textans[i] + "," + text[i];
            }
            richTextBox1.Lines = strt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (count < 10)
            {
                try
                {
                    HtmlElementCollection elementlist;
                    elementlist = webBrowser1.Document.GetElementsByTagName("div");
                    foreach (HtmlElement element in elementlist)
                    {
                        if (element.GetAttribute("align").ToString() == "right")
                        {
                            HtmlElementCollection imglist = element.Document.GetElementsByTagName("img");
                            foreach (HtmlElement imghtml in imglist)
                            {
                                if (imghtml.GetAttribute("src").ToString() == "http://140.130.28.17/online_test/word_examine/img/main03_1_05.jpg")
                                {
                                    imghtml.InvokeMember("click");
                                }
                            }
                        }
                    }
                    type = 1;
                    timer1.Interval = 500;
                    timer1.Enabled = true;
                }
                catch
                {
                    webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_home.asp");
                    richTextBox1.AppendText("練習下一頁錯誤\n");
                    type = 0;
                    count = 0;
                    timer1.Interval = 1000;
                    timer1.Enabled = true;
                }

            }
            else
            {
                try
                {
                    HtmlElementCollection elementlist;
                    elementlist = webBrowser1.Document.GetElementsByTagName("div");
                    foreach (HtmlElement element in elementlist)
                    {
                        if (element.GetAttribute("align").ToString() == "right")
                        {
                            HtmlElementCollection imglist = element.Document.GetElementsByTagName("img");
                            foreach (HtmlElement imghtml in imglist)
                            {
                                if (imghtml.GetAttribute("src").ToString() == "http://140.130.28.17/online_test/word_examine/img/button17.jpg")
                                {
                                    imghtml.InvokeMember("click");
                                }
                            }
                        }
                    }
                    count = 0;
                    type = 3; // 執行考試  , 暫時分開
                    timer1.Interval = 1000;
                    timer1.Enabled = true;
                }
                catch
                {
                    webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_home.asp");
                    richTextBox1.AppendText("跳出練習錯誤\n");
                    type = 0;
                    count = 0;
                    timer1.Interval = 1000;
                    timer1.Enabled = true;
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (type) {
                case 0:
                    timer1.Enabled = false;
                    button2_Click(this, e);
                    break;
                case 1:
                    timer1.Enabled = false;
                    button3_Click(this, e);
                    break;
                case 2:
                    timer1.Enabled = false;
                    button5_Click(this, e);
                    break;
                case 3:
                    timer1.Enabled = false;
                    button6_Click(this, e);
                    break;
                default:
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                //HtmlElement element;
                //element = webBrowser1.Document.GetElementsByTagName("table")[4].GetElementsByTagName("tbody")[0].GetElementsByTagName("tr")[5].GetElementsByTagName("td")[1].GetElementsByTagName("table")[0].GetElementsByTagName("tbody")[0].GetElementsByTagName("tr")[13].GetElementsByTagName("td")[3].GetElementsByTagName("a")[0];
                //element.InvokeMember("click");
                webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_promote.asp");
            }
            catch
            {
                richTextBox1.AppendText("進入考試錯誤\n");
            }
            testtype = 1;
            timer2.Interval = 2000;
            timer2.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Random crandom = new Random();
            int tt = (int)(crandom.NextDouble() * 20);
            if (testcount <= 10)
            {
                int titlecount = 0; //題目之答案位置;
                HtmlElement title;  //題目
                try
                {
                    title = webBrowser1.Document.GetElementById("ajax_word_table").GetElementsByTagName("table")[1].GetElementsByTagName("strong")[1].GetElementsByTagName("font")[0];
                    for (int i = 0; i < radio.Length; i++)
                    {

                        if (title.InnerHtml.Replace(" ", "") == radio[i])
                        {
                            //MessageBox.Show(title.InnerHtml);
                            titlecount = i;
                            //MessageBox.Show(radio[i] + ","+radioans[i]);
                            break;
                        }
                    }
                    HtmlElementCollection radiotestlist;
                    try
                    {
                        radiotestlist = webBrowser1.Document.GetElementById("ajax_word_table").GetElementsByTagName("table")[1].GetElementsByTagName("input");
                        foreach (HtmlElement radiohtml in radiotestlist)
                        {
                            //MessageBox.Show(radiohtml.GetAttribute("value").ToString().Replace(" ", "").Replace("。", "").Replace("；", "").Replace("、", ""));
                            //textBox3.Text = radiohtml.GetAttribute("value").ToString().Replace(" ", "").Replace("。", "").Replace("；", "").Replace("、", "");
                            if (radiohtml.GetAttribute("value").ToString().Replace(" ", "").Replace("。", "").Replace("；", "").Replace("、", "") == radioans[titlecount])
                            {
                                radiohtml.InvokeMember("click");
                                break;
                            }
                        }
                    }
                    catch
                    {
                        richTextBox1.AppendText("選取R考試答案錯誤\n");
                    }
                    testtype = 2;
                    label4.Text = (int.Parse(textBox1.Text) + tt * 100).ToString();
                    timer2.Interval = int.Parse(textBox1.Text);// + tt * 100;
                    timer2.Enabled = true;
                }
                catch
                {
                    webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_home.asp");
                    richTextBox1.AppendText("取得R考試題目錯誤\n");
                    type = 0;
                    count = 0;
                    timer1.Interval = 1000;
                    timer1.Enabled = true;
                }
            }
            else
            {
                int textcount = 0; //題目之答案位置;
                HtmlElementCollection elementlist;
                try
                {
                    elementlist = webBrowser1.Document.GetElementsByTagName("span");
                    foreach (HtmlElement element in elementlist)
                    {
                        if (element.GetAttribute("classname").ToString() == "style8")
                        {
                            for (int i = 0; i < text.Length; i++)
                            {
                                if (element.InnerHtml.Replace(" ", "").Replace("。", "").Replace("，", "") == text[i])
                                {
                                    textcount = i;
                                    break;
                                }
                            }
                        }
                        if (textcount != 0)
                        {
                            break;
                        }
                    }
                    HtmlElement textedit;
                    try
                    {
                        textedit = webBrowser1.Document.GetElementById("u_ans");
                        textedit.InnerText = textans[textcount];
                    }
                    catch
                    {
                        richTextBox1.AppendText("填入T考試答案錯誤\n");
                    }
                    testtype = 2;
                    label4.Text = (int.Parse(textBox1.Text) + tt * 100).ToString();
                    timer2.Interval = int.Parse(textBox1.Text);// + tt * 100;
                    timer2.Enabled = true;
                }
                catch
                {
                    richTextBox1.AppendText("取得T考試題目錯誤\n");
                }

            }

        }

        private async void button8_Click(object sender, EventArgs e)
        {
            if (testcount < 20)
            {
                HtmlElement element;
                try
                {
                    element = webBrowser1.Document.GetElementById("image_next");
                    element.InvokeMember("click");
                    testcount++;
                    testtype = 1;
                    timer2.Interval = 1000;
                    timer2.Enabled = true;
                }
                catch
                {
                    webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_home.asp");
                    richTextBox1.AppendText("考試下一頁錯誤\n");
                    type = 0;
                    count = 0;
                    timer1.Interval = 1000;
                    timer1.Enabled = true;
                }
            }
            else
            {
                HtmlElement element;
                try
                {
                    element = webBrowser1.Document.GetElementById("end_lab").GetElementsByTagName("img")[0];
                    element.InvokeMember("click");
                    testcount = 1;
                    testtype = 3; //結束考試
                    timer2.Interval = 1000;
                    timer2.Enabled = true;
                }
                catch
                {
                    webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_home.asp");
                    richTextBox1.AppendText("考試跳出錯誤\n");
                    type = 0;
                    count = 0;
                    timer1.Interval = 1000;
                    timer1.Enabled = true;
                }

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Random crandom = new Random();
            int tt = (int)(crandom.NextDouble() * 15);
            MessageBox.Show(tt.ToString());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string str = "12 35。";
            MessageBox.Show(str.Replace(" ", "").Replace("。", ""));
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            switch (testtype)
            {
                case 1:
                    timer2.Enabled = false;
                    button7_Click(this, e);
                    break;
                case 2:
                    timer2.Enabled = false;
                    button8_Click(this, e);
                    break;
                case 3:
                    timer2.Enabled = false;
                    button11_Click(this, e);
                    break;
                default:
                    break;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            HtmlElement element;
            try
            {
                // using mshtml
                IHTMLDocument2 doc2 = (IHTMLDocument2)webBrowser1.Document.DomDocument;
                doc2.parentWindow.execScript("window.open=function(url){window.location.href=url;}");
                element = webBrowser1.Document.GetElementById("image_next");
                element.InvokeMember("click");
                type = 0;
                timer1.Interval = 1000;
                timer1.Enabled = true;
            }
            catch
            {
                webBrowser1.Navigate("http://140.130.28.17/online_test/word_examine/custom_home.asp");
                richTextBox1.AppendText("考試跳出錯誤\n");
                type = 0;
                count = 0;
                timer1.Interval = 1000;
                timer1.Enabled = true;
            }
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            //if (webBrowser1.StatusText == "http://140.130.28.17/online_test/word_examine/elementary_promote_failed.asp") 

            // using mshtml
            IHTMLDocument2 doc2 = (IHTMLDocument2)webBrowser1.Document.DomDocument;
            doc2.parentWindow.execScript("window.open=function(url){window.location.href=url;}");
            e.Cancel = true;

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            // LINQ for JSON 
            // http://www.newtonsoft.com/json/help/html/LINQtoJSON.htm

            string json = @"{
                'Name': 'Bad Boys',
                'ReleaseDate': '1995-4-7T00:00:00',
                'Genres': [
                    'Action',
                    'Comedy'
                ]
            }";

            string json2 = "[{\"name\":\"Tom\",\"lastname\":\"Chen\",\"report\":[{\"subject\":\"Math\",\"score\":80},{\"subject\":\"English\",\"score\":90}]},"+
                "{\"name\":\"Amy\",\"lastname\":\"Lin\",\"report\":[{\"subject\":\"Math\",\"score\":86},{\"subject\":\"English\",\"score\":88}]}]";

            string json3 = @"[
              'Small',
              'Medium',
              'Large'
            ]";

            string json4 = @"[
              {'Small' : '123'},
              {'Medium' : '123'},
              {'Large' : '123'},
            ]";
            JArray a = JArray.Parse(json3); // [{}] 陣列json
            //JObject m = JObject.Parse(json); // {} 物件json 
            //MessageBox.Show(m["Name"].ToString());
            MessageBox.Show(a[1].ToString());

            /*
            HtmlElement countelement;
            countelement = webBrowser1.Document.GetElementsByTagName("table")[5].GetElementsByTagName("td")[1];

            string Sentence = countelement.InnerHtml;
            string Pattern = "[\\D \" \\s]";
            string ReplacePattern = "";
           // MessageBox.Show(countelement.InnerHtml);
            
            if (Regex.IsMatch(Sentence, Pattern))   // 是否有符合的字串
            {
                string s = Regex.Replace(Sentence, Pattern, ReplacePattern);
                MessageBox.Show(s.Substring(8));
            }
            */
        }

        private string ReleaseDate {get; set;}

        // 禁止alert跳出
        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            IHTMLWindow2 win = (IHTMLWindow2)webBrowser1.Document.Window.DomWindow;
            string s = @"function confirm() {";
            s += @"return true;";
            s += @"}";
            s += @"function alert() {}";
            win.execScript(s, "javascript");
        }
    }
}
