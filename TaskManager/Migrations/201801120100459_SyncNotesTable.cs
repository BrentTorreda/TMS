namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncNotesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "MadeBy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "MadeBy");
        }
    }
}
