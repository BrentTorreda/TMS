namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubTaskOccurrenceDetailsTableAddedStartedOnInSubTasksLevel1 : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.SubTaskOccurrenceId)
                .ForeignKey("dbo.SubTasksLevel1", t => t.SubTaskId, cascadeDelete: true)
                .Index(t => t.SubTaskId);
            
            AddColumn("dbo.SubTasksLevel1", "StartedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubTasksOccurrenceDetails", "SubTaskId", "dbo.SubTasksLevel1");
            DropIndex("dbo.SubTasksOccurrenceDetails", new[] { "SubTaskId" });
            DropColumn("dbo.SubTasksLevel1", "StartedOn");
            DropTable("dbo.SubTasksOccurrenceDetails");
        }
    }
}
