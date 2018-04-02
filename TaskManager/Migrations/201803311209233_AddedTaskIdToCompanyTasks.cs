namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTaskIdToCompanyTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyTasks", "TaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.CompanyTasks", "TaskId");
            AddForeignKey("dbo.CompanyTasks", "TaskId", "dbo.Tasks", "TaskId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompanyTasks", "TaskId", "dbo.Tasks");
            DropIndex("dbo.CompanyTasks", new[] { "TaskId" });
            DropColumn("dbo.CompanyTasks", "TaskId");
        }
    }
}
