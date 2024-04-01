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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace DichVuChoThueXe
{
    public partial class DS_XCT : Form
    {
        string Quyen = "";
        public DS_XCT(string quyen)
        {
            InitializeComponent();
            this.Quyen = quyen;
        }
        string anh = "D:\\Img\\default.jpg";
        public SqlConnection conn;
        public SqlCommand cmd;
        public void Ketnoi()
        {
            string chuoiketnoi = "SERVER = VNPC10-HPFOLIO\\TH_HUY_FPT; database = NL_DVCTX; Integrated Security = True";
            conn = new SqlConnection();
            conn.ConnectionString = chuoiketnoi;
            conn.Open();
        }
        private void DS_XCT_Load(object sender, EventArgs e)
        {
            Ketnoi();
            ham h = new ham();
            h.HienThiDG(dataGridView1, "select * from XE_CHO_THUE", conn);
            pictureBox1.Image = Image.FromFile(anh);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            String BG = "D:\\Img\\GD3.jpg";
            pictureBox2.Image = Image.FromFile(BG);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            if (Quyen == "KH")
            {
                button3.Enabled = false;
                button3.BackColor = Color.Gray;
                button5.Enabled = false;
                button5.BackColor = Color.Gray;
                button7.Enabled = false;
                button7.BackColor = Color.Gray;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TrangThaiXe f = new TrangThaiXe();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            cmd = conn.CreateCommand();
            cmd.CommandText = "insert into XE_CHO_THUE values ('" + textBox2.Text + "', '" + textBox5.Text +
                "', '" + textBox6.Text + "', '" + anh + "', '" + textBox3.Text +
                "', '" + comboBox1.SelectedItem.ToString() + "' )";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from XE_CHO_THUE", conn);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE XE_CHO_THUE SET ID_XCT = '" + textBox2.Text + "', hangXe = '" + textBox5.Text +
               "', moTa = '" + textBox6.Text + "', hinhAnh = '" + anh + "', giaChoThue = '" + textBox3.Text +
               "', trangThai = '" + comboBox1.SelectedItem.ToString() + "' WHERE ID_XCT = '" + textBox2.Text + "'";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from XE_CHO_THUE", conn);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            cmd = conn.CreateCommand();
            cmd.CommandText = "delete from XE_CHO_THUE where ID_XCT = '" + textBox2.Text + "'";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from XE_CHO_THUE", conn);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedItem = "1";
            textBox5.Text = "";
            textBox6.Text = "";
            anh = "D:\\Img\\default.jpg";
            pictureBox1.Image = Image.FromFile(anh);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                anh = open.FileName;
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            anh = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            pictureBox1.Image = Image.FromFile(anh);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            comboBox1.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }
                private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string timkiem = "";
                timkiem = textBox1.Text;
                Ketnoi();
                ham h = new ham();
                h.HienThiDG(dataGridView1, "select * from XE_CHO_THUE where ID_XCT like '%" + timkiem +
                    "%' OR hangXe like '%" + timkiem + "%' OR moTa like '%" + timkiem + "%' OR hinhAnh like '%" + timkiem +
                    "%' OR giaChoThue like '%" + timkiem + "%' OR trangThai like '%" + timkiem + "%' ", conn);
            }
        }
    }
}
