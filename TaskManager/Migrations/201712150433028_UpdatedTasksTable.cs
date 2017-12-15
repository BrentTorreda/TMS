namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTasksTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tasks", "DateWorkStarted", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tasks", "TimeWorked", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Tasks", "TaskCategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Tasks", "TaskTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Tasks", "CompanyId", c => c.Int(nullable: false));
            AddColumn("dbo.Tasks", "PriceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tasks", "TaskName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Tasks", "TaskDescription", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tasks", "TaskStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tasks", "TaskCategoryId");
            CreateIndex("dbo.Tasks", "TaskTypeId");
            CreateIndex("dbo.Tasks", "TaskStatusId");
            CreateIndex("dbo.Tasks", "CompanyId");
            CreateIndex("dbo.Tasks", "PriceId");
            AddForeignKey("dbo.Tasks", "CompanyId", "dbo.Companies", "CompanyId", cascadeDelete: true);
            AddForeignKey("dbo.Tasks", "PriceId", "dbo.Prices", "PriceId", cascadeDelete: true);
            AddForeignKey("dbo.Tasks", "TaskCategoryId", "dbo.TaskCategories", "TaskCategoryId", cascadeDelete: true);
            AddForeignKey("dbo.Tasks", "TaskStatusId", "dbo.TaskStatuses", "TaskStatusId", cascadeDelete: true);
            AddForeignKey("dbo.Tasks", "TaskTypeId", "dbo.TaskTypes", "TaskTypeId", cascadeDelete: true);
            DropColumn("dbo.Tasks", "DateStarted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "DateStarted", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Tasks", "TaskTypeId", "dbo.TaskTypes");
            DropForeignKey("dbo.Tasks", "TaskStatusId", "dbo.TaskStatuses");
            DropForeignKey("dbo.Tasks", "TaskCategoryId", "dbo.TaskCategories");
            DropForeignKey("dbo.Tasks", "PriceId", "dbo.Prices");
            DropForeignKey("dbo.Tasks", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Tasks", new[] { "PriceId" });
            DropIndex("dbo.Tasks", new[] { "CompanyId" });
            DropIndex("dbo.Tasks", new[] { "TaskStatusId" });
            DropIndex("dbo.Tasks", new[] { "TaskTypeId" });
            DropIndex("dbo.Tasks", new[] { "TaskCategoryId" });
            AlterColumn("dbo.Tasks", "TaskStatusId", c => c.String(nullable: false));
            AlterColumn("dbo.Tasks", "TaskDescription", c => c.String());
            AlterColumn("dbo.Tasks", "TaskName", c => c.String(nullable: false));
            DropColumn("dbo.Tasks", "PriceId");
            DropColumn("dbo.Tasks", "CompanyId");
            DropColumn("dbo.Tasks", "TaskTypeId");
            DropColumn("dbo.Tasks", "TaskCategoryId");
            DropColumn("dbo.Tasks", "TimeWorked");
            DropColumn("dbo.Tasks", "DateWorkStarted");
            DropColumn("dbo.Tasks", "DateCreated");
        }
    }
}
