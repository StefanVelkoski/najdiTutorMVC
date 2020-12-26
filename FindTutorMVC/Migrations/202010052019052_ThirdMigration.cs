namespace FindTutorMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Announcement_Id", "dbo.Announcements");
            DropIndex("dbo.Reviews", new[] { "Announcement_Id" });
            AlterColumn("dbo.Reviews", "Announcement_Id", c => c.Int());
            CreateIndex("dbo.Reviews", "Announcement_Id");
            AddForeignKey("dbo.Reviews", "Announcement_Id", "dbo.Announcements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Announcement_Id", "dbo.Announcements");
            DropIndex("dbo.Reviews", new[] { "Announcement_Id" });
            AlterColumn("dbo.Reviews", "Announcement_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "Announcement_Id");
            AddForeignKey("dbo.Reviews", "Announcement_Id", "dbo.Announcements", "Id", cascadeDelete: true);
        }
    }
}
