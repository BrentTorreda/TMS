namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNotesIsCompletedToTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Notes", c => c.String(maxLength: 1500));
            AddColumn("dbo.Tasks", "IsCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "IsCompleted");
            DropColumn("dbo.Tasks", "Notes");
        }
    }
}
