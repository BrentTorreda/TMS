namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubTasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberGroups",
                c => new
                    {
                        MemberGroupId = c.Int(nullable: false, identity: true),
                        MemberGroupName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MemberGroupId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        MemberName = c.String(nullable: false),
                        LoginName = c.String(nullable: false),
                        SecurityLevelId = c.Int(nullable: false),
                        MemberGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.MemberGroups", t => t.MemberGroupId, cascadeDelete: true)
                .Index(t => t.MemberGroupId);
            
            CreateTable(
                "dbo.SubTasks",
                c => new
                    {
                        SubTaskId = c.Int(nullable: false, identity: true),
                        SubTaskName = c.String(nullable: false),
                        SubTaskDescription = c.String(nullable: false),
                        TaskStatusId = c.String(nullable: false),
                        TaskId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubTaskId)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false),
                        TaskDescription = c.String(),
                        Hours = c.Int(nullable: false),
                        DateStarted = c.DateTime(nullable: false),
                        TaskStatusId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubTasks", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.SubTasks", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Members", "MemberGroupId", "dbo.MemberGroups");
            DropIndex("dbo.SubTasks", new[] { "MemberId" });
            DropIndex("dbo.SubTasks", new[] { "TaskId" });
            DropIndex("dbo.Members", new[] { "MemberGroupId" });
            DropTable("dbo.Tasks");
            DropTable("dbo.SubTasks");
            DropTable("dbo.Members");
            DropTable("dbo.MemberGroups");
        }
    }
}
