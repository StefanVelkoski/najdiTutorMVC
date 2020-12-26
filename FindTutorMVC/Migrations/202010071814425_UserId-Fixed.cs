namespace FindTutorMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdFixed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FavouriteAnnouncementToUsers", "UserId", c => c.String(nullable: false));
            AlterColumn("dbo.ReviewToUsers", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReviewToUsers", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.FavouriteAnnouncementToUsers", "UserId", c => c.Int(nullable: false));
        }
    }
}
