namespace SHUBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rate : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Ratings");
            DropColumn("dbo.Ratings", "RatingId");
            DropColumn("dbo.Ratings", "RaterCount");
            DropColumn("dbo.Ratings", "CummulativeRate");
            DropColumn("dbo.Ratings", "Ratings");
            AddColumn("dbo.Ratings", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Ratings", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "BookISBN", c => c.String());
            AddColumn("dbo.Ratings", "UserId", c => c.String());
            AddPrimaryKey("dbo.Ratings", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "Ratings", c => c.Double(nullable: false));
            AddColumn("dbo.Ratings", "CummulativeRate", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "RaterCount", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "RatingId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Ratings", "RatingId");
            DropPrimaryKey("dbo.Ratings");
            DropColumn("dbo.Ratings", "UserId");
            DropColumn("dbo.Ratings", "BookISBN");
            DropColumn("dbo.Ratings", "Score");
            DropColumn("dbo.Ratings", "Id");
           
        }
    }
}
