namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTaskPoolTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskPools",
                c => new
                    {
                        TaskPoolId = c.Int(nullable: false, identity: true),
                        SubTaskLevel1Id = c.Int(nullable: false),
                        TaskStatusId = c.Int(nullable: false),
                        TimeWorked = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.TaskPoolId)
                .ForeignKey("dbo.SubTasksLevel1", t => t.SubTaskLevel1Id, cascadeDelete: false)
                .ForeignKey("dbo.TaskStatuses", t => t.TaskStatusId, cascadeDelete: true)
                .Index(t => t.SubTaskLevel1Id)
                .Index(t => t.TaskStatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskPools", "TaskStatusId", "dbo.TaskStatuses");
            DropForeignKey("dbo.TaskPools", "SubTaskLevel1Id", "dbo.SubTasksLevel1");
            DropIndex("dbo.TaskPools", new[] { "TaskStatusId" });
            DropIndex("dbo.TaskPools", new[] { "SubTaskLevel1Id" });
            DropTable("dbo.TaskPools");
        }
    }
}
