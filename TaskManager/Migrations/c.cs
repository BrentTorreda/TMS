namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubtaskIdInSubTasksDeferralDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubTasksDeferralDetails", "SubTaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.SubTasksDeferralDetails", "SubTaskId");
            AddForeignKey("dbo.SubTasksDeferralDetails", "SubTaskId", "dbo.SubTasksLevel1", "SubTaskId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubTasksDeferralDetails", "SubTaskId", "dbo.SubTasksLevel1");
            DropIndex("dbo.SubTasksDeferralDetails", new[] { "SubTaskId" });
            DropColumn("dbo.SubTasksDeferralDetails", "SubTaskId");
        }
    }
}
