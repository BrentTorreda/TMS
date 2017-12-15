namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedLoginNameFromMembers : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Members", "LoginName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "LoginName", c => c.String(nullable: false));
        }
    }
}
