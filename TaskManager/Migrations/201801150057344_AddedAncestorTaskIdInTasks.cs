namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAncestorTaskIdInTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "AncestorTaskId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "AncestorTaskId");
        }
    }
}
