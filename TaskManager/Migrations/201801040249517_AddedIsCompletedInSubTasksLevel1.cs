namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsCompletedInSubTasksLevel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubTasksLevel1", "IsCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubTasksLevel1", "IsCompleted");
        }
    }
}
