using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication1.stud
{
    public partial class IO_compression : Form
    {
        public IO_compression()
        {
            InitializeComponent();
        }

        private void IO_compression_Load(object sender, EventArgs e)
        {
            button1.Text = "壓縮";
            button2.Text = "解壓縮";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string readpath = Application.StartupPath + @"\test";
            string generatecompressfile = Application.StartupPath + "compress.zip";
            try
            {
                ZipFile.CreateFromDirectory(readpath,generatecompressfile,CompressionLevel.NoCompression,true);
                MessageBox.Show("壓縮檔案完成,路徑:" + readpath, "資訊");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "發生例外");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string extracpath = Application.StartupPath + @"\output";
            string generatecompressfile = Application.StartupPath + "compress.zip";
            try
            {
                ZipFile.ExtractToDirectory(generatecompressfile, extracpath);
                MessageBox.Show("解壓縮檔案完成,路徑:" + extracpath, "資訊");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "發生例外");
            }
        }
    }
}
