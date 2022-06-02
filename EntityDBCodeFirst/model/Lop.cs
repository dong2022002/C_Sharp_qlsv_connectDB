using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDBCodeFirst.model
{
    public class Lop
    {
        public int ID { get; set; }
        public string TenLop { get; set; }
        public virtual IList<SinhVien> IlistSV { get; set; }
    }
}
