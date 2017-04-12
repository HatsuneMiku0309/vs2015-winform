using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.IO;

namespace WebSearch
{
    public partial class Form1 : Form
    {
        int WebSearch = 0; //runtime總輪數
        int times = 0; //一輪內次數 , searchtext總筆數
        int a_times = 0; //serchtext內的字串切割數量 split(',');
        int type = 0; //一搜尋引擎內狀態
        int page = 1; //一搜尋引擎頁數
        /*
         * 1. 進入google
         * 2. 塞入搜尋條件
         * 3. 提交
         * 4. 搜尋
         * 5. 下一頁
         */

        int mytotaltime = 0; //執行總時間累計
        string[] searchlist;
        string[][] searchlist_A; //searchlist在分解 Split(',');
        int[] searchkey;
        int searchcount;
        int[] searchcount_A;
        string[] judgelist;
        int judgecount;
        string[] weblist;
        int webcount;
        int stop = 0; //停止條件
        int delaysp;
        Point fv; //form滑鼠位置
        Point pv; //panel1滑鼠位置
        Point templistBox;

        string server;
        string user;
        string password;
        string DBtext;
        string strconn;

        Nullable<int> searchtextlistkey = null;
        string[] judgelistbox2;
        int[] judgekeybox2;
        int judgecountbox2;

        string[] judgetolistbox2;

        int error_count = 0;

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();//引用stopwatch物件

        private delegate void dlgCursorPoint(object sender, EventArgs e);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            settimer.Text = ConfigurationManager.ConnectionStrings["onetimes"].ToString();
            delayspeed.Text = ConfigurationManager.ConnectionStrings["eledelay"].ToString();
            timerset.Text = ConfigurationManager.ConnectionStrings["delay"].ToString();

            Server.Text = ConfigurationManager.ConnectionStrings["server"].ToString();
            acc.Text = ConfigurationManager.ConnectionStrings["acc"].ToString();
            pass.Text = ConfigurationManager.ConnectionStrings["pass"].ToString(); 
            DB.Text = ConfigurationManager.ConnectionStrings["DB"].ToString();

            rand_time.Text = ConfigurationManager.ConnectionStrings["rand"].ToString();

            strconn = "SERVER = " + Server.Text + "; DATABASE = " + DB.Text + "; User ID =" + acc.Text + " ; password = " + pass.Text + ";Charset=utf8";
            listBox1.ScrollAlwaysVisible = true ;
            listBox1.Location = new Point(0,426);
            panel2.Location = new Point(610, 0);
            panel3.Location = new Point(585, 3);
            WebSearch--;
            DBsearchText();
            DBweb();
            searchTextList();
        }

        private void searchTextList()
        {
            int count = 0;
            string sql;
            //string strconn = "SERVER = localhost; DATABASE = test; User ID = root ; password = 80408228";
            MySqlCommand cmd;
            MySqlDataReader read;
            MySqlConnection sqlconn = new MySqlConnection(strconn);
            try
            {
                sqlconn.Open();
                sql = "select count(*) from judge";

                cmd = new MySqlCommand(sql, sqlconn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    judgetolistbox2 = new string[r]; //取得總比數
                }
                cmd.Dispose();
                /*
                * 取得條件判斷
                */
                sql = "select * from judge";

                cmd = new MySqlCommand(sql, sqlconn);
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    judgetolistbox2[count] = read["jd_name"].ToString() + "：" + searchlist[count];
                    count++;

                }
                read.Dispose();
                cmd.Dispose();
                sqlconn.Close();
                sqlconn.Dispose();
                listBox2.Items.Clear();
                listBox2.Items.AddRange(judgetolistbox2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("請開啟mysql或設定錯誤 \n" + ex.ToString());
            }

           
        }

        private async void CursorPoint(object sender , EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //啟用
        {
            if ((Server.Text != "") && (acc.Text != "") && (DB.Text != ""))
            {
                start();//
                button1.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
            else
            {
                MessageBox.Show("請設定DB相關設定");
            }
        }

        private void start()
        {
            DBsearchText();
            DBweb();
            searchTextList();
            /*
            SqlDataAdapter da1 = new SqlDataAdapter(sql, sqlconn);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            MessageBox.Show(ds.Tables[0].Rows[0][0].ToString());
            */
            sw.Reset();//碼表歸零
            sw.Start();//碼表開始計時
            type = 1; //進入google
            timer1.Interval = 1000+int.Parse(timerset.Text);
            timer1.Enabled = true;

        }

        private void DBsearchText()
        {
            server = Server.Text;
            user = acc.Text;
            password = pass.Text;
            DBtext = DB.Text;
            strconn = "SERVER = " + server + "; DATABASE = " + DBtext + "; User ID =" + user + " ; password = " + password + ";Charset=utf8";

            WebSearch++;
            label8.Text = "已執行" + WebSearch.ToString() + "次";
            delaysp = int.Parse(delayspeed.Text);
            int count = 0;
            MySqlCommand cmd;
            MySqlDataReader read;
            MySqlConnection sqlconn = new MySqlConnection(strconn);
            try
            {
                sqlconn.Open();
                /*
               * 取得搜尋條件總數
               */
                string sql = "select count(*) from searchtext";
                cmd = new MySqlCommand(sql, sqlconn);

                object result = cmd.ExecuteScalar();
                int r = 0;
                if (result != null)
                {
                    r = Convert.ToInt32(result);
                    searchlist = new string[r]; //取得總筆數
                    searchkey = new int[r];
                    searchlist_A = new string[r][];
                    searchcount = r;
                    searchcount_A = new int[r];
                }
                cmd.Dispose();
                /*
                    * 取得搜尋條件
                    */
                sql = "select * from searchtext";
                cmd = new MySqlCommand(sql, sqlconn);
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    searchkey[count] = (int)read["se_key"];
                    searchlist[count] = read["se_text"].ToString();
                    string[] tempsearchlist = searchlist[count].Split(',');
                    searchcount_A[count] = tempsearchlist.Length;  //各項切割總數
                    searchlist_A[count] = new string[searchcount_A[count]];
                    for (int a_count = 0; a_count < searchcount_A[count]; a_count++)
                    {
                        searchlist_A[count][a_count] = tempsearchlist[a_count];
                    }
                    count++;
                }

                read.Dispose();

                cmd.Dispose();
                sqlconn.Close();
                sqlconn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("請開啟mysql或設定錯誤 \n"+ex.ToString());
                write_log("DBsearchText_mysql錯誤");
            }
        }

        private void DBweb()
        {
            int count = 0;
            //string strconn = "SERVER = localhost; DATABASE = test; User ID = root ; password = 80408228";
            int last_limit = 0;
            MySqlCommand cmd;
            MySqlDataReader read;
            MySqlConnection sqlconn = new MySqlConnection(strconn);
            try
            {
                sqlconn.Open();
                /*
                 * 取得已搜尋之網頁總數
                 */
                string sql = "select count(*) from search";
                cmd = new MySqlCommand(sql, sqlconn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    if (r > 50)
                    {
                        r = 50;
                    }
                    last_limit = r;
                    weblist = new string[r]; //取得總比數
                    webcount = r;
                }
                cmd.Dispose();
                /*
                 * 取得已搜尋之網頁資料
                 */
                sql = "select * from search order by web_key desc limit 0 , "+ last_limit.ToString();
                cmd = new MySqlCommand(sql, sqlconn);
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    weblist[count] = "頁數:" + read["web_page"].ToString().Trim() + " 項目:" + read["web_item"].ToString().Trim() + " 搜尋引擎:" + read["web_type"].ToString().Trim() + " 搜尋條件:" + read["web_search"].ToString().Trim() + " 搜尋判斷:" + read["web_condition"].ToString().Trim() + " 時間:" + read["web_time"].ToString().Trim();
                    count++;
                }
                read.Dispose();
                cmd.Dispose();
                sqlconn.Close();
                sqlconn.Dispose();

                listBox1.Items.Clear();
                listBox1.Items.AddRange(weblist);
            }
            catch (Exception ex)
            {
                MessageBox.Show("請開啟mysql或設定錯誤 \n" + ex.ToString());
                write_log("DBweb_mysql錯誤");
            }
        }

        private void DBjudge(int se_key = -1)
        {
            int count = 0;
            string sql;
            //string strconn = "SERVER = localhost; DATABASE = test; User ID = root ; password = 80408228";
            MySqlCommand cmd;
            MySqlDataReader read;
            MySqlConnection sqlconn = new MySqlConnection(strconn);
            try
            {
                sqlconn.Open();
                /*
               * 取得條件判斷總數
               */
                if (se_key != -1)
                {
                    sql = "select count(*) from judge where se_key = " + se_key.ToString();
                }
                else
                {
                    sql = "select count(*) from judge";
                }
                cmd = new MySqlCommand(sql, sqlconn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    judgelist = new string[r]; //取得總比數
                    judgecount = r;
                }
                cmd.Dispose();
                /*
                * 取得條件判斷
                */
                if (se_key != -1)
                {
                    sql = "select * from judge where se_key = " + se_key.ToString();
                }
                else
                {
                    sql = "select * from judge";
                }
                cmd = new MySqlCommand(sql, sqlconn);
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    judgelist[count] = read["jd_name"].ToString();
                    count++;
                }
                read.Dispose();
                cmd.Dispose();
                sqlconn.Close();
                sqlconn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("請開啟mysql或設定錯誤 \n" + ex.ToString());
                write_log("DBjudge_mysql錯誤");
            }
        }

        /*
         * 用於listBox2搜尋用
         */
        private void DBjudgelist(int se_key = -1)
        {
            int count = 0;
            string sql;
            //string strconn = "SERVER = localhost; DATABASE = test; User ID = root ; password = 80408228";
            MySqlCommand cmd;
            MySqlDataReader read;
            MySqlConnection sqlconn = new MySqlConnection(strconn);
            try
            {
                sqlconn.Open();
                /*
                * 取得條件判斷總數
                */
                if (se_key != -1)
                {
                    sql = "select count(*) from judge where se_key = " + se_key.ToString();
                }
                else
                {
                    sql = "select count(*) from judge";
                }
                cmd = new MySqlCommand(sql, sqlconn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    judgelistbox2 = new string[r]; //取得總比數
                    judgekeybox2 = new int[r];
                    judgecountbox2 = r;
                }
                cmd.Dispose();
                /*
                * 取得條件判斷
                */
                if (se_key != -1)
                {
                    sql = "select * from judge where se_key = " + se_key.ToString();
                }
                else
                {
                    sql = "select * from judge";
                }
                cmd = new MySqlCommand(sql, sqlconn);
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    judgelistbox2[count] = read["jd_name"].ToString();
                    judgekeybox2[count] = (int)read["jd_key"];
                    count++;
                }
                read.Dispose();
                cmd.Dispose();
                sqlconn.Close();
                sqlconn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("請開啟mysql或設定錯誤 \n" + ex.ToString());
                write_log("DBjudgelist_mysql錯誤");
            }
        }

        /*
         * 搜尋searchkey 之 judge 及 進入 google
         */
        private void googleNavigate(int searchkey)
        {
            try {
                DBjudge(searchkey);

                textBox1.Text = searchlist[times];
                richTextBox1.Lines = judgelist;

                webBrowser1.Navigate("https://www.google.com.tw/");

                timer1.Interval = 1000 + int.Parse(timerset.Text);
                timer1.Enabled = true;
            } catch (Exception ex) {
                write_log("Navigate to google error");
                type = 1;
                timer1.Interval = 1000 ;
                timer1.Enabled = true;
            }
        }


        /*
         * 塞入搜尋條件
         */
        private void setGoogleText()
        {
            HtmlElement element;
            try {
                element = webBrowser1.Document.GetElementById("lst-ib");
                element.InnerText = searchlist_A[times][a_times];
                timer1.Interval = 500 + int.Parse(timerset.Text);
                timer1.Enabled = true;
            } catch (Exception ex) {
                error_count++;
                if (error_count >= 5)
                {
                    type = 1;
                    error_count = 0;
                    write_log("google搜尋字串查詢失敗5次，重新進入google");
                }
                else
                {
                    type = 2;
                    write_log("google搜尋字串查詢失敗(setGoogleText)");
                }
                
                timer1.Interval = 500 ;
                timer1.Enabled = true;
                
              
            }
        }

        /*
         * 提交搜尋
         */
        private void submitGoogle()
        {
            try
            {
                HtmlElement element;
                element = webBrowser1.Document.GetElementById("tsf");
                element.InvokeMember("submit");
                timer1.Interval = 2000 + int.Parse(timerset.Text);
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                error_count++;
                if (error_count >= 5)
                {
                    type = 1;
                    error_count = 0;
                    write_log("google搜尋提交錯誤5次，重新進入google");
                }
                else
                {
                    type = 3;
                    write_log("google搜尋提交(submitGoogle)");
                }
                
                timer1.Interval = 500 ;
                timer1.Enabled = true;
            }
        }

        /*
         * 搜尋判斷條件(judge)
         */
        private async void searchJudgeGoogle()
        {
            /*
             * 進行搜尋
             */
            int item = 0;  //第一項
            HtmlElementCollection elementlist;
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            if (stop != 1)
            {
                label20.Text = page.ToString()+"頁";
                try
                {
                    elementlist = webBrowser1.Document.GetElementById("rso").GetElementsByTagName("div");
                    foreach (HtmlElement divghtml in elementlist)
                    {
                        await Task.Delay(delaysp+(rnd.Next(1,10)*int.Parse(rand_time.Text))); //延遲,避免被認為是機器人
                        if (divghtml.GetAttribute("classname").ToString() == "g")
                        {
                            item++;
                            HtmlElementCollection divflist = divghtml.GetElementsByTagName("div");
                            foreach (HtmlElement divfhtml in divflist)
                            {
                                if (divfhtml.GetAttribute("classname").ToString() == "f kv _SWb")
                                {
                                    HtmlElementCollection citelist = divfhtml.GetElementsByTagName("cite");
                                    foreach (HtmlElement citehtml in citelist)
                                    {
                                        if (citehtml.GetAttribute("classname").ToString() == "_Rm")
                                        {
                                            for (int i = 0; i < judgecount; i++)
                                            {
                                                if ((citehtml.InnerHtml.IndexOf(judgelist[i].Trim()) >= 0))
                                                {
                                                    stop = 1;
                                                    SaveData(page.ToString(), item.ToString(), "google", searchlist_A[times][a_times], judgelist[i]);
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
                        if (stop == 1)
                        {
                            break;
                        }
                    }

                    /*
                     *  搜尋結果無,判斷頁數
                     */
                    if ((page < 10) && (stop != 1)) // 頁數不滿10頁,跳頁
                    {
                        page++;
                        type = 5; //nextpage
                        timer1.Interval = 500 + int.Parse(timerset.Text);
                        timer1.Enabled = true;
                    }
                    else if ((page >= 10) && (stop != 1)) // 頁數滿10頁,換搜尋引擎
                    {
                        SaveData("搜尋無結果", "搜尋無結果", "google", searchlist_A[times][a_times], judgelist[0]);
                        DBweb();
                        stop = 0;
                        page = 1;
                        type = 10; // 進入yahoo
                        timer1.Interval = 500 + int.Parse(timerset.Text);
                        timer1.Enabled = true;
                        //MessageBox.Show("滿10換搜尋");
                    }
                    /*
                     * 搜尋到結果,換搜尋引擎
                     */
                    else if (stop == 1)
                    {
                        DBweb();
                        stop = 0;
                        page = 1;
                        type = 10; // 進入yahoo
                        timer1.Interval = 500 + int.Parse(timerset.Text);
                        timer1.Enabled = true;
                        //MessageBox.Show("搜尋到結果換搜尋");
                    }

                }
                catch (Exception ex)
                {
                    error_count++;
                    if (error_count >= 5)
                    {
                        type = 1;
                        error_count = 0;
                        write_log("google_DOM搜尋錯誤5次，重新進入google");
                    }
                    else
                    {
                        type = 4;
                        write_log("google_DOM搜尋錯誤(searchJudgeGoogle)");
                    }
                    
                    timer1.Interval = 1000;
                    timer1.Enabled = true;
                }
            }
        }

        private void nextPageGoogle()
        {
            try
            {
                HtmlElementCollection elementlist;
                elementlist = webBrowser1.Document.GetElementsByTagName("a");
                foreach (HtmlElement curhtml in elementlist)
                {
                    if (curhtml.GetAttribute("id").ToString() == "pnnext")
                    {
                        curhtml.InvokeMember("click");
                        break;
                    }
                }
                type = 4;
                timer1.Interval = 500 + int.Parse(timerset.Text);
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                error_count++;
                if (error_count >= 5)
                {
                    type = 1;
                    error_count = 0;
                    write_log("google_next錯誤5次，重新進入google");
                }
                else
                {
                    type = 5;
                    write_log("google_next錯誤(nextPageGoogle)");
                }
                timer1.Interval = 500;
                timer1.Enabled = true;

            }
        }

        private void yahooNavigate()
        {
            try
            {
                webBrowser1.Navigate("https://tw.yahoo.com/");
                timer1.Interval = 10000 + int.Parse(timerset.Text); //yahoo要較久時間進行loding
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                write_log("nabigate to yahoo error");
                type = 10;
                timer1.Interval = 1000;
                timer1.Enabled = true;
            }
        }

        private async void setYahooText()
        {
            try
            {
                await Task.Delay(2000); //給予緩衝時間loding
                HtmlElement element;
                element = webBrowser1.Document.GetElementById("UHSearchBox");
                element.InnerText = searchlist_A[times][a_times];
                timer1.Interval = 2500;
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                error_count++;
                if (error_count >= 5)
                {
                    type = 10;
                    error_count = 0;
                    write_log("yahoo搜尋字串塞入錯誤5次，yahoo");
                }
                else
                {
                    type = 11;
                    write_log("yahoo搜尋字串塞入錯誤(setYahooText)");
                }
                
                timer1.Interval = 2500;
                timer1.Enabled = true;
            }
        }

        private void submitYahoo()
        {
            try
            {
                HtmlElement element;
                element = webBrowser1.Document.GetElementById("UHSearch");
                element.InvokeMember("submit");
                timer1.Interval = 3000 + int.Parse(timerset.Text);
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                error_count++;
                if (error_count >= 5)
                {
                    type = 10;
                    error_count = 0;
                    write_log("yahoo搜尋提交錯誤5次，yahoo");
                }
                else
                {
                    type = 12;
                    write_log("yahoo搜尋提交錯誤(submitYahoo)");
                }
                
                timer1.Interval = 500 ;
                timer1.Enabled = true;
            }
        }

        private async void searchJudgeYahoo()
        {
            int item = 0;
            HtmlElementCollection elementlist;
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            if (stop != 1)
            {
                label20.Text = page.ToString()+"頁";
                try
                {
                    elementlist = webBrowser1.Document.GetElementsByTagName("ol");
                    foreach (HtmlElement curhtml in elementlist)
                    {
                        await Task.Delay(delaysp+(rnd.Next(1,10)*int.Parse(rand_time.Text))); //延遲，避免被認為是機器人
                        //if (curhtml.GetAttribute("classname").ToString() == " reg searchCenterMiddle")  //local
                        if ((curhtml.GetAttribute("classname").ToString() == "mb-15 reg searchCenterMiddle")||(curhtml.GetAttribute("classname").ToString() == " reg searchCenterMiddle")) //187616817主機
                        {
                            HtmlElementCollection lihtmllist = curhtml.GetElementsByTagName("li");
                            foreach (HtmlElement lihtml in lihtmllist)
                            {
                                await Task.Delay(delaysp);
                                HtmlElementCollection spanlist = lihtml.GetElementsByTagName("span");
                                foreach (HtmlElement spanhtml in spanlist)
                                {
                                    if (spanhtml.GetAttribute("classname").ToString() == " fz-ms fw-m fc-12th wr-bw")
                                    {
                                        item++;
                                        for (int i = 0; i < judgecount; i++)
                                        {
                                            if (spanhtml.InnerHtml.IndexOf(judgelist[i].Trim()) >= 0)
                                            {
                                                stop = 1;
                                                SaveData(page.ToString(), item.ToString(), "yahoo", searchlist_A[times][a_times], judgelist[i]);
                                                break;
                                            }
                                        }
                                    }
                                    if (stop == 1)
                                    {
                                        break;
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

                    /*
                     * 搜尋無結果,判斷頁數
                     */
                    if ((page < 10) && (stop != 1))
                    {
                        page++;
                        type = 14; //nextPageYahoo
                        timer1.Interval = 500 + int.Parse(timerset.Text);
                        timer1.Enabled = true;
                    }
                    else if ((page >= 10) && (stop != 1)) //搜尋無結果,頁數滿10,換搜尋條件
                    {
                        SaveData("搜尋無結果", "搜尋無結果", "yahoo", searchlist_A[times][a_times], judgelist[0]);
                        DBweb();
                        page = 1;
                        if (a_times < searchcount_A[times] - 1)
                        {
                            type = 1; //換搜尋條件
                            a_times++;
                            timer1.Interval = 500 + int.Parse(timerset.Text);
                            timer1.Enabled = true;
                            //MessageBox.Show("搜尋無結果,頁數滿10,換搜尋條件");
                        }
                        else
                        {
                            if (times < searchcount - 1)
                            {
                                type = 1; //換搜尋條件
                                times++;
                                a_times = 0;
                                timer1.Interval = 500 + int.Parse(timerset.Text);
                                timer1.Enabled = true;
                                //MessageBox.Show("搜尋無結果,頁數滿10,換搜尋條件");
                            }
                            else
                            {
                                sw.Stop();
                                int totaltime = (int)sw.Elapsed.TotalSeconds;
                                times = 0;
                                a_times = 0;
                                type = 0;

                                webBrowser1.Dispose();
                                webBrowser1 = new WebBrowser();
                                webBrowser1.Width = 357;
                                webBrowser1.Height = 463;
                                webBrowser1.Location = new Point(0, 0);
                                webBrowser1.Visible = true;
                                this.Controls.Add(webBrowser1);

                                int mytime = (int.Parse(settimer.Text) - totaltime) * 1000;
                                if (mytime > 0)
                                {
                                    timer1.Interval = mytime;
                                }
                                else
                                {
                                    timer1.Interval = 1000;
                                }
                                timer1.Enabled = true;
                                //MessageBox.Show("一輪結束，準備下輪");
                            }
                        }
                    }
                    else if (stop == 1) //搜尋到結果,換搜尋條件
                    {
                        DBweb();
                        stop = 0;
                        page = 1;
                        page = 1;
                        if (a_times < searchcount_A[times] - 1)
                        {
                            type = 1; //換搜尋條件
                            a_times++;
                            timer1.Interval = 500 + int.Parse(timerset.Text);
                            timer1.Enabled = true;
                            //MessageBox.Show("搜尋無結果,頁數滿10,換搜尋條件");
                        }
                        else
                        {
                            if (times < searchcount - 1)
                            {
                                type = 1;
                                times++;
                                a_times = 0;
                                timer1.Interval = 500 + int.Parse(timerset.Text);
                                timer1.Enabled = true;
                                //MessageBox.Show("搜尋到結果,換搜尋條件");
                            }
                            else
                            {
                                sw.Stop();
                                int totaltime = (int)sw.Elapsed.TotalSeconds;
                                times = 0;
                                a_times = 0;
                                type = 0;

                                webBrowser1.Dispose();
                                webBrowser1 = new WebBrowser();
                                webBrowser1.Width = 357;
                                webBrowser1.Height = 463;
                                webBrowser1.Location = new Point(0, 0);
                                webBrowser1.Visible = true;
                                this.Controls.Add(webBrowser1);


                                int mytime = (int.Parse(settimer.Text) - totaltime) * 1000;
                                if (mytime > 0)
                                {
                                    timer1.Interval = mytime;
                                }
                                else
                                {
                                    timer1.Interval = 1000;
                                }
                                timer1.Enabled = true;
                                //MessageBox.Show("一輪結束，準備下輪");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    error_count++;
                    if (error_count >= 5)
                    {
                        type = 10;
                        error_count = 0;
                        write_log("yahoo_DOM錯誤5次，yahoo");
                    }
                    else
                    {
                        write_log("yahoo_DOM錯誤(searchJudgeYahoo)");
                        type = 13;
                    }
                    
                    timer1.Interval = 1000;
                    timer1.Enabled = true;
                }
            }
        }

        private void nextPageYahoo()
        {
            try
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
                type = 13;
                timer1.Interval = 1000 + int.Parse(timerset.Text);
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                error_count++;
                if (error_count >= 5)
                {
                    type = 10;
                    error_count = 0;
                    write_log("yahoo_next錯誤5次，yahoo");
                }
                else
                {
                    type = 14;
                    write_log("yahoo_next錯誤(nextPageYahoo)");
                }
                
                timer1.Interval = 1000;
                timer1.Enabled = true;
            }
        }
        /*
        private void tempSaveData(string web_page, string web_item, string web_type, string web_search, string web_condition)
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
        */
        private void SaveData(string web_page, string web_item, string web_type, string web_search, string web_condition)
        {
            try
            {
                MySqlCommand cmd;

                MySqlConnection sqlconn = new MySqlConnection(strconn);
                sqlconn.Open();

                string sql = "insert into search (web_page,web_item,web_type,web_search,web_condition) values (@web_page,@web_item,@web_type,@web_search,@web_condition)";
                cmd = new MySqlCommand(sql, sqlconn);
                cmd.Parameters.Add("@web_page", MySqlDbType.VarChar).Value = web_page;
                cmd.Parameters.Add("@web_item", MySqlDbType.VarChar).Value = web_item;
                cmd.Parameters.Add("@web_type", MySqlDbType.VarChar).Value = web_type;
                cmd.Parameters.Add("@web_search", MySqlDbType.Text).Value = web_search;
                cmd.Parameters.Add("@web_condition", MySqlDbType.Text).Value = web_condition;
                cmd.ExecuteNonQuery();

                sqlconn.Close();
                sqlconn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                write_log("DB存檔錯誤(SaveData)");
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (type)
            {
                case 0: //全部重置 , start
                    timer1.Enabled = false;
                    start();
                    break;
                case 1: //進入google
                    timer1.Enabled = false;
                    type = 2; 
                    googleNavigate(searchkey[times]);
                    break;
                case 2: //塞入搜尋條件
                    timer1.Enabled = false;
                    type = 3; 
                    setGoogleText(); 
                    break;
                case 3: //提交google搜尋
                    timer1.Enabled = false;
                    type = 4;
                    submitGoogle();
                    break;
                case 4: //進行搜尋
                    timer1.Enabled = false;
                    searchJudgeGoogle();
                    break;
                case 5: //搜尋無結果,google換頁
                    timer1.Enabled = false;
                    nextPageGoogle();
                    break;
                case 10: // 進入yahoo
                    timer1.Enabled = false;
                    type = 11; //塞入搜尋條件yahoo
                    yahooNavigate();
                    break;
                case 11: //塞入搜尋條件yahoo
                    timer1.Enabled = false;
                    type = 12;
                    setYahooText();
                    break;
                case 12: //提交搜尋yahoo
                    timer1.Enabled = false;
                    type = 13;
                    submitYahoo();
                    break;
                case 13: //進行搜尋yahoo
                    timer1.Enabled = false;
                    searchJudgeYahoo();
                    break;
                case 14: //搜尋無結果,yahoo換頁
                    timer1.Enabled = false;
                    nextPageYahoo();
                    break;
                default:
                    break;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (Control pc in panel1.Controls)
            {
                pc.MouseMove += CursorPoint;
                dlgCursorPoint dcp = CursorPoint;
                dcp.Invoke(this, e);
            }
        }

        private async void listBox1_MouseLeave(object sender, EventArgs e)
        {
            /*
            templistBox = listBox1.Location;
            if (templistBox.Y < 416)
            {
                for (int i = templistBox.Y; i < 416; i = i + 10)
                {
                    await Task.Delay(10);
                    listBox1.Location = new Point(0, i);
                }
            }
            */
        }

        private async void listBox1_MouseEnter(object sender, EventArgs e)
        {
            /*
            templistBox = listBox1.Location;
            if (templistBox.Y > 206)
            {
                for (int i = templistBox.Y; i > 206; i = i - 10)
                {
                    await Task.Delay(10);
                    listBox1.Location = new Point(0, i);
                }
            }
            */
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (panel2.Location.X >= 610)
            {
                for (int i = panel2.Location.X; i >= 450; i = i - 10)
                {
                    await Task.Delay(10);
                    panel2.Location = new Point(i, 0);
                    panel3.Location = new Point(i-25, 3);
                }
            }
            else
            {
                for (int i = panel2.Location.X; i <= 610; i = i + 10)
                {
                    await Task.Delay(10);
                    panel2.Location = new Point(i, 0);
                    panel3.Location = new Point(i-25, 3);
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                searchtextlistkey = searchkey[listBox2.SelectedIndex];
                DBjudgelist(searchkey[listBox2.SelectedIndex]);
                richTextBox2.Clear();
                richTextBox2.Lines = judgelistbox2;
                //textBox2.Text = listBox2.SelectedItem.ToString();
                textBox2.Text = searchlist[listBox2.SelectedIndex];
            }
        }

        private void insert_DB_Click(object sender, EventArgs e)
        { 
            if (listBox2.Items.Contains(textBox2.Text))
            {
                MessageBox.Show("有重複的搜尋條件");
            }
            else
            { 
                if ((textBox2.Text != "") && (textBox2.Text != null) && (richTextBox2.Text !="") && (richTextBox2.Text != null))
                {
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    MessageBoxIcon icon = MessageBoxIcon.Question;
                    DialogResult resultmessage;
                    resultmessage = MessageBox.Show("是否要執行新增?", "新增:" + textBox2.Text, buttons, icon);
                    switch (resultmessage)
                    {
                        case DialogResult.Yes:
                            MySqlCommand cmd;
                            MySqlConnection sqlconn = new MySqlConnection(strconn);
                            string strInsert = "";
                            Nullable<int> se_key = null;
                            try
                            {
                                sqlconn.Open();
                                string sql = "insert into searchtext (se_text) values (@se_text)";
                                cmd = new MySqlCommand(sql, sqlconn);
                                cmd.Parameters.Add("@se_text", MySqlDbType.VarChar).Value = textBox2.Text;
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();

                                sql = "select se_key from searchtext order by se_key desc";
                                cmd = new MySqlCommand(sql, sqlconn);
                                object result = cmd.ExecuteScalar();
                                if (result != null)
                                {
                                    se_key = Convert.ToInt32(result); //最後一筆新增key
                                }
                                cmd.Dispose();

                                string[] judgetext = richTextBox2.Text.Split('\n');
                                for (int i = 0; i < judgetext.Length; i++)
                                {
                                    if ((judgetext[i] != null) && (judgetext[i] != ""))
                                    {
                                        if (i == 0)
                                        {
                                            strInsert += "('" + judgetext[i] + "'," + se_key.ToString() + ")";
                                        }
                                        else
                                        {
                                            strInsert += ",('" + judgetext[i] + "'," + se_key.ToString() + ")";
                                        }
                                    }
                                }
                                if ((strInsert != null) && (strInsert != ""))
                                {
                                    sql = "insert into judge (jd_name,se_key) values " + strInsert;
                                    cmd = new MySqlCommand(sql, sqlconn);
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                }

                                sqlconn.Close();
                                sqlconn.Dispose();
                                DBsearchText(); //重置
                                searchTextList();
                                //listBox2.Items.Clear();
                                //listBox2.Items.AddRange(searchlist);
                                textBox2.Clear();
                                richTextBox2.Clear();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                write_log("新增DB連線錯誤(insert_DB_Click)");
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("請輸入搜尋條件或搜尋判斷條件");
                }
                searchtextlistkey = null;

            }
        }

        private async void listBox1_Click(object sender, EventArgs e)
        {
            templistBox = listBox1.Location;
            if (templistBox.Y > 216)
            {
                for (int i = templistBox.Y; i >= 216; i = i - 10)
                {
                    await Task.Delay(10);
                    listBox1.Location = new Point(0, i);
                }
            }
            else if (templistBox.Y < 426)
            {
                for (int i = templistBox.Y; i < 426; i = i + 10)
                {
                    await Task.Delay(10);
                    listBox1.Location = new Point(0, i);
                }
            }
        }

        private void update_DB_Click(object sender, EventArgs e)
        {
            if (searchtextlistkey == null)
            {
                MessageBox.Show("請選擇項目");
            }
            else
            {
                if ((textBox2.Text != "") && (textBox2.Text != null) && (richTextBox2.Text != "") && (richTextBox2.Text != null))
                {
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    MessageBoxIcon icon = MessageBoxIcon.Question;
                    DialogResult resultmessage;
                    resultmessage = MessageBox.Show("是否要執行修改?", "修改:" + textBox2.Text, buttons, icon);
                    switch (resultmessage)
                    {
                        case DialogResult.Yes:
                            MySqlCommand cmd;
                            MySqlConnection sqlconn = new MySqlConnection(strconn);

                            try
                            {
                                sqlconn.Open();
                                string sql = "UPDATE `searchtext` SET `se_text`='" + textBox2.Text + "' WHERE `se_key` = " + searchtextlistkey.ToString();
                                cmd = new MySqlCommand(sql, sqlconn);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();

                                string[] judgetext = richTextBox2.Text.Split('\n');  //{1,2,3, , ,123}
                                if (judgekeybox2.Length >= judgetext.Length)  //{1,2,3}
                                {
                                    for (int i = 0; i < judgekeybox2.Length; i++)
                                    {
                                        if (i <= judgetext.Length - 1)
                                        {
                                            if ((judgetext[i] != null) && (judgetext[i] != ""))
                                            {
                                                sql = "UPDATE `judge` SET `jd_name`= '" + judgetext[i] + "' WHERE se_key = " + searchtextlistkey.ToString() + " and jd_key = " + judgekeybox2[i];
                                            }
                                            else
                                            {
                                                sql = "delete from `judge` WHERE se_key = " + searchtextlistkey.ToString() + " and jd_key = " + judgekeybox2[i];
                                            }
                                        }
                                        else
                                        {
                                            sql = "delete from `judge` WHERE se_key = " + searchtextlistkey.ToString() + " and jd_key = " + judgekeybox2[i];
                                        }
                                        cmd = new MySqlCommand(sql, sqlconn);
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                    }
                                }
                                else //修改數量 > DB數量
                                {
                                    for (int i = 0; i < judgetext.Length; i++)
                                    {
                                        if (i <= judgekeybox2.Length - 1)
                                        {
                                            if ((judgetext[i] != null) && (judgetext[i] != ""))
                                            {
                                                sql = "UPDATE `judge` SET `jd_name`='" + judgetext[i] + "' WHERE se_key = " + searchtextlistkey.ToString() + " and jd_key = " + judgekeybox2[i];
                                            }
                                            else
                                            {
                                                sql = "delete from `judge` WHERE se_key = " + searchtextlistkey.ToString() + " and jd_key = " + judgekeybox2[i];
                                            }
                                        }
                                        else
                                        {
                                            if ((judgetext[i] != null) && (judgetext[i] != ""))
                                            {
                                                sql = "INSERT INTO `judge`(`jd_name`, `se_key`) VALUES " + "('" + judgetext[i] + "'," + searchtextlistkey.ToString() + ")";
                                            }
                                        }
                                        cmd = new MySqlCommand(sql, sqlconn);
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                    }
                                }

                                sqlconn.Close();
                                sqlconn.Dispose();
                                DBsearchText(); //重置
                                searchTextList();
                                //listBox2.Items.Clear();
                                //listBox2.Items.AddRange(searchlist);
                                textBox2.Clear();
                                richTextBox2.Clear();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                write_log("修改DB連現錯誤(update_DB_Click)");
                            }
                            searchtextlistkey = null;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("請輸入搜尋條件或搜尋判斷條件");
                }
            }
        }

        private void delete_DB_Click(object sender, EventArgs e)
        {
            if (searchtextlistkey == null)
            {
                MessageBox.Show("請選擇項目");
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Question;
                DialogResult result;
                result = MessageBox.Show("是否要執行刪除?", "刪除:" + listBox2.SelectedItem, buttons, icon);
                switch (result)
                {
                    case DialogResult.Yes:
                        MySqlCommand cmd;
                        MySqlConnection sqlconn = new MySqlConnection(strconn);
                        try
                        {
                            sqlconn.Open();
                            string sql = "delete from searchtext where se_key = " + searchtextlistkey.ToString();
                            cmd = new MySqlCommand(sql, sqlconn);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();

                            sql = "delete from judge where se_key = " + searchtextlistkey.ToString();
                            cmd = new MySqlCommand(sql, sqlconn);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();

                            sqlconn.Close();
                            sqlconn.Dispose();
                            DBsearchText(); //重置
                            searchTextList();
                            //listBox2.Items.Clear();
                            //listBox2.Items.AddRange(searchlist);
                            textBox2.Clear();
                            richTextBox2.Clear();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            write_log("刪除DB連現錯誤(delete_DB_Click)");
                        }

                        searchtextlistkey = null;
                        break;
                    default:

                        break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();//引用stopwatch物件
            sw.Reset();//碼表歸零
            sw.Start();//碼表開始計時

                       //目標程式

            for (int i = 0; i < 100; i++)
            {

            }
            sw.Stop();//碼錶停止
                      //印出所花費的總豪秒數
            string result1 = sw.Elapsed.TotalSeconds.ToString();
            MessageBox.Show(result1);
            */
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://my-user-agent.com/");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            webBrowser1.Dispose();
            webBrowser1 = new WebBrowser();
            webBrowser1.Width = 357;
            webBrowser1.Height = 463;
            webBrowser1.Location = new Point(0, 0);
            webBrowser1.Visible = true;
            this.Controls.Add(webBrowser1);

            //write_log("test");




            //Random crandom = new Random();
            //crandom.Next(0, 10);
            //Random rnd = new Random(Guid.NewGuid().GetHashCode());
            //MessageBox.Show(rnd.Next(1,10).ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string test = "";
            test = ConfigurationManager.ConnectionStrings["eledelay"].ToString();
            MessageBox.Show(test);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string assemblyConfigFile = Assembly.GetEntryAssembly().Location;

                Configuration config = ConfigurationManager.OpenExeConfiguration(assemblyConfigFile);

                //AppSettingsSection appSetting = (AppSettingsSection)config.GetSection("appSettings");
                ConnectionStringsSection connectionstring = (ConnectionStringsSection)config.GetSection("connectionStrings");

                //appSettings.Settings.Remove("onetimes");
                connectionstring.ConnectionStrings.Remove("onetimes");
                connectionstring.ConnectionStrings.Remove("eledelay");
                connectionstring.ConnectionStrings.Remove("delay");
                connectionstring.ConnectionStrings.Remove("server");
                connectionstring.ConnectionStrings.Remove("acc");
                connectionstring.ConnectionStrings.Remove("pass");
                connectionstring.ConnectionStrings.Remove("DB");
                connectionstring.ConnectionStrings.Remove("rand");

                //appSettings.Settings.Add("onetimes", "123");

                ConnectionStringSettings onetimes = new ConnectionStringSettings();
                onetimes.Name = "onetimes";
                onetimes.ConnectionString = settimer.Text.ToString();
                connectionstring.ConnectionStrings.Add(onetimes);
                config.Save();

                ConnectionStringSettings eledelay = new ConnectionStringSettings();
                eledelay.Name = "eledelay";
                eledelay.ConnectionString = delayspeed.Text.ToString();
                connectionstring.ConnectionStrings.Add(eledelay);
                config.Save();

                ConnectionStringSettings delay = new ConnectionStringSettings();
                delay.Name = "delay";
                delay.ConnectionString = timerset.Text;
                connectionstring.ConnectionStrings.Add(delay);
                config.Save();

                ConnectionStringSettings server = new ConnectionStringSettings();
                server.Name = "server";
                server.ConnectionString = Server.Text.ToString();
                connectionstring.ConnectionStrings.Add(server);
                config.Save();

                ConnectionStringSettings acc_text = new ConnectionStringSettings();
                acc_text.Name = "acc";
                acc_text.ConnectionString = acc.Text.ToString();
                connectionstring.ConnectionStrings.Add(acc_text);
                config.Save();

                ConnectionStringSettings pass_test = new ConnectionStringSettings();
                pass_test.Name = "pass";
                pass_test.ConnectionString = pass.Text.ToString();
                connectionstring.ConnectionStrings.Add(pass_test);
                config.Save();

                ConnectionStringSettings DB_text = new ConnectionStringSettings();
                DB_text.Name = "DB";
                DB_text.ConnectionString = DB.Text.ToString();
                connectionstring.ConnectionStrings.Add(DB_text);
                config.Save();

                ConnectionStringSettings rand_text = new ConnectionStringSettings();
                rand_text.Name = "rand";
                rand_text.ConnectionString = rand_time.Text.ToString();
                connectionstring.ConnectionStrings.Add(rand_text);
                config.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("存檔失敗");
                write_log("conf存檔錯誤");
            }
        }

        private void write_log(string mes)
        {
            //今日日期
            DateTime Date = DateTime.Now;
            string TodyMillisecond = Date.ToString("yyyy-MM-dd HH:mm:ss");
            string Tody = Date.ToString("yyyy-MM-dd");

            //如果此路徑沒有資料夾
            if (!Directory.Exists("./_Log"))
            {
                //新增資料夾
                Directory.CreateDirectory("./_Log");
            }

            //把內容寫到目的檔案，若檔案存在則附加在原本內容之後(換行)
            File.AppendAllText("./_Log\\" + Tody + ".txt", "\r\n" + TodyMillisecond + "：" + mes);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool atrue = false;
            bool afalse = false;

            char ret = ((atrue = true) || (afalse = true)) ? 'a' : 'b';

            MessageBox.Show(afalse.ToString());
        }
    }
}
