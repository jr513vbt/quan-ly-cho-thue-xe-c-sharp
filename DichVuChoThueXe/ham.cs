using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DichVuChoThueXe
{
    internal class ham
    {
        public void DangNhap(string user, string pass, SqlConnection conn)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select * from TAI_KHOAN where TK = '" + user + "' and MK = '" + pass + "'", conn);
            //MessageBox.Show("select * from TAI_KHOAN where TK = '" + user + "' and MK = '" + pass + "'");
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "tk");

            if (dataset.Tables[0].Rows.Count > 0)
            {
                string TenDangNhap = dataset.Tables[0].Rows[0][0].ToString();
                string MatKhau = dataset.Tables[0].Rows[0][1].ToString();
                string Ten = dataset.Tables[0].Rows[0][2].ToString();
                string Quyen = dataset.Tables[0].Rows[0][3].ToString();
                string Avatar = dataset.Tables[0].Rows[0][4].ToString();
                GiaoDien f = new GiaoDien(TenDangNhap, MatKhau, Ten, Quyen, Avatar);
                f.Show();
            }
            else
            {
                MessageBox.Show("Tk, Mk khong chinh xac");
                DangNhap f = new DangNhap();
                f.Show();
            }
        }
        public void HienThiDG(DataGridView dg, string cautruyvan, SqlConnection conn)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(cautruyvan, conn);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "data");
            dg.DataSource = dataset;
            dg.DataMember = "data";
        }
    }
}
