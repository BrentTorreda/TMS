namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCompanyTasksTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyTasks",
                c => new
                    {
                        CompTaskId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        TaskTypeId = c.Int(nullable: false),
                        TaskCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompTaskId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.TaskCategories", t => t.TaskCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.TaskTypes", t => t.TaskTypeId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.TaskTypeId)
                .Index(t => t.TaskCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompanyTasks", "TaskTypeId", "dbo.TaskTypes");
            DropForeignKey("dbo.CompanyTasks", "TaskCategoryId", "dbo.TaskCategories");
            DropForeignKey("dbo.CompanyTasks", "CompanyId", "dbo.Companies");
            DropIndex("dbo.CompanyTasks", new[] { "TaskCategoryId" });
            DropIndex("dbo.CompanyTasks", new[] { "TaskTypeId" });
            DropIndex("dbo.CompanyTasks", new[] { "CompanyId" });
            DropTable("dbo.CompanyTasks");
        }
    }
}
