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
    public partial class DS_TK : Form
    {
        public DS_TK()
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
        private void DS_TK_Load(object sender, EventArgs e)
        {
            Ketnoi();
            ham h = new ham();
            h.HienThiDG(dataGridView1, "select * from TAI_KHOAN", conn);
            pictureBox1.Image = Image.FromFile(anh);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            String BG = "D:\\Img\\GD1.jpg";
            pictureBox2.Image = Image.FromFile(BG);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            if (anh == "D:\\Img\\default.jpg" ) anh = "D:\\Img\\Avatar\\default1.jpg";
            if (comboBox1.SelectedItem.ToString() == null) comboBox1.SelectedItem = "KH";
            cmd = conn.CreateCommand();
            if (comboBox1.SelectedItem == "KH")
            {
                cmd2 = conn.CreateCommand();
                cmd2.CommandText = "insert into KHACH_HANG values ('', '', '" + textBox4.Text + "', '" + textBox2.Text + 
                    "', '', '', '', '', '' )";
                cmd2.ExecuteNonQuery();
            }
            cmd.CommandText = "insert into TAI_KHOAN values ('" + textBox2.Text + "', '" + textBox3.Text +
                "', '" + textBox4.Text + "', '" + comboBox1.SelectedItem.ToString() + "', '" + anh + "' )";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from TAI_KHOAN", conn);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            if (anh == null) anh = "D:\\Img\\default.jpg";
            if (comboBox1.SelectedItem.ToString() == null) comboBox1.SelectedItem = "KH";
            cmd = conn.CreateCommand();
            if (comboBox1.SelectedItem == "KH")
            {
                cmd2 = conn.CreateCommand();
                cmd2.CommandText = "UPDATE KHACH_HANG SET  ten_KH = '" + textBox4.Text + "', TK_KH = '" + textBox2.Text +
                    "' WHERE TK_KH = '" + textBox2.Text + "' ";
                cmd2.ExecuteNonQuery();
            }
            cmd.CommandText = "UPDATE TAI_KHOAN SET Tk = '" + textBox2.Text + "', Mk = '" + textBox3.Text +
               "', Ten = '" + textBox4.Text + "', Quyen = '" + comboBox1.SelectedItem.ToString() + "', Avatar = '" +anh +
               "' WHERE Tk = '" + textBox2.Text + "'";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from TAI_KHOAN", conn);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            cmd = conn.CreateCommand();
            cmd.CommandText = "delete from TAI_KHOAN where Tk = '" + textBox2.Text + "'";
            cmd.ExecuteNonQuery();

            cmd2 = conn.CreateCommand();
            cmd2.CommandText = "delete from KHACH_HANG where TK_KH = '" + textBox2.Text + "'";
            cmd2.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from TAI_KHOAN", conn);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedItem = "KH";
            anh = "D:\\Img\\default.jpg";
            pictureBox1.Image = Image.FromFile(anh);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            comboBox1.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            anh = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            pictureBox1.Image = Image.FromFile(anh);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string timkiem = "";
                timkiem = textBox1.Text;
                ham h = new ham();
                h.HienThiDG(dataGridView1, "select * from TAI_KHOAN where TK like '%" + timkiem +
                    "%' OR MK like '%" + timkiem + "%' OR Ten like '%" + timkiem +
                    "%' OR Quyen like '%" + timkiem + "%' OR Avatar like '%" + timkiem + "%' ", conn);
            }
        }
    }
}
