namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicine", "Created_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Medicine", "Expiration_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Patient", "Arrival_Date", c => c.DateTime());
            AddColumn("dbo.Patient", "Discharge_Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patient", "Discharge_Date");
            DropColumn("dbo.Patient", "Arrival_Date");
            DropColumn("dbo.Medicine", "Expiration_Date");
            DropColumn("dbo.Medicine", "Created_Date");
        }
    }
}
