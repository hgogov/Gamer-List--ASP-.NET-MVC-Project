namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_UserGameStatus_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserGameStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        GameId = c.Int(nullable: false),
                        GameStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.GameStatus", t => t.GameStatusId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId)
                .Index(t => t.GameStatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGameStatuses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserGameStatuses", "GameStatusId", "dbo.GameStatus");
            DropForeignKey("dbo.UserGameStatuses", "GameId", "dbo.Games");
            DropIndex("dbo.UserGameStatuses", new[] { "GameStatusId" });
            DropIndex("dbo.UserGameStatuses", new[] { "GameId" });
            DropIndex("dbo.UserGameStatuses", new[] { "UserId" });
            DropTable("dbo.UserGameStatuses");
        }
    }
}
