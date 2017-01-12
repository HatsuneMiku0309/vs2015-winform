using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;


namespace AnimeSearch
{
    public partial class Form1 : Form
    {
        int panel_x;
        int x,x2;

        private delegate void MousePoint(object sender, MouseEventArgs e);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;

            comboBox1.Items.Add("mistake");
            comboBox1.Items.Add("xrgon");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            panel1.Location = new Point(-180, 0);
            panel_x = panel1.Location.X;
            panel2.Location = new Point(0, 0);
            webBrowser1.Navigate("http://player.twitch.tv/?branding=false&showInfo=false&channel=xargon0731");
            webBrowser2.Navigate("http://www.twitch.tv/xargon0731/chat");
            comboBox1.SelectedIndex = 0;
            statusStrip1.Items[0].Text = "載入中";
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            x = panel1.Location.X;
            x2 = panel2.Location.X;
            //label1.Text = x.ToString();
            timer1.Interval = 10;
            if (x < 0)
            {
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Location = new Point((x + 10), 0);
            timer1.Enabled = false;
        }

        private async void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Document.MouseMove += new HtmlElementEventHandler(Document_MouseMove);

            statusStrip1.Items[0].Text = "載入完畢:" + e.Url.ToString();
            await Task.Delay(1000);
            if (comboBox1.SelectedItem.ToString() == "mistake")
            {
                statusStrip1.Items[0].Text = "mistake台";
            }
            else if (comboBox1.SelectedItem.ToString() == "xrgon")
            {
                statusStrip1.Items[0].Text = "xrgon台";
            }
            

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            panel1.Location = new Point((x - 10), 0);
            timer2.Enabled = false;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "mistake")
            {
                webBrowser1.Navigate("http://player.twitch.tv/?branding=false&showInfo=false&channel=mistakelolz");
                webBrowser2.Navigate("http://www.twitch.tv/mistakelolz/chat");
                statusStrip1.Items[0].Text = "轉至mistake台";
            } else if (comboBox1.SelectedItem.ToString() == "xrgon") {
                webBrowser1.Navigate("http://player.twitch.tv/?branding=false&showInfo=false&channel=xargon0731");
                webBrowser2.Navigate("http://www.twitch.tv/xargon0731/chat");
                statusStrip1.Items[0].Text = "轉至xrgon台";
            }
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser2.Document.MouseMove += new HtmlElementEventHandler(Document_MouseMove);
        }

        private void Document_MouseMove(object sender, HtmlElementEventArgs e)
        {
            //statusStrip1.Items[0].Text = webBrowser1.StatusText;
            Point v = this.PointToClient(Cursor.Position);
            //label2.Text = v.X.ToString();
            //v = new Point(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y);           
            if (v.X <= 50)
            {
                x = panel1.Location.X;
                timer1.Interval = 10;
                if (x < 0)
                {
                    timer1.Enabled = true;
                }
            }
            else
            {
                if (x >= panel_x)
                {
                    x = panel1.Location.X;
                    timer2.Enabled = true;
                    timer2.Interval = 10;
                }
            }
        }
    }
}
