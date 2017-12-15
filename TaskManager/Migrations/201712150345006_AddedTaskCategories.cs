namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTaskCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskCategories",
                c => new
                    {
                        TaskCategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.TaskCategoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TaskCategories");
        }
    }
}
