namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CoverImage_To_Game : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "CoverImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "CoverImagePath");
        }
    }
}
