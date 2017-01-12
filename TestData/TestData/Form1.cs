using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using TestData.Database1DataSetTableAdapters;

namespace TestData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            SqlCommand cmd;
            //SqlDataReader read;
            SqlConnection SqlConn;
            SqlDataAdapter adap;

            try
            {
                string ConnStr = ConfigurationManager.ConnectionStrings["TestData.Properties.Settings.Database1ConnectionString"].ToString();
                SqlConn = new SqlConnection(ConnStr);
                SqlConn.Open();

                sql = "insert into dbo.student (name,number) values ('test1','401231531')";
                cmd = new SqlCommand(sql, SqlConn);
                cmd.ExecuteNonQuery();
               
                cmd.Dispose();
                SqlConn.Close();
                SqlConn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.ConnectionStrings["TestData.Properties.Settings.Database1ConnectionString"].ToString();
            string sql;
            SqlDataReader read;
            SqlCommand cmd;
            label1.Text = "";
            try
            {
                SqlConnection SqlConn = new SqlConnection(connstr);
                SqlConn.Open();

                sql = "select * from student";
                cmd = new SqlCommand(sql, SqlConn);

                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    label1.Text += read["name"].ToString();
                }
                SqlConn.Close();
                SqlConn.Dispose();
                cmd.Dispose();
            }
            catch
            {

            }
               
        }
    }
}
