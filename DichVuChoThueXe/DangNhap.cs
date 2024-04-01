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
using System.IO;

namespace DichVuChoThueXe
{
    public partial class DangNhap : Form
    {        
        public DangNhap()
        {
            InitializeComponent();
            String Path = "D:\\Img\\lock.gif";
            pictureBox1.Image = Image.FromFile(Path);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        }
        public SqlConnection conn;
        public void Ketnoi()
        {
            string chuoiketnoi = "SERVER = VNPC10-HPFOLIO\\TH_HUY_FPT; database = NL_DVCTX; Integrated Security = True";
            conn = new SqlConnection();
            conn.ConnectionString = chuoiketnoi;
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ketnoi();
            ham h = new ham();
            h.DangNhap(textBox1.Text, textBox2.Text, conn);       
            this.Hide();
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DangKy dk = new DangKy();
            dk.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
