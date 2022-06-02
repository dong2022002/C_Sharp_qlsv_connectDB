using EntityDBCodeFirst.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityDBCodeFirst
{
    public partial class Form1 : Form
    {
        QLSinhVienDB db = new QLSinhVienDB();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadListView();
        }

        private void LoadListView()
        {
            lvDSSV.Items.Clear();
            var _listSV = db.sinhViens.Where(sv => sv.ID > 0).ToList();
            foreach (var sv in _listSV)
            {
                ListViewItem item = new ListViewItem(sv.ID.ToString());
                item.SubItems.Add(sv.Name);
                item.SubItems.Add(sv.lop.TenLop);
                lvDSSV.Items.Add(item);
            }
        }
    }
}
