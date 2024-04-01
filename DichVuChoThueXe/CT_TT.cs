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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DichVuChoThueXe
{
    public partial class CT_TT : Form
    {
        string MaHD = "", Quyen = "";
        public CT_TT(string MaHD, string Quyen)
        {
            InitializeComponent();
            this.MaHD = MaHD;
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
            /*
        private void button3_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            cmd = conn.CreateCommand();
            cmd.CommandText = "insert into CT_THANHTOAN values ('" + textBox2.Text + "', '" + textBox5.Text +
                "', '" + textBox10.Text + "', '" + textBox3.Text + "', '" + textBox6.Text + "', '" + textBox9.Text + "' )";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from CT_THANHTOAN", conn);
        } */
        private void button5_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            cmd = conn.CreateCommand();
            cmd2 = conn.CreateCommand();
            cmd2.CommandText = "UPDATE HOP_DONG SET tongChiPHi = '" + textBox9.Text + "' WHERE ID_HD = '" + textBox5.Text + "' ";
            cmd2.ExecuteNonQuery();
            cmd.CommandText = "UPDATE CT_THANHTOAN SET ID_CTTT = '" + textBox5.Text + "', ID_HD = '" + textBox5.Text +
               "', tienDatCoc = '" + textBox10.Text + "', chiPhiThueXe = '" + textBox3.Text + "', chiPhiPhatSinh = '" + textBox6.Text +
               "', tongChiPhi = '" + textBox9.Text + "' WHERE ID_CTTT = '" + textBox5.Text + "'";
            cmd.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from CT_THANHTOAN", conn);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ham h = new ham();
            cmd = conn.CreateCommand();
            cmd.CommandText = "delete from CT_THANHTOAN where ID_CTTT = '" + textBox5.Text + "'";
            cmd.ExecuteNonQuery();
            cmd2 = conn.CreateCommand();
            cmd2.CommandText = "delete from HOP_DONG where ID_HD = '" + textBox5.Text + "'";
            cmd2.ExecuteNonQuery();
            h.HienThiDG(dataGridView1, "select * from CT_THANHTOAN", conn);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox5.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string timkiem = "";
                timkiem = textBox1.Text;
                ham h = new ham();
                h.HienThiDG(dataGridView1, "select * from CT_THANHTOAN where ID_CTTT like '%" + timkiem +
                    "%' OR ID_HD like '%" + timkiem + "%' OR tienDatCoc like '%" + timkiem +
                    "%' OR chiPhiThueXe like '%" + timkiem + "%' OR chiPhiPhatSinh like '%" + timkiem + 
                    "%' OR tongChiPhi like '%" + timkiem + "%' ", conn);
            }
        }

        private void CT_TT_Load(object sender, EventArgs e)
        {
            Ketnoi();
            ham h = new ham();
            h.HienThiDG(dataGridView1, "select * from CT_THANHTOAN", conn);
            String BG = "D:\\Img\\doanh-nghiep394505.jpg";
            pictureBox2.Image = Image.FromFile(BG);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            cmd = conn.CreateCommand();
            cmd.CommandText = "select * from CT_THANHTOAN where ID_HD = '" + MaHD + "' ";
            cmd.ExecuteNonQuery();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    textBox5.Text = reader.GetString(1);
                    textBox10.Text = reader.GetString(2);
                    textBox3.Text = reader.GetString(3);
                    textBox6.Text = reader.GetString(4);
                    textBox9.Text = reader.GetString(5);
                }
            }
        }
    }
}
