namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateCreatedHoursPriceIdInSubtaskLevel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubTasksLevel1", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.SubTasksLevel1", "Hours", c => c.Int(nullable: false));
            AddColumn("dbo.SubTasksLevel1", "PriceId", c => c.Int(nullable: false));
            CreateIndex("dbo.SubTasksLevel1", "PriceId");
            AddForeignKey("dbo.SubTasksLevel1", "PriceId", "dbo.Prices", "PriceId", cascadeDelete: false);
            DropColumn("dbo.SubTasksLevel1", "TaskStatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubTasksLevel1", "TaskStatusId", c => c.String(nullable: false));
            DropForeignKey("dbo.SubTasksLevel1", "PriceId", "dbo.Prices");
            DropIndex("dbo.SubTasksLevel1", new[] { "PriceId" });
            DropColumn("dbo.SubTasksLevel1", "PriceId");
            DropColumn("dbo.SubTasksLevel1", "Hours");
            DropColumn("dbo.SubTasksLevel1", "DateCreated");
        }
    }
}
