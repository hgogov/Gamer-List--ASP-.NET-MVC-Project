namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_UserGameRating_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserGameRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        GameId = c.Int(nullable: false),
                        GameRatingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.GameRatings", t => t.GameRatingId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId)
                .Index(t => t.GameRatingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGameRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserGameRatings", "GameRatingId", "dbo.GameRatings");
            DropForeignKey("dbo.UserGameRatings", "GameId", "dbo.Games");
            DropIndex("dbo.UserGameRatings", new[] { "GameRatingId" });
            DropIndex("dbo.UserGameRatings", new[] { "GameId" });
            DropIndex("dbo.UserGameRatings", new[] { "UserId" });
            DropTable("dbo.UserGameRatings");
        }
    }
}
