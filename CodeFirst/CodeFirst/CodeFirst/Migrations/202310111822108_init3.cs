namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patient", "LuckLevel", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patient", "LuckLevel", c => c.Int(nullable: false));
        }
    }
}
