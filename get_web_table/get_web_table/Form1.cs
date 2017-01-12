using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace get_web_table
{
    public partial class Form1 : Form
    {
        string URL = "https://tw.screener.finance.yahoo.net/future/aa03?fumr=futurepart&opmr=optionpart&random=0.7791457287967205";
       
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://my-user-agent.com/");
        }



        private void webBrowser1_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            
            int i;
            //HtmlElement element;
            HtmlElementCollection elementlist7400,elementlist8400;
            URL = webBrowser1.Url.ToString();
            elementlist7400 = webBrowser1.Document.GetElementsByTagName("table")[3].GetElementsByTagName("tbody")[0].GetElementsByTagName("tr");
            elementlist8400 = webBrowser1.Document.GetElementsByTagName("table")[3].GetElementsByTagName("tbody")[0].GetElementsByTagName("tr"); 
            for (i = 0; i < elementlist7400.Count; i++)
            {
                if (elementlist7400[i].GetElementsByTagName("td")[7].InnerHtml.Trim() == textBox1.Text) //Trim 去字串空白
                {          
                    textBox2.Text = elementlist7400[i].GetElementsByTagName("td")[2].GetElementsByTagName("a")[0].InnerHtml;
                    textBox3.Text = elementlist7400[i].GetElementsByTagName("td")[10].GetElementsByTagName("a")[0].InnerHtml;   
                }

                if (elementlist8400[i].GetElementsByTagName("td")[7].InnerHtml.Trim() == textBox6.Text)
                {
                    textBox5.Text = elementlist8400[i].GetElementsByTagName("td")[2].GetElementsByTagName("a")[0].InnerHtml;
                    textBox4.Text = elementlist8400[i].GetElementsByTagName("td")[10].GetElementsByTagName("a")[0].InnerHtml;
                }
            }
            timer1.Enabled = true; 
            
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(URL);
            comboBox1.Items.Add("201509W2");
            comboBox1.Items.Add("201509");
            comboBox1.Items.Add("201510");
            comboBox1.Items.Add("201511");
            comboBox1.Items.Add("201512");
            comboBox1.Items.Add("201603");
            comboBox1.SelectedIndex = 1;
            //webBrowser1.Navigate("http://localhost/input.php");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                webBrowser1.Navigate(URL);
            }
            catch
            {
                timer1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //HtmlElement elements,elements1;
            /* input text
            elements = webBrowser1.Document.GetElementById("edit");
            elements.InnerText = "test";
            */
            /*  button click
            elements = webBrowser1.Document.GetElementById("sub");
            elements.InvokeMember("click");
            */
            /* form submit
            elements = webBrowser1.Document.GetElementById("fomr1");
            elements.InvokeMember("submit");
            */
            webBrowser1.Document.GetElementById("itemselect").SetAttribute("value", comboBox1.SelectedItem.ToString());
            webBrowser1.Document.GetElementById("itemselect").RaiseEvent("onchange");
        }

    }
}
