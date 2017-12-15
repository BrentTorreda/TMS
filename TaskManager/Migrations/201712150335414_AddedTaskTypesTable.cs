namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTaskTypesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskTypes",
                c => new
                    {
                        TaskTypeId = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false, maxLength: 255),
                        AssingmentSecurityLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskTypeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TaskTypes");
        }
    }
}
