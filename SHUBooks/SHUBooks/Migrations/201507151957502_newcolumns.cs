namespace SHUBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newcolumns : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "Book_ISBN", "dbo.Books");
            DropIndex("dbo.Ratings", new[] { "Book_ISBN" });
            RenameColumn(table: "dbo.Books", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Ratings", name: "Book_ISBN", newName: "BookId");
            RenameIndex(table: "dbo.Books", name: "IX_User_Id", newName: "IX_UserId");
            DropPrimaryKey("dbo.Books");
            AddColumn("dbo.Books", "BookId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Books", "ISBN", c => c.String());
            AlterColumn("dbo.Ratings", "BookId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Books", "BookId");
            CreateIndex("dbo.Ratings", "BookId");
            AddForeignKey("dbo.Ratings", "BookId", "dbo.Books", "BookId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "BookId", "dbo.Books");
            DropIndex("dbo.Ratings", new[] { "BookId" });
            DropPrimaryKey("dbo.Books");
            AlterColumn("dbo.Ratings", "BookId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Books", "ISBN", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Books", "BookId");
            AddPrimaryKey("dbo.Books", "ISBN");
            RenameIndex(table: "dbo.Books", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Ratings", name: "BookId", newName: "Book_ISBN");
            RenameColumn(table: "dbo.Books", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.Ratings", "Book_ISBN");
            AddForeignKey("dbo.Ratings", "Book_ISBN", "dbo.Books", "ISBN");
        }
    }
}
