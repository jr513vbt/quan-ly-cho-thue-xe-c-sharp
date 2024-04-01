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
    public partial class TT_CaNhan : Form
    {

        string TaiKhoan = "", Ten = "", Quyen ="", anh = "";
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlCommand cmd2;
        public void Ketnoi()
        {
            string chuoiketnoi = "SERVER = VNPC10-HPFOLIO\\TH_HUY_FPT; database = NL_DVCTX; Integrated Security = True";
            conn = new SqlConnection();
            conn.ConnectionString = chuoiketnoi;
            conn.Open();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            string phai;
            if (checkBox1.Checked)
                phai = "Nam";
            else phai = "Nu";
            cmd = conn.CreateCommand();
            if (Quyen == "KH")
            {
                cmd.CommandText = "UPDATE KHACH_HANG SET CMND = '" + textBox10.Text + "', ho_KH = '" + textBox9.Text +
                "', ten_KH = '" + textBox2.Text + "',TK_KH = '" + textBox3.Text + "',SDT = '" + textBox8.Text +
                "', Email = '" + textBox1.Text + "', ngaySinh = '" + dateTimePicker1.Value.ToString() + "', gioiTinh = '" + phai +
                "', diaChi = '" + textBox4.Text + "' WHERE TK_KH = '" + textBox3.Text + "' ";
                cmd.ExecuteNonQuery();
                cmd2 = conn.CreateCommand();
                cmd2.CommandText = "UPDATE TAI_KHOAN set Ten = '" + textBox2.Text + "', Avatar = '" + anh + 
                    "'  WHERE TK = '" + textBox3.Text + "' ";
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                cmd2 = conn.CreateCommand();
                cmd2.CommandText = "UPDATE TAI_KHOAN set Ten = '" + textBox2.Text + "', Avatar = '" + anh + 
                    "'  WHERE TK = '" + textBox3.Text + "' ";
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
            }

        }

        private void TT_CaNhan_Load(object sender, EventArgs e)
        {
            Ketnoi();
            string phai = "";
            cmd = conn.CreateCommand();
            cmd.CommandText = "select * from KHACH_HANG where Ten_KH = '" + textBox2.Text + "' and TK_KH = '" + textBox3.Text + "'";
            cmd.ExecuteNonQuery();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    textBox10.Text = reader.GetString(0);
                    textBox9.Text = reader.GetString(1);
                    textBox8.Text = reader.GetString(4);
                    textBox1.Text = reader.GetString(5);
                    dateTimePicker1.Value = DateTime.Parse(reader.GetDateTime(6).ToString());                    
                    phai = reader.GetString(7);
                    if (phai == "Nam")
                        checkBox1.Checked = true;
                    if (phai == "Nu")
                        checkBox1.Checked = false;

                    textBox4.Text = reader.GetString(8);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg;*.jpeg;*.gif)|*.jpg;*.jpeg;*.gif";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                anh = open.FileName;
            }
        }

        public TT_CaNhan(string TaiKhoan, string Ten, string Quyen, string anh)
        {
            InitializeComponent();
            this.TaiKhoan = TaiKhoan;
            this.Ten = Ten;
            this.Quyen = Quyen;
            this.anh = anh;
            pictureBox1.Image = Image.FromFile(anh);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
         //   label2.Text = Ten;
            textBox3.Text = TaiKhoan;
            textBox2.Text = Ten;
            textBox3.Enabled = false;
            if (Quyen != "KH")
            {
                textBox10.Enabled = false;
                textBox10.BackColor = Color.Gray;
                textBox9.Enabled = false;
                textBox9.BackColor = Color.Gray;
                textBox8.Enabled = false;
                textBox8.BackColor = Color.Gray;
                textBox1.Enabled = false;
                textBox1.BackColor = Color.Gray;
                textBox4.Enabled = false;
                textBox4.BackColor = Color.Gray;
                checkBox1.Enabled = false;
                checkBox1.BackColor = Color.Gray;
                dateTimePicker1.Enabled = false;
             //   dateTimePicker1.BackColor = Color.Gray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
