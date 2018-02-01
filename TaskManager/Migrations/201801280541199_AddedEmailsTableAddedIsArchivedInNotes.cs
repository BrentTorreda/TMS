namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailsTableAddedIsArchivedInNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "IsArchived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "IsArchived");
        }
    }
}
