namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedApplicationUserIdToMembers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Members", "ApplicationUserId");
            AddForeignKey("dbo.Members", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FistName");
            DropColumn("dbo.AspNetUsers", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "FistName", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 255));
            DropForeignKey("dbo.Members", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Members", new[] { "ApplicationUserId" });
            DropColumn("dbo.Members", "ApplicationUserId");
        }
    }
}
