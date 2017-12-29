namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedOrdertoSubTaskOrderInSubtaskLevel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubTasksLevel1", "SubTaskOrder", c => c.Int(nullable: false));
            DropColumn("dbo.SubTasksLevel1", "Order");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubTasksLevel1", "Order", c => c.Int(nullable: false));
            DropColumn("dbo.SubTasksLevel1", "SubTaskOrder");
        }
    }
}
