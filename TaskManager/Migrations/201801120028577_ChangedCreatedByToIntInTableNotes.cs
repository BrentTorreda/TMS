namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedCreatedByToIntInTableNotes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "CreatedBy", c => c.Int(nullable: false));
            CreateIndex("dbo.Notes", "CreatedBy");
            AddForeignKey("dbo.Notes", "CreatedBy", "dbo.Members", "MemberId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "CreatedBy", "dbo.Members");
            DropIndex("dbo.Notes", new[] { "CreatedBy" });
            AlterColumn("dbo.Notes", "CreatedBy", c => c.String());
        }
    }
}
