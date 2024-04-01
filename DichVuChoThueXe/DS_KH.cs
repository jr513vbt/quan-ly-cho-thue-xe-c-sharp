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
    public partial class DS_KH : Form
    {
        string Quyen = "";
        public DS_KH()
        {
            InitializeComponent();
        }
        string anh = "D:\\Img\\default.jpg";
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
        private void button3_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            string phai;
            if (checkBox1.Checked)
                phai = "Nam";
            else phai = "Nu";
            cmd = conn.CreateCommand();
            cmd2 = conn.CreateCommand();
            cmd2.CommandText = "insert into TAI_KHOAN values ('" + textBox3.Text + "', '1', '" + textBox10.Text +
                "', 'KH',  '" + anh + "')";
            cmd2.ExecuteNonQuery();
            cmd.CommandText = "insert into KHACH_HANG values ('" + textBox2.Text + "', '" + textBox5.Text +
                "', '" + textBox10.Text + "', '" + textBox3.Text + "', '" + textBox6.Text +
                "', '" + textBox9.Text + "', '" + dateTimePicker1.Value.ToString() + "', '" + phai +
                "', '" + textBox8.Text + "')";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from KHACH_HANG", conn);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            string phai;
            if (checkBox1.Checked)
                phai = "Nam";
            else phai = "Nu";
            cmd = conn.CreateCommand();
            cmd2 = conn.CreateCommand();
            cmd2.CommandText = "UPDATE TAI_KHOAN SET Ten = '" + textBox10.Text + "' WHERE TK = '" + textBox3.Text + "' ";
            cmd2.ExecuteNonQuery();
            cmd.CommandText = "UPDATE KHACH_HANG SET CMND = '" + textBox2.Text + "', ho_KH = '" + textBox5.Text
                + "', ten_KH = '" + textBox10.Text + "',TK_KH = '" + textBox3.Text + "',SDT = '" + textBox6.Text +
               "', Email = '" + textBox9.Text + "', ngaySinh = '" + dateTimePicker1.Value.ToString() + "', gioiTinh = '" + phai +
               "', diaChi = '" + textBox8.Text + "' WHERE TK_KH = '" + textBox3.Text + "'";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from KHACH_HANG", conn);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            cmd = conn.CreateCommand();
            cmd.CommandText = "delete from KHACH_HANG where TK_KH = '" + textBox3.Text + "'";
            cmd.ExecuteNonQuery();
            cmd2 = conn.CreateCommand();
            cmd2.CommandText = "delete from TAI_KHOAN where TK = '" + textBox3.Text + "' ";
            cmd2.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from KHACH_HANG", conn);
        }

        private void DS_KH_Load(object sender, EventArgs e)
        {
            Ketnoi();
            ham h = new ham();
            h.HienThiDG(dataGridView1, "select * from KHACH_HANG", conn);
         /*   if (Quyen == "KH")
            {
                textBox3.Enabled = false;
                button3.Enabled = false;
                button3.BackColor = Color.Gray;
            }   //  */
            String BG = "D:\\Img\\GD3.jpg";
            pictureBox2.Image = Image.FromFile(BG);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            textBox5.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            checkBox1.Checked = false;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string phai = "";
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
            phai = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            if (phai == "Nam")
                checkBox1.Checked = true;
            if (phai == "Nu")
                checkBox1.Checked = false;
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string timkiem = "";
                timkiem = textBox1.Text;
                ham h = new ham();
                h.HienThiDG(dataGridView1, "select * from KHACH_HANG where CMND like '%" + timkiem +
                    "%' OR ho_KH like '%" + timkiem + "%' OR ten_KH like '%" + timkiem + "%' OR tk_KH like '%" + timkiem + 
                    "%' OR SDT like '%" + timkiem + "%' OR email like '%" + timkiem + "%' OR ngaySinh like '%" + timkiem + 
                    "%' OR gioiTinh like '%" + timkiem + "%' OR diaChi like '%" + timkiem + "%' ", conn);
            }
        }
    }
}