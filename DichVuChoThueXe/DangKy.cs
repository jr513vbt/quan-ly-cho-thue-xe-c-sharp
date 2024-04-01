using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DichVuChoThueXe
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
            String Path = "D:\\Img\\Trash, I think @@\\Gif\\background (1).gif";
            pictureBox1.Image = Image.FromFile(Path);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
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
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox2.Text == textBox4.Text)
            {
                Ketnoi();
                string ex2 = "";
                string query = "insert into TAI_KHOAN values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + 
                    "', 'KH', 'D:\\Img\\Avatar\\default1.jpg')";
                try  
                {
                    SqlCommand comd = new SqlCommand(query, conn);
                    comd.ExecuteNonQuery();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "insert into KHACH_HANG values ('', '', '', '" + textBox1.Text + "', '', '', '', '', '')";
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    ex2 = ex.Message;
                }
                if (ex2 == "")
                    MessageBox.Show("Dang ky tai khoan thanh cong");
            }
            else MessageBox.Show("Mat khau nhap lai khong dung");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ketnoi();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from tai_khoan where Tk = '" + textBox1.Text + "' ", conn);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "tk");

            if (dataset.Tables[0].Rows.Count > 0)
            {
                //string TenDangNhap = dataset.Tables[0].Rows[0][0].ToString();
                MessageBox.Show("Tai khoan da ton tai!!");
            }
            else MessageBox.Show("OK");
        }
    }
}
