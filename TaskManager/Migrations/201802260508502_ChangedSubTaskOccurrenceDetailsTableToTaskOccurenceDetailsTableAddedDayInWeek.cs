namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedSubTaskOccurrenceDetailsTableToTaskOccurenceDetailsTableAddedDayInWeek : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubTasksOccurrenceDetails", "SubTaskId", "dbo.SubTasksLevel1");
            DropIndex("dbo.SubTasksOccurrenceDetails", new[] { "SubTaskId" });
            CreateTable(
                "dbo.TasksOccurrenceDetails",
                c => new
                    {
                        SubTaskOccurrenceId = c.Int(nullable: false, identity: true),
                        TaskId = c.Int(nullable: false),
                        pattern = c.String(),
                        recurEvery = c.Int(nullable: false),
                        dayInWeek = c.Int(nullable: false),
                        dayInMonth = c.Int(nullable: false),
                        firstBiMonthlyDay = c.Int(nullable: false),
                        secondBiMonthlyDay = c.Int(nullable: false),
                        dayInYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubTaskOccurrenceId)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId);
            
            DropTable("dbo.SubTasksOccurrenceDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SubTasksOccurrenceDetails",
                c => new
                    {
                        SubTaskOccurrenceId = c.Int(nullable: false, identity: true),
                        SubTaskId = c.Int(nullable: false),
                        pattern = c.String(),
                        recurEvery = c.Int(nullable: false),
                        dayInMonth = c.Int(nullable: false),
                        firstBiMonthlyDay = c.Int(nullable: false),
                        secondBiMonthlyDay = c.Int(nullable: false),
                        dayInYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubTaskOccurrenceId);
            
            DropForeignKey("dbo.TasksOccurrenceDetails", "TaskId", "dbo.Tasks");
            DropIndex("dbo.TasksOccurrenceDetails", new[] { "TaskId" });
            DropTable("dbo.TasksOccurrenceDetails");
            CreateIndex("dbo.SubTasksOccurrenceDetails", "SubTaskId");
            AddForeignKey("dbo.SubTasksOccurrenceDetails", "SubTaskId", "dbo.SubTasksLevel1", "SubTaskId", cascadeDelete: true);
        }
    }
}
