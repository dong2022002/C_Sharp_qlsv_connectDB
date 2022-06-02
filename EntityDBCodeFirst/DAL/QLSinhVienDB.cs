using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDBCodeFirst.model;

namespace EntityDBCodeFirst.DAL
{
    public class QLSinhVienDB : DbContext
    {
        public QLSinhVienDB() : base("DBSinhVien")
        {

        }
        public static QLSinhVienDB Create()
        {
            return new QLSinhVienDB();
        }
        public DbSet<SinhVien> sinhViens { get; set; }
        public DbSet<Lop> lops { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Lop>()
                .HasMany(sv => sv.IlistSV)
                .WithRequired(l => l.lop)
                .HasForeignKey(l=>l.MaLop)
                .WillCascadeOnDelete(false);
        }
    }
}
