namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedMemberIdInSubtaskLevel1ToNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubTasksLevel1", "MemberId", "dbo.Members");
            DropIndex("dbo.SubTasksLevel1", new[] { "MemberId" });
            AlterColumn("dbo.SubTasksLevel1", "MemberId", c => c.Int());
            CreateIndex("dbo.SubTasksLevel1", "MemberId");
            AddForeignKey("dbo.SubTasksLevel1", "MemberId", "dbo.Members", "MemberId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubTasksLevel1", "MemberId", "dbo.Members");
            DropIndex("dbo.SubTasksLevel1", new[] { "MemberId" });
            AlterColumn("dbo.SubTasksLevel1", "MemberId", c => c.Int(nullable: false));
            CreateIndex("dbo.SubTasksLevel1", "MemberId");
            AddForeignKey("dbo.SubTasksLevel1", "MemberId", "dbo.Members", "MemberId", cascadeDelete: true);
        }
    }
}
