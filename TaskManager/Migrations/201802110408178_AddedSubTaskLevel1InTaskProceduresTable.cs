namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubTaskLevel1InTaskProceduresTable : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TaskProcedures", "SubtaskId");
            AddForeignKey("dbo.TaskProcedures", "SubtaskId", "dbo.SubTasksLevel1", "SubTaskId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskProcedures", "SubtaskId", "dbo.SubTasksLevel1");
            DropIndex("dbo.TaskProcedures", new[] { "SubtaskId" });
        }
    }
}
