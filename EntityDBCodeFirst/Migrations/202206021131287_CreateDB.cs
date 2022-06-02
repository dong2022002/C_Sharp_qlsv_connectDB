namespace EntityDBCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lops",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenLop = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SinhViens",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MaLop = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lops", t => t.MaLop)
                .Index(t => t.MaLop);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SinhViens", "MaLop", "dbo.Lops");
            DropIndex("dbo.SinhViens", new[] { "MaLop" });
            DropTable("dbo.SinhViens");
            DropTable("dbo.Lops");
        }
    }
}
