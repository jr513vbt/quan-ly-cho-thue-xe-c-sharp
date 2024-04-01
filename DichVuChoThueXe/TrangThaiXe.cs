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

namespace DichVuChoThueXe
{
    public partial class TrangThaiXe : Form
    {
        public TrangThaiXe()
        {
            InitializeComponent();
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
        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TrangThaiXe_Load(object sender, EventArgs e)
        {
            Ketnoi();
            ham h = new ham();
            h.HienThiDG(dataGridView1, "select * from TRANG_THAI", conn);
        }
    }
}
