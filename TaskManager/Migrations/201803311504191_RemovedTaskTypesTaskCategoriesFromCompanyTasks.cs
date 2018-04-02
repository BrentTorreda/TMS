namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTaskTypesTaskCategoriesFromCompanyTasks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CompanyTasks", "TaskCategoryId", "dbo.TaskCategories");
            DropForeignKey("dbo.CompanyTasks", "TaskTypeId", "dbo.TaskTypes");
            DropIndex("dbo.CompanyTasks", new[] { "TaskTypeId" });
            DropIndex("dbo.CompanyTasks", new[] { "TaskCategoryId" });
            DropColumn("dbo.CompanyTasks", "TaskTypeId");
            DropColumn("dbo.CompanyTasks", "TaskCategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CompanyTasks", "TaskCategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.CompanyTasks", "TaskTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.CompanyTasks", "TaskCategoryId");
            CreateIndex("dbo.CompanyTasks", "TaskTypeId");
            AddForeignKey("dbo.CompanyTasks", "TaskTypeId", "dbo.TaskTypes", "TaskTypeId", cascadeDelete: true);
            AddForeignKey("dbo.CompanyTasks", "TaskCategoryId", "dbo.TaskCategories", "TaskCategoryId", cascadeDelete: true);
        }
    }
}
