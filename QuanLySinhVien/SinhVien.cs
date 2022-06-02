using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    public class SinhVien
    {

        public string MSSV { get; set; }
        public string HoTen { get; set; }
        public int MaLop { get; set; }
        
        public SinhVien()
        {

        }
        public SinhVien(string mSSV, string hoTen,int maLop)
        {
            MSSV = mSSV;
            HoTen = hoTen;
            MaLop = maLop;
        }
    }
}
