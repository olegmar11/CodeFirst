namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class og : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipment",
                c => new
                    {
                        IDeq = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        Manufacturer = c.String(),
                    })
                .PrimaryKey(t => t.IDeq);
            
            CreateTable(
                "dbo.Medicine",
                c => new
                    {
                        IDmed = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Producer = c.String(),
                    })
                .PrimaryKey(t => t.IDmed);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        IDpat = c.Int(nullable: false, identity: true),
                        First_Name = c.String(maxLength: 20),
                        Last_Name = c.String(maxLength: 20),
                        Middle_Name = c.String(maxLength: 20),
                        Age = c.Int(nullable: false),
                        Disease = c.String(),
                        Ward = c.String(),
                    })
                .PrimaryKey(t => t.IDpat);
            
            CreateTable(
                "dbo.Proced",
                c => new
                    {
                        IDproc = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Equipment_ID = c.Int(),
                        Medicine_ID = c.Int(),
                        Patient_ID = c.Int(),
                        Staff_ID = c.Int(),
                    })
                .PrimaryKey(t => t.IDproc)
                .ForeignKey("dbo.Equipment", t => t.Equipment_ID)
                .ForeignKey("dbo.Medicine", t => t.Medicine_ID)
                .ForeignKey("dbo.Patient", t => t.Patient_ID)
                .ForeignKey("dbo.Staff", t => t.Staff_ID)
                .Index(t => t.Equipment_ID)
                .Index(t => t.Medicine_ID)
                .Index(t => t.Patient_ID)
                .Index(t => t.Staff_ID);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        IDstaff = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Last_Name = c.String(),
                        Middle_Name = c.String(),
                        Age = c.Int(nullable: false),
                        Position = c.String(),
                    })
                .PrimaryKey(t => t.IDstaff);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Proced", "Staff_ID", "dbo.Staff");
            DropForeignKey("dbo.Proced", "Patient_ID", "dbo.Patient");
            DropForeignKey("dbo.Proced", "Medicine_ID", "dbo.Medicine");
            DropForeignKey("dbo.Proced", "Equipment_ID", "dbo.Equipment");
            DropIndex("dbo.Proced", new[] { "Staff_ID" });
            DropIndex("dbo.Proced", new[] { "Patient_ID" });
            DropIndex("dbo.Proced", new[] { "Medicine_ID" });
            DropIndex("dbo.Proced", new[] { "Equipment_ID" });
            DropTable("dbo.Staff");
            DropTable("dbo.Proced");
            DropTable("dbo.Patient");
            DropTable("dbo.Medicine");
            DropTable("dbo.Equipment");
        }
    }
}
