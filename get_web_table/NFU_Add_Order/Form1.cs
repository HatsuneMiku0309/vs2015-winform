using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using NFU_Add_Order.NFU_Service;
using NFU_Add_Order.NFU_Service2;
using Newtonsoft.Json.Linq;

namespace NFU_Add_Order
{
    public partial class Form1 : Form
    {
        int NotCatchMoney = 0; //未收金額
        int StartCatchMonty = 0; //開收金額
        int CatchedMoney = 0; //已收金額
        int ForecastCatchMoney = 0; //預收金額

        public Form1()
        {
            InitializeComponent();
        }

        // IsNumeric Function
        static bool IsNumeric(object Expression)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum;

            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;

            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SimpleService sv = new SimpleService();
            string message = "";
            button4.Enabled = false;

            if (textBox1.Text == "")
            {
                MessageBox.Show("請輸入帳號！");
                return;
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("請輸入密碼！");
                return;
            }

            if (textBox3.Text == "")
            {
                MessageBox.Show("請輸入起始單字數量！");
                return;
            }
            else {
                if (!IsNumeric(textBox3.Text))
                {
                    MessageBox.Show("起始單字數量必須是數字！");
                    return;
                }
            }

            if (textBox4.Text == "")
            {
                MessageBox.Show("請輸入價格！");
                return;
            }
            else {
                if (!IsNumeric(textBox4.Text))
                {
                    MessageBox.Show("價格必須是數字！");
                    return;
                }
            }

            bool IsAdd = sv.OrderAddOrderInfo(textBox1.Text,textBox2.Text,int.Parse(textBox3.Text), int.Parse(textBox4.Text), out message);

            if (IsAdd)
            {
                MessageBox.Show(message);
            }
            else {
                MessageBox.Show(message);
                return;
            }

            // listview repeat
            AddListView();
        }

        private string SearchOrderInfo()
        {
            SimpleService sv = new SimpleService();
            string OrderInfoData = "";
            string message = "";

            
            bool IsGet = sv.OrderSearchOrderInfo(out OrderInfoData, out message);

            if (IsGet)
            {
                return OrderInfoData;
            }
            else {
                return message;
            }            
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;
            label10.BackColor = Color.LightPink;
            label10.Text = "完成且已繳款";
            label11.BackColor = Color.Green;
            label11.Text = "完成尚未繳款";

            this.Cursor = Cursors.AppStarting;
            await Task.Delay(500);                        
            AddListView();
            this.Cursor = Cursors.Default;
        }

        private void AddListView()
        {
            // initilze
            NotCatchMoney = 0;
            CatchedMoney = 0;
            StartCatchMonty = 0;
            ForecastCatchMoney = 0;

            string OrderInfo = SearchOrderInfo();
            JArray LinqSearchOrderArray = JArray.Parse(OrderInfo); // [{}] 陣列json

            listView1.Clear();

            // Set the view to show details.
            listView1.View = View.Details;
            // Allow the user to edit item text.
            //listView1.LabelEdit = true;
            // Allow the user to rearrange columns.
            listView1.AllowColumnReorder = true;
            // Display check boxes.
            listView1.CheckBoxes = true;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;
            // Display grid lines.
            listView1.GridLines = true;
            // Sort the items in the list in ascending order.
            listView1.Sorting = SortOrder.Ascending;

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            listView1.Columns.Add("流水號", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("帳號", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("密碼", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("起始單字數量", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("現在單字數量", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("小計", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("狀態", -2, HorizontalAlignment.Center);

            listView1.Columns[0].Width = 90;
            listView1.Columns[1].Width = 100;
            listView1.Columns[2].Width = 100;
            listView1.Columns[3].Width = 100;
            listView1.Columns[4].Width = 100;
            listView1.Columns[5].Width = 100;
            listView1.Columns[6].Width = 40;
            listView1.Width = 90 + 100 + 100 + 100 + 100 + 100 + 40;

            foreach (object JsonData in LinqSearchOrderArray)
            {
                JObject Json = JObject.Parse(JsonData.ToString());

                // Create three items and three sets of subitems for each item.
                ListViewItem item = new ListViewItem(Json["id"].ToString());
                // Place a check mark next to the item.
                
                if (Json["isUse"].ToString() == "2") // 完成工作
                {
                    item.Checked = true;
                    item.BackColor = Color.Green;

                    
                    if (Json["IsCheck"].ToString() == "1") // 已收錢
                    {
                        item.BackColor = Color.LightPink;
                        CatchedMoney += int.Parse(Json["Subtotal"].ToString());                        
                    }
                    else {
                        StartCatchMonty += int.Parse(Json["Subtotal"].ToString());
                        NotCatchMoney += int.Parse(Json["Subtotal"].ToString());
                    }
                }
                else if (Json["isUse"].ToString() == "-1") // 錯誤(此處不算金額)
                {
                    item.Checked = false;
                    item.BackColor = Color.Red;
                }
                else { // 上未完成(正在run)
                    NotCatchMoney += int.Parse(Json["Subtotal"].ToString());
                }

                item.SubItems.Add(Json["nfuAccount"].ToString());
                item.SubItems.Add(Json["nfuPassword"].ToString());
                item.SubItems.Add(Json["startEnCount"].ToString());
                item.SubItems.Add(Json["NowEnCount"].ToString());
                item.SubItems.Add(Json["Subtotal"].ToString());
                item.SubItems.Add(Json["isUse"].ToString());
                

                //Add the items to the ListView.
                listView1.Items.Add(item);
            }
            ForecastCatchMoney += CatchedMoney + NotCatchMoney;
            textBox5.Text = NotCatchMoney.ToString();
            textBox6.Text = CatchedMoney.ToString();
            textBox7.Text = ForecastCatchMoney.ToString();
            textBox8.Text = StartCatchMonty.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            AddListView();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                button4.Enabled = false;
                ListView.SelectedListViewItemCollection SelectListViewSelectItem = this.listView1.SelectedItems;
                ListViewItem item = SelectListViewSelectItem[0];

                int order_id = int.Parse(item.SubItems[0].Text);
                textBox9.Text = order_id.ToString();

                if (item.BackColor == Color.LightPink)
                {
                    radioButton1.Checked = true;
                    button4.Enabled = true;
                }
                else if (item.BackColor == Color.Green)
                {
                    radioButton2.Checked = true;
                    button4.Enabled = true;
                }
                else {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    button4.Enabled = false;
                }
            } catch (Exception ex) {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

            foreach (object obj in groupBox2.Controls)
            {
                if (obj is RadioButton)
                {
                    RadioButton radio = (RadioButton) obj;
                    if (radio.Checked)
                    {
                        if (radio.Text == "已收款")
                        {
                            ChangeOrderInfoStatus(int.Parse(textBox9.Text), 0, 2, 1);
                        }
                        else {
                            ChangeOrderInfoStatus(int.Parse(textBox9.Text), 0, 2, 0);
                        }
                    }
                }                
            }
            textBox9.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            button4.Enabled = false;

            AddListView();
        }

        // 改變訂單使用狀態
        private void ChangeOrderInfoStatus(int id, int NowEnCount, int status, int IsCheck)
        {
            string message = "";
            try
            {
                SimpleService nfu_service = new SimpleService();
                bool isSuccess = nfu_service.OrderChangeOrderInfoStatus(id, NowEnCount, status, IsCheck, out message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法取得【更改訂單狀態】服務");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
