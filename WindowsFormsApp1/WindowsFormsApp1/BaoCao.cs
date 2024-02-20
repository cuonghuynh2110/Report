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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace WindowsFormsApp1
{
    public partial class frmBaoCao : Form
    {
        string connectionString = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=QLBANSACH;Integrated Security=True";
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable("HoaDon");
        private string tenNhanVien;
        public frmBaoCao(string tennv)
        {
            tenNhanVien = tennv;
            InitializeComponent();
        }

        private void crystalReportViewer2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            string sqlFormattedDate = selectedDate.ToString("yyyy-MM-dd");

            string query = "SELECT h.MaHoaDon, k.HoLot + ' ' + k.Ten AS 'TenKH', k.Phai, k.DiaChi, h.TongTien, h.NgayBan FROM HoaDon h, KhachHang k WHERE h.MaKhachHang = k.MaKH AND h.NgayBan = @ngayban";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ngayban", sqlFormattedDate); //Thêm tham số và giá trị tương ứng
            SqlDataAdapter da = new SqlDataAdapter(cmd); // Khai báo DataAdapter
            DataTable dt = new DataTable(); // Khai báo DataTable
            da.Fill(dt); // Đổ dữ liệu vào DataTable
            
            //gán dữ liệu cho report
            rptHoaDon baocao = new rptHoaDon();
            baocao.SetDataSource(dt);
            TextObject txtNguoiLap = (TextObject)baocao.ReportDefinition.Sections["Section1"].ReportObjects["TxtobjNguoiLap"];
            txtNguoiLap.Text = "Người Lập: " + this.tenNhanVien;
            //Hiển thị báo cáo
            this.crystalReportViewer1.ReportSource = baocao;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }
    }
}
