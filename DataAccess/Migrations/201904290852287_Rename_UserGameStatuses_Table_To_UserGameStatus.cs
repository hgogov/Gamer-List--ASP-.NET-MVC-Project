namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_UserGameStatuses_Table_To_UserGameStatus : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserGameStatuses", newName: "UserGameStatus");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserGameStatus", newName: "UserGameStatuses");
        }
    }
}
