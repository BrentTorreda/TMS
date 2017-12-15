namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTaskProcedures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskProcedures",
                c => new
                    {
                        TaskProcedureId = c.Int(nullable: false, identity: true),
                        TaskSteps = c.String(nullable: false, maxLength: 1000),
                        TaskVideoFile = c.String(nullable: false, maxLength: 1000),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskProcedureId)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskProcedures", "TaskId", "dbo.Tasks");
            DropIndex("dbo.TaskProcedures", new[] { "TaskId" });
            DropTable("dbo.TaskProcedures");
        }
    }
}
