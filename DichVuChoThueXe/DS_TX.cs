using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace DichVuChoThueXe
{
    public partial class DS_TX : Form
    {
        string TenDangNhap = "", Ten = "", Quyen = "";
        public DS_TX(string TenDangNhap, string Ten, string Quyen)
        {
            InitializeComponent();
            this.TenDangNhap = TenDangNhap;
            this.Ten = Ten;
            this.Quyen = Quyen;
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
            ham h = new ham();
            string Status = "Chua xu ly";
            if (comboBox1.SelectedItem == null) comboBox1.SelectedItem = Status;
            cmd = conn.CreateCommand();
            cmd.CommandText = "insert into THUE_XE values ('" + textBox2.Text + "', '" + textBox5.Text +
                "', '" + textBox10.Text + "', '" + textBox3.Text + "', '" + textBox6.Text +
                "', '" + textBox9.Text + "', '" + textBox4.Text + "', '" + dateTimePicker1.Value.ToString() +
                "', '" + dateTimePicker2.Value.ToString() + "', '" + comboBox1.SelectedItem.ToString() + "')";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from THUE_XE", conn);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE THUE_XE SET ID_TX = '" + textBox2.Text + "', Ten_KH = '" + textBox5.Text +
               "', CMND = '" + textBox10.Text + "',SDT = '" + textBox3.Text + "',moTaNhuCau = '" + textBox6.Text +
               "', CP_ToiThieu = '" + textBox9.Text + "', CP_ToiDa = '" + textBox4.Text + 
               "', ngayBD = '" + dateTimePicker1.Value.ToString() + "', ngayTra = '" + dateTimePicker2.Value.ToString() + 
               "', trangThai = '" + comboBox1.SelectedItem.ToString() + "' WHERE ID_TX = '" + textBox2.Text + 
               "' AND Ten_KH = '" + textBox5.Text + "' AND CMND = '" + textBox10.Text + "'";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from THUE_XE", conn);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            if (comboBox1.SelectedItem != "Chua xu ly")
            {
                MessageBox.Show("Yeu cau dang trong qua trinh xu ly hoac da xu ly");
            }
            else
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "delete from THUE_XE where ID_TX = '" + textBox2.Text + "' AND Ten_KH = '" + textBox5.Text +
                    "' AND CMND = '" + textBox10.Text + "'";
                cmd.ExecuteNonQuery();
                h.HienThiDG(dataGridView1, "select * from THUE_XE", conn);
            }            
        }

        private void DS_TX_Load(object sender, EventArgs e)
        {
            Ketnoi();
            ham h = new ham();
            h.HienThiDG(dataGridView1, "select * from THUE_XE", conn);
            String BG = "D:\\Img\\GD3.jpg";
            pictureBox2.Image = Image.FromFile(BG);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            if (Quyen == "KH")
            {
                textBox5.Enabled = false;
                textBox10.Enabled = false;
                textBox3.Enabled = false;
                comboBox1.Enabled = false;
                        /*
                textBox3.BackColor = Color.Gray;
                textBox3.ForeColor = Color.Red;     //      */
                cmd = conn.CreateCommand();
                cmd.CommandText = "select * from KHACH_HANG where TK_KH = '" + TenDangNhap + "'";
                cmd.ExecuteNonQuery();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox5.Text = reader.GetString(2);
                        textBox10.Text = reader.GetString(0);
                        textBox3.Text = reader.GetString(4);

                    }
                }
            }
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Quyen != "KH")
            {
                textBox3.Text = "";
                textBox5.Text = "";
                textBox10.Text = "";
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            textBox9.Text = "";
            comboBox1.SelectedItem = "Chua xu ly";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string timkiem = "";
                timkiem = textBox1.Text;
                ham h = new ham();
                h.HienThiDG(dataGridView1, "select * from THUE_XE where ID_TX like '%" + timkiem +
                    "%' OR ten_KH like '%" + timkiem + "%' OR CMND like '%" + timkiem + "%' OR SDT like '%" + timkiem +
                    "%' OR moTaNhuCau like '%" + timkiem + "%' OR cp_toithieu like '%" + timkiem + "%' OR cp_toida like '%" + timkiem +
                    "%' OR ngayBD like '%" + timkiem + "%' OR ngayTra like '%" + timkiem + "%' ", conn);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Quyen != "KH")
            {
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            }
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

            dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
            dateTimePicker2.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());

            comboBox1.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
        }
    }
}
