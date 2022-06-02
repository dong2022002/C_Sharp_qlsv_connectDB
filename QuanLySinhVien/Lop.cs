using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    public class Lop
    {
        public int  MaLop { get; set; }
        public string  TenLop { get; set; }
        public Lop()
        {

        }

        public Lop(int maLop, string tenLop)
        {
            MaLop = maLop;
            TenLop = tenLop;
        }


    }
}
