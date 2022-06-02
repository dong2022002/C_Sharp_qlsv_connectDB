using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDBCodeFirst.model
{
    public class SinhVien
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MaLop { get; set; }
        public virtual Lop lop { get; set; }
    }
}
