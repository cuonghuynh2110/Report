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

namespace WindowsFormsApp1
{
    public partial class frmDangNhap : Form
    {
        string connectionString = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=QLBANSACH;Integrated Security=True";
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable("taikhoan");
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string query = "select * from taikhoan where tennguoidung = @username";
            string selectTenNV = "select HoLot + ' ' + TenNV from NhanVien where MaNhanVien = @manv";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", this.txtUsername.Text); //Thêm tham số và giá trị tương ứng
            SqlDataAdapter da = new SqlDataAdapter(cmd); // Khai báo DataAdapter
            //cmd.ExecuteScalar();
            
            DataTable tk = new DataTable();
            DataTable nv = new DataTable();
            da.Fill(tk);
            if (tk.Rows.Count > 0)
            {
                string manv = tk.Rows[0][2].ToString();
                cmd.CommandText = selectTenNV;
                cmd.Parameters.AddWithValue("@manv", manv);
                da.SelectCommand = cmd;
                da.Fill(nv);
                string tenNV = nv.Rows[0][0].ToString();
                
                frmBaoCao f = new frmBaoCao(tenNV);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại!");
            }
        }
    }
}
