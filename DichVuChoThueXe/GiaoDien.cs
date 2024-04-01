using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DichVuChoThueXe
{
    public partial class GiaoDien : Form
    {
        string TenDangNhap = "", MatKhau = "", Ten = "", Quyen = "", Avatar = "";
        public GiaoDien(string TenDangNhap, string MatKhau, string Ten, string Quyen, string Avatar)
        {
            InitializeComponent();
            this.TenDangNhap = TenDangNhap;
            this.MatKhau = MatKhau;
            this.Ten = Ten;
            this.Quyen = Quyen;
            this.Avatar = Avatar;
            pictureBox1.Image = Image.FromFile(Avatar);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label1.Text = Ten;
            String BG = "D:\\Img\\GD2.png";
            pictureBox2.Image = Image.FromFile(BG);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void GiaoDien_Load(object sender, EventArgs e)
        {
            if (Quyen == "KH")
            {
                button3.Enabled = false;
                button3.BackColor = Color.Gray;
                button8.Enabled = false;
                button8.BackColor = Color.Gray;
                button5.Enabled = false;
                button5.BackColor = Color.Gray;
            }
            if (Quyen == "NV")
            {
                button5.Enabled = false;
                button5.BackColor = Color.Gray;
            }
        }
        public SqlConnection conn;
        public SqlCommand cmd;
        public void Ketnoi()
        {
            string chuoiketnoi = "SERVER = VNPC10-HPFOLIO\\TH_HUY_FPT; database = NL_DVCTX; Integrated Security = True";
            conn = new SqlConnection();
            conn.ConnectionString = chuoiketnoi;
            conn.Open();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            TT_CaNhan f = new TT_CaNhan(TenDangNhap, Ten, Quyen, Avatar);
            f.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DS_KH f = new DS_KH();
            f.Show();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            DS_TX f = new DS_TX(TenDangNhap, Ten, Quyen);
            f.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string Path = "";
            Ketnoi();
            cmd = conn.CreateCommand();
            cmd.CommandText = "select * from TAI_KHOAN where TK = '" + TenDangNhap + "' and MK = '" + MatKhau + "' ";
            cmd.ExecuteNonQuery();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Ten = reader.GetString(2);
                    Path = reader.GetString(4);
                }
            }
            pictureBox1.Image = Image.FromFile(Path);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            Avatar = Path;
            label1.Text = Ten;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            DS_HD f = new DS_HD(Quyen);
            f.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DS_XCT f = new DS_XCT(Quyen);
            f.Show();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            DS_TK f = new DS_TK();
            f.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DangNhap f = new DangNhap();
            f.Show();
            this.Hide();
        }
    }
}
