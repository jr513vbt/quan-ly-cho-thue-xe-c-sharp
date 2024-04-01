using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace DichVuChoThueXe
{
    public partial class DS_HD : Form
    {
        string Quyen = "", MaHD = "";
        public DS_HD(string Quyen)
        {
            InitializeComponent();
            this.Quyen = Quyen;
        }
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
            cmd = conn.CreateCommand();
            cmd2 = conn.CreateCommand();
            cmd2.CommandText = "insert into CT_THANHTOAN values ('" + textBox2.Text + "', '" + textBox2.Text + "' ,'', '', '', '' ) ";
            cmd2.ExecuteNonQuery();
            cmd.CommandText = "insert into HOP_DONG values ('" + textBox2.Text + "', '" + textBox5.Text +
                "', '" + textBox4.Text + "', '" + textBox10.Text + "', '" + textBox3.Text + "', '" + dateTimePicker1.Value.ToString() +
                "', '" + dateTimePicker2.Value.ToString() + "', '')";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from HOP_DONG", conn);
        }

        private void DS_HD_Load(object sender, EventArgs e)
        {
            Ketnoi();
            ham h = new ham();
            h.HienThiDG(dataGridView1, "select * from HOP_DONG", conn);
            String BG = "D:\\Img\\GD3.jpg";
            pictureBox2.Image = Image.FromFile(BG);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            textBox7.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE HOP_DONG SET ID_HD = '" + textBox2.Text + "', CMND = '" + textBox5.Text +
               "', TK_KH = '" + textBox4.Text + "', ten_KH = '" + textBox10.Text + "',ID_XCT = '" + textBox3.Text + 
               "', ngayBatDau = '" + dateTimePicker1.Value.ToString() +
               "', ngayTra = '" + dateTimePicker2.Value.ToString() + "' WHERE ID_HD = '" + textBox2.Text + "'";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from HOP_DONG", conn);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            cmd = conn.CreateCommand();
            cmd.CommandText = "delete from HOP_DONG where ID_HD = '" + textBox2.Text + "'";
            cmd.ExecuteNonQuery();
            cmd2 = conn.CreateCommand();
            cmd2.CommandText = "delete from CT_THANHTOAN where ID_CTTT = '" + textBox2.Text + "'";
            cmd2.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from HOP_DONG", conn);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            textBox5.Text = "";
            textBox10.Text = "";
            textBox7.Text = "";
            ham h = new ham();
            h.HienThiDG(dataGridView1, "select * from HOP_DONG", conn);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ham h = new ham();
            h.HienThiDG(dataGridView1, "select * from HOP_DONG", conn);
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
            dateTimePicker2.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
            textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string timkiem = "";
                timkiem = textBox1.Text;
                ham h = new ham();
                h.HienThiDG(dataGridView1, "select * from HOP_DONG where ID_HD like '%" + timkiem +
                    "%' OR CMND like '%" + timkiem + "%' OR TK_KH like '%" + timkiem + "%' OR ten_KH like '%" + timkiem +
                    "%' OR ID_XCT like '%" + timkiem + "%' OR ngayBatDau like '%" + timkiem + "%' OR ngayTra like '%" + timkiem +
                    "%' OR tongChiPhi like '%" + timkiem + "%' ", conn);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MaHD = textBox2.Text;
            CT_TT f = new CT_TT(MaHD, Quyen);
            f.Show();
        }
    }
}
