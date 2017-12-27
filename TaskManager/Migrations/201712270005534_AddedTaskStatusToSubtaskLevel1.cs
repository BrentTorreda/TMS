namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTaskStatusToSubtaskLevel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubTasksLevel1", "TaskStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.SubTasksLevel1", "TaskStatusId");
            AddForeignKey("dbo.SubTasksLevel1", "TaskStatusId", "dbo.TaskStatuses", "TaskStatusId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubTasksLevel1", "TaskStatusId", "dbo.TaskStatuses");
            DropIndex("dbo.SubTasksLevel1", new[] { "TaskStatusId" });
            DropColumn("dbo.SubTasksLevel1", "TaskStatusId");
        }
    }
}
