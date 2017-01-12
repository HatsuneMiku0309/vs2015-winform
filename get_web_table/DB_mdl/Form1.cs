using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DB_mdl
{
    public partial class Form1 : Form
    {
        string server,DBtext,user,password,DBtable;
        string[][] result;
        string temp_id = "";

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                string strconn;
                strconn = "SERVER = " + server + "; DATABASE = " + DBtext + "; User ID =" + user + " ; password = " + password + ";Charset=utf8";

                MySqlConnection mys_conn = new MySqlConnection(strconn);
                mys_conn.Open();

                string mdl_online = "0";
                if (radioButton1.Checked)
                {
                    mdl_online = "1";
                }
                else
                {
                    mdl_online = "0";
                }

                string sql = "update `" + DBtable + "` set `mdl_id`='" + textBox6.Text + "' , `mdl_code`='" + textBox7.Text + "' , `mdl_upcode`='" + textBox8.Text +
                             "' , `mdl_name`='" + textBox9.Text + "' , `mdl_folder`='" + textBox10.Text + "' , `mdl_link`='" + textBox11.Text + "' , `mdl_pic`='" + comboBox1.SelectedItem +
                             "' , `mdl_online`='" + mdl_online + "' where mdl_id='" + temp_id + "'";

                MySqlCommand mys_com = new MySqlCommand(sql, mys_conn);
                mys_com.ExecuteNonQuery();

                mys_com.Dispose();
                mys_conn.Dispose();
                mys_conn.Close();
                listBox1.Items.Clear();

                clear_input();

                button1_Click(this, e);
            }
        }

        private void clear_input()
        {
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            comboBox1.SelectedIndex = -1;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                string strconn;
                strconn = "SERVER = " + server + "; DATABASE = " + DBtext + "; User ID =" + user + " ; password = " + password + ";Charset=utf8";

                MySqlConnection mys_conn = new MySqlConnection(strconn);
                mys_conn.Open();

                string mdl_online = "0";
                if (radioButton1.Checked)
                {
                    mdl_online = "1";
                }
                else
                {
                    mdl_online = "0";
                }

                string sql = "insert into `" + DBtable + "`(`mdl_id`, `mdl_code`, `mdl_upcode`, `mdl_name`, `mdl_folder`, `mdl_link`, `mdl_pic`, `mdl_online`)";
                sql += " VALUES ('" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + comboBox1.SelectedItem + "','" +
                       mdl_online + "')";

                MySqlCommand mys_com = new MySqlCommand(sql, mys_conn);
                mys_com.ExecuteNonQuery();

                mys_com.Dispose();
                mys_conn.Dispose();
                mys_conn.Close();
                listBox1.Items.Clear();

                clear_input();

                button1_Click(this, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "" && listBox1.SelectedIndex > -1 && listBox1.SelectedIndex.ToString() == textBox6.Text)
            {
                string strconn;
                strconn = "SERVER = " + server + "; DATABASE = " + DBtext + "; User ID =" + user + " ; password = " + password + ";Charset=utf8";

                MySqlConnection mys_conn = new MySqlConnection(strconn);
                mys_conn.Open();

                string mdl_online = "0";
                if (radioButton1.Checked)
                {
                    mdl_online = "1";
                }
                else
                {
                    mdl_online = "0";
                }

                string sql = "DELETE FROM `" + DBtable + "` WHERE `mdl_id`='" + textBox6.Text + "'";

                MySqlCommand mys_com = new MySqlCommand(sql, mys_conn);
                mys_com.ExecuteNonQuery();

                mys_com.Dispose();
                mys_conn.Dispose();
                mys_conn.Close();
                listBox1.Items.Clear();

                clear_input();

                button1_Click(this, e);
            }
            else
            {
                MessageBox.Show("請選擇項目 and 請勿自行輸入");
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            temp_id = result[listBox1.SelectedIndex][0].ToString();
            textBox6.Text = result[listBox1.SelectedIndex][0].ToString();
            textBox7.Text = result[listBox1.SelectedIndex][1].ToString();
            textBox8.Text = result[listBox1.SelectedIndex][2].ToString();
            textBox9.Text = result[listBox1.SelectedIndex][3].ToString();
            textBox10.Text = result[listBox1.SelectedIndex][4].ToString();
            textBox11.Text = result[listBox1.SelectedIndex][5].ToString();

            comboBox1.SelectedItem = result[listBox1.SelectedIndex][6];

            if (result[listBox1.SelectedIndex][8] == "1")
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }

            //Console.WriteLine(listBox1.SelectedItem);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server = textBox1.Text;
            user = textBox2.Text;
            password = textBox3.Text;
            DBtext = textBox4.Text;
            DBtable = textBox5.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strconn;
            strconn = "SERVER = " + server + "; DATABASE = " + DBtext + "; User ID =" + user + " ; password = " + password + ";Charset=utf8";

            MySqlConnection mys_conn = new MySqlConnection(strconn);
            mys_conn.Open();

            string sql = "select * from " + DBtable + " order by mdl_code asc";

            MySqlCommand mys_com = new MySqlCommand(sql,mys_conn);

            MySqlDataReader mys_read = mys_com.ExecuteReader();
            int listcount = 0;
            while (mys_read.Read())
            {
                if (mys_read.HasRows)
                {
                    listcount++;
                }
            }
            result = new string[listcount][];

            //釋放資源
            mys_read.Dispose();

            //重新載入
            mys_read = mys_com.ExecuteReader();
            int times = 0;
            while (mys_read.Read())
            {
                result[times] = new string[mys_read.FieldCount];
                string str="";
                for (int i = 0; i < mys_read.FieldCount; i++)
                {
                    result[times][i] = mys_read[i].ToString();
                    if (i == 0)
                    {
                        str += mys_read[i].ToString();
                        continue;
                    }
                    str += " , "+ mys_read[i].ToString();
                }
                times++;
                listBox1.Items.Add(str.ToString());
            }

            mys_read.Dispose();
            mys_read.Close();
            mys_com.Dispose();
            mys_conn.Dispose();
            mys_conn.Close();

            
        }
    }
}
