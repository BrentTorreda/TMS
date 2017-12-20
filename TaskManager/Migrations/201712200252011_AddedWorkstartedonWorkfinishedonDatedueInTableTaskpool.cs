namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWorkstartedonWorkfinishedonDatedueInTableTaskpool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskPools", "WorkStartedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.TaskPools", "WorkFinishedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.TaskPools", "DateDue", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskPools", "DateDue");
            DropColumn("dbo.TaskPools", "WorkFinishedOn");
            DropColumn("dbo.TaskPools", "WorkStartedOn");
        }
    }
}
