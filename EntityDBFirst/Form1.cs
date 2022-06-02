using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityDBFirst
{
    public partial class Form1 : Form
    {
        QLSinhVienEntities db = new QLSinhVienEntities();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadListView();
            LoadDSLop();
        }

        private void LoadDSLop()
        {
            var _listLop = db.Lops.Where(sv => sv.ID > 0).ToList();
            foreach (var item in _listLop)
            {
                cboLop.Items.Add(item.TenLop);
            }
        }

        private void LoadListView()
        {
            lvDSSV.Items.Clear();
            var _listSV = db.SinhViens.Where(sv => sv.ID > 0).ToList();
            foreach (var sv in _listSV)
            {
                ListViewItem item = new ListViewItem(sv.ID.ToString());
                item.SubItems.Add(sv.HoTen);
                item.SubItems.Add(sv.Lop.TenLop);
                lvDSSV.Items.Add(item);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtHoTenSV.Clear();
            cboLop.Text = cboLop.Items[0].ToString();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadListView();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SinhVien sv;
            if (string.IsNullOrEmpty(txtHoTenSV.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
          
        }
       
    }
}
