namespace FindTutorMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnnouncementModel_Changed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Announcements", "Title", c => c.String());
            AlterColumn("dbo.Announcements", "Category", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Announcements", "Category", c => c.String(nullable: false));
            AlterColumn("dbo.Announcements", "Title", c => c.String(nullable: false));
        }
    }
}
