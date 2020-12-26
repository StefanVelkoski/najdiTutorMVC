namespace FindTutorMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Announcements", "Difficulty", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Announcements", "Difficulty", c => c.String(nullable: false));
        }
    }
}
