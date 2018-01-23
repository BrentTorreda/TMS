namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsReadIsMadeToTaskInNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Notes", "IsMadeToTask", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "IsMadeToTask");
            DropColumn("dbo.Notes", "IsRead");
        }
    }
}
