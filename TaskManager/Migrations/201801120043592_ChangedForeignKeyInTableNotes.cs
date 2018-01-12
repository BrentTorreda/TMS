namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedForeignKeyInTableNotes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Notes", new[] { "CreatedBy" });
            DropColumn("dbo.Notes", "AssignedTo");
            RenameColumn(table: "dbo.Notes", name: "CreatedBy", newName: "AssignedTo");
            AlterColumn("dbo.Notes", "AssignedTo", c => c.Int(nullable: false));
            CreateIndex("dbo.Notes", "AssignedTo");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Notes", new[] { "AssignedTo" });
            AlterColumn("dbo.Notes", "AssignedTo", c => c.String(nullable: false));
            RenameColumn(table: "dbo.Notes", name: "AssignedTo", newName: "CreatedBy");
            AddColumn("dbo.Notes", "AssignedTo", c => c.String(nullable: false));
            CreateIndex("dbo.Notes", "CreatedBy");
        }
    }
}
