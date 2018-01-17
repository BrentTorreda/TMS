namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMembersMadeByInNotesAddedCreatedByActionInTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "CreatedByAction", c => c.String());
            CreateIndex("dbo.Notes", "MadeBy");
            AddForeignKey("dbo.Notes", "MadeBy", "dbo.Members", "MemberId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "MadeBy", "dbo.Members");
            DropIndex("dbo.Notes", new[] { "MadeBy" });
            DropColumn("dbo.Tasks", "CreatedByAction");
        }
    }
}
