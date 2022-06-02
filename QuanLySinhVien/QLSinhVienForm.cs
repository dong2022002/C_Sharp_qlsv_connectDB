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

namespace QuanLySinhVien
{
    public partial class QLSinhVienForm : Form
    {
        List<SinhVien> _dsSinhVien;
        List<Lop> _dsLop;
        
        public QLSinhVienForm()
        {
            InitializeComponent();
        }

        private void QLSinhVienForm_Load(object sender, EventArgs e)
        {
            _dsLop = new List<Lop>();
            _dsSinhVien = new List<SinhVien>();
            LoadDSLop();
            LoadDSSinhVien();
        }

        private void LoadDSSinhVien()
        {
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["DBSinhVien"].ToString();
            SqlConnection sqlConnection = new SqlConnection(con);
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = "select sv.ID,HoTen,TenLop,sv.MaLop from SinhVien sv , Lop l where sv.MaLop=l.ID";
            sqlConnection.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            DisplaySinhVien(sqlDataReader);
            sqlConnection.Close();
        }

        

        private void DisplaySinhVien(SqlDataReader sqlDataReader)
        {
            SinhVien sv;
            lvDSSV.Items.Clear();
            while (sqlDataReader.Read())
            {
                sv = new SinhVien();
                //ListViewItem item = new ListViewItem(sqlDataReader["ID"].ToString());
                sv.MSSV = sqlDataReader["ID"].ToString();
                //item.SubItems.Add(sqlDataReader["HoTen"].ToString());
                sv.HoTen = sqlDataReader["HoTen"].ToString();
                //item.SubItems.Add(sqlDataReader["TenLop"].ToString());
                sv.MaLop = (int)sqlDataReader["MaLop"];
                //lvDSSV.Items.Add(item);
                _dsSinhVien.Add(sv);
            }
            LoadListView(_dsSinhVien);
        }

        private void LoadDSLop()
        {
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["DBSinhVien"].ToString();
            SqlConnection sqlConnection = new SqlConnection(con);
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = "select * from Lop";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlConnection.Open();
            adapter.Fill(dt);
            sqlConnection.Close();
            sqlConnection.Dispose();
            ThemDSLop(dt);
            cboLop.DataSource = dt;
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "ID";
        }

        private void ThemDSLop(DataTable dt)
        {
            Lop lop;
            foreach (DataRow row in dt.Rows)
            {
                lop = new Lop();
                lop.MaLop = (int)row["ID"];
                lop.TenLop = row["TenLop"].ToString();
                _dsLop.Add(lop);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtHoTenSV.Clear();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadDSSinhVien();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SinhVien sv;
            if (string.IsNullOrEmpty(txtHoTenSV.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
            sv = GetSinhVien();
            if (string.IsNullOrEmpty(sv.MSSV.ToString()))
            {
                ThemSV(sv);
                LoadDSSinhVien();
            }
            else
            {
                CapNhatSV(sv);
                LoadDSSinhVien();
            }

        }

        private void ThemSV(SinhVien sv)
        {
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["DBSinhVien"].ToString();
            SqlConnection sqlConnection = new SqlConnection(con);
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = "exec InsertStudent N'" + sv.HoTen + "'," + sv.MaLop;
            sqlConnection.Open();
            int num = cmd.ExecuteNonQuery();
            if (num>0)
            {
                txtHoTenSV.Clear();
                txtID.Clear();
            }
            sqlConnection.Close();
            sqlConnection.Dispose();
        }

        private void CapNhatSV(SinhVien sv)
        {
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["DBSinhVien"].ToString();
            SqlConnection sqlConnection = new SqlConnection(con);
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = "select TenLop from Lop where Lop.ID="+sv.MaLop;
            
            sqlConnection.Open();
            string lop = cmd.ExecuteScalar().ToString();
            cmd.CommandText = "update SinhVien set HoTen=N'" + txtHoTenSV.Text + "',MaLop=" + sv.MaLop + "where id =" + sv.MSSV;
            int num = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            if (num > 0)
            {
                ListViewItem item = lvDSSV.SelectedItems[0];
                item.SubItems[1].Text =txtHoTenSV.Text;
                item.SubItems[2].Text = lop;
            }
            
        }

        private SinhVien GetSinhVien()
        {
            SinhVien sv = new SinhVien();
            sv.MSSV = txtID.Text;
            sv.HoTen = txtHoTenSV.Text;
            sv.MaLop = (int)cboLop.SelectedValue;
            return sv;
        }

        private void lvDSSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            int num = lvDSSV.SelectedItems.Count;
            if (num>0)
            {
                ListViewItem item = lvDSSV.SelectedItems[0];
                txtHoTenSV.Text = item.SubItems[1].Text;
                txtID.Text = item.SubItems[0].Text;
                cboLop.Text = item.SubItems[2].Text;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                var rs = _dsSinhVien.Where(sv => sv.HoTen.IndexOf(txtSearch.Text, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                LoadListView(rs);
            }
            else
            {
                LoadListView(_dsSinhVien);
            }
        }

        private void LoadListView(List<SinhVien> rs)
        {
            Lop l = new Lop();
            lvDSSV.Items.Clear();
            ListViewItem item;
            foreach (SinhVien sv in rs)
            {
                item = new ListViewItem(sv.MSSV.ToString());
                item.SubItems.Add(sv.HoTen);
                foreach (Lop lop in _dsLop)
                {
                    if (lop.MaLop==sv.MaLop)
                    {
                        
                        l.TenLop = lop.TenLop;
                        break; 
                    }
                }
                item.SubItems.Add(l.TenLop);
                lvDSSV.Items.Add(item);
            }
        }
    }
}
