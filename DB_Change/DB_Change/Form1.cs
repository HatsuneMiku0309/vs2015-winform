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

namespace DB_Change
{
    public partial class Form1 : Form
    {
        string[] changelist;
        string[][] productlist;
        string last_cid;
        string[][] changeproductlist;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            MySqlCommand mysqlcmd;
            MySqlDataReader reader;
            string strconn = "SERVER = localhost; DATABASE = wwwtouch_main; User ID =root ; password ='';Charset=utf8";
            MySqlConnection sqlconn = new MySqlConnection(strconn);
            sqlconn.Open();

            /*
            using (SqlCommand command = new SqlCommand(
		    "INSERT INTO Dogs1 VALUES(@Weight, @Name, @Breed)", con))
	        {
		    command.Parameters.Add(new SqlParameter("Weight", weight));
            */


            string sql = "select * from categories where categories_id = @categories_id order by categories_id asc";
            mysqlcmd = new MySqlCommand(sql, sqlconn);
            mysqlcmd.Parameters.Add(new MySqlParameter("@categories_id", textBox1.Text));
            //mysqlcmd.Parameters.AddWithValue("@categories_id", textBox1.Text);

            reader = mysqlcmd.ExecuteReader();
            while (reader.Read())
            {
                string value = "";
                changelist = new string[reader.FieldCount];
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var key = reader.GetName(i);
                    changelist[i] = reader[i].ToString();
                    value += reader[i].ToString()+" , ";
                }
                listBox1.Items.Add(value);
            }

            mysqlcmd.Dispose();
            sqlconn.Dispose();
            sqlconn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            MySqlCommand mysqlcmd;
            MySqlDataReader reader;
            string strconn = "SERVER = localhost; DATABASE = pettw_main; User ID =root ; password ='';Charset=utf8";
            MySqlConnection sqlconn = new MySqlConnection(strconn);
            sqlconn.Open();

            /*
            using (SqlCommand command = new SqlCommand(
		    "INSERT INTO Dogs1 VALUES(@Weight, @Name, @Breed)", con))
	        {
		    command.Parameters.Add(new SqlParameter("Weight", weight));
            */


            string sql = "select * from products order by id asc";
            mysqlcmd = new MySqlCommand(sql, sqlconn);

            reader = mysqlcmd.ExecuteReader();
            while (reader.Read())
            {
                string value = "";

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var key = reader.GetName(i);
                    value += reader[i].ToString() + " , ";
                }
                listBox1.Items.Add(value);
            }

            mysqlcmd.Dispose();
            sqlconn.Dispose();
            sqlconn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string myEditText = "";
            string tempname = changelist[2];

            changelist[1] = "0";
            changelist[2] = textBox2.Text;
            changelist[3] = "6";
            changelist[4] = tempname;
            changelist[5] = "NULL";
            changelist[6] = "NULL";
            changelist[7] = "NULL";
            changelist[8] = "NULL";
            changelist[9] = "0";
            changelist[10] = "1";

            for (int i = 0; i < changelist.Length; i++)
            {
                myEditText += changelist[i]+" , ";
            }
            MessageBox.Show(myEditText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlCommand mysqlcmd;
            string strconn = "SERVER = localhost; DATABASE = pettw_main; User ID =root ; password ='';Charset=utf8";
            MySqlConnection sqlconn = new MySqlConnection(strconn);
            sqlconn.Open();

            string sql = "INSERT INTO `p_categories`(`parent_id`, `group_id`, `admin_id`, `name`, `description`, `meta_title`, `meta_keyword`, `meta_description`, `sort`, `status`) VALUES ( "+changelist[1]+ "," + changelist[2] + "," + changelist[3] + ",'" + changelist[4] + "'," + changelist[5] + "," + changelist[6] + "," + changelist[7] + "," + changelist[8] + "," + changelist[9] + "," + changelist[10] + " )";
            //string sql = "INSERT INTO `p_categories`(`parent_id`) VALUES ('0')";
            textBox3.Text = sql;
            mysqlcmd = new MySqlCommand(sql, sqlconn);
            mysqlcmd.ExecuteNonQuery();

            mysqlcmd.Dispose();
            sqlconn.Dispose();
            sqlconn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            MySqlCommand mysqlcmd;
            MySqlDataReader reader;
            string strconn = "SERVER = localhost; DATABASE = pettw_main; User ID =root ; password ='';Charset=utf8";
            MySqlConnection sqlconn = new MySqlConnection(strconn);
            sqlconn.Open();

            string sql = "select * from p_categories order by categories_id desc";
            mysqlcmd = new MySqlCommand(sql, sqlconn);
            mysqlcmd.Parameters.Add(new MySqlParameter("@categories_id", textBox1.Text));
            //mysqlcmd.Parameters.AddWithValue("@categories_id", textBox1.Text);

            reader = mysqlcmd.ExecuteReader();
            if(reader.Read())
            {
                last_cid = reader["categories_id"].ToString();
            }
            //MessageBox.Show(last_cid);
            mysqlcmd.Dispose();
            sqlconn.Dispose();
            sqlconn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            MySqlCommand mysqlcmd;
            MySqlDataReader reader;
            string strconn = "SERVER = localhost; DATABASE = wwwtouch_main; User ID =root ; password ='';Charset=utf8";
            MySqlConnection sqlconn = new MySqlConnection(strconn);
            sqlconn.Open();

            /*
            using (SqlCommand command = new SqlCommand(
		    "INSERT INTO Dogs1 VALUES(@Weight, @Name, @Breed)", con))
	        {
		    command.Parameters.Add(new SqlParameter("Weight", weight));
            */


            string sql = "select count(*) from products where categories_id = @categories_id order by categories_id asc";
            mysqlcmd = new MySqlCommand(sql, sqlconn);
            mysqlcmd.Parameters.Add(new MySqlParameter("@categories_id", textBox1.Text));
            object result =  mysqlcmd.ExecuteScalar();
            int r = 0;
            if (result != null)
            {
                r = Convert.ToInt32(result);
                productlist = new string[r][];
                changeproductlist = new string[r][];
            }
            //mysqlcmd.Parameters.AddWithValue("@categories_id", textBox1.Text);

            sql = "select * from products where categories_id = @categories_id order by categories_id asc";
            mysqlcmd = new MySqlCommand(sql, sqlconn);
            mysqlcmd.Parameters.Add(new MySqlParameter("@categories_id", textBox1.Text));

            reader = mysqlcmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {  
                string value = "";
                productlist[count] = new string[reader.FieldCount];
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var key = reader.GetName(i);
                    string data = "";
                    if (reader[i].ToString() != "")
                    {
                        data = reader[i].ToString();
                    }
                    else
                    {
                        data = "NULL";
                    }
                    //productlist[count][i] = reader[i].ToString();
                    productlist[count][i] = data;
                    //value += reader[i].ToString() + " , ";
                    value += data + " , ";
                }
                count++;
                listBox1.Items.Add(value);
            }

            mysqlcmd.Dispose();
            sqlconn.Dispose();
            sqlconn.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(productlist[0][4]);
            
            for (int i = 0; i < productlist.Length; i++)
            {
                changeproductlist[i] = new string[21];
                string value = "";
                string temp_number = productlist[i][4].ToString();
                string temp_price = productlist[i][6];
                string temp_price2 = productlist[i][7];
                string temp_photo = productlist[i][8];
                string temp_description = productlist[i][9];
                string temp_style_description = productlist[i][10];
                changeproductlist[i][1] = "1";
                changeproductlist[i][2] = last_cid;
                changeproductlist[i][3] = productlist[i][3];
                changeproductlist[i][4] = "1";
                changeproductlist[i][5] = "NULL";
                changeproductlist[i][6] = "0";
                changeproductlist[i][7] = temp_price;
                changeproductlist[i][8] = temp_price2;
                changeproductlist[i][9] = temp_number;
                changeproductlist[i][10] = temp_number;
                changeproductlist[i][11] = "1";
                changeproductlist[i][12] = "NULL";
                changeproductlist[i][13] = temp_description.Replace("'","\\'");
                changeproductlist[i][14] = temp_style_description.Replace("'", "\\'");
                changeproductlist[i][15] = "NULL";
                changeproductlist[i][16] = "NULL";
                changeproductlist[i][17] = "NULL";
                changeproductlist[i][18] = "NULL";
                changeproductlist[i][19] = "NULL";
                changeproductlist[i][20] = temp_photo;
                for (int j = 1; j < 20; j++)
                {
                    value += changeproductlist[i][j]+ " , ";
                }
                listBox2.Items.Add(value);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            MySqlCommand mysqlcmd;
            string sql = "";
            string strconn = "SERVER = localhost; DATABASE = pettw_main; User ID =root ; password ='';Charset=utf8";
            MySqlConnection sqlconn = new MySqlConnection(strconn);
            sqlconn.Open();

            for (int i = 0; i < changeproductlist.Length; i++)
            {
                sql = "INSERT INTO `products`(`admin_id`, `categories_id`, `name`, `status`, `sort`, `price`, `m_price`, `p_number`, `p_number2`, `tax`, `tag`, `description`, `style_description`, `bigphoto`) "
                            + " VALUES ( " + changeproductlist[i][1] + "," + changeproductlist[i][2] + ",'" + changeproductlist[i][3] + "','" + changeproductlist[i][4] + "','" + changeproductlist[i][6] + "','" + changeproductlist[i][7] + "','" + changeproductlist[i][8] + "','" + changeproductlist[i][9] + "','" + changeproductlist[i][10]
                            + "','" + changeproductlist[i][11] + "','" + changeproductlist[i][12] + "','" + changeproductlist[i][13] + "','" + changeproductlist[i][14] + "','" + changeproductlist[i][20] + "')";
                //string sql = "INSERT INTO `p_categories`(`parent_id`) VALUES ('0')";
                textBox3.Text = sql;
                mysqlcmd = new MySqlCommand(sql, sqlconn);
                mysqlcmd.ExecuteNonQuery();
                mysqlcmd.Dispose();
            }
            sqlconn.Dispose();
            sqlconn.Close();
        }
    }
}
