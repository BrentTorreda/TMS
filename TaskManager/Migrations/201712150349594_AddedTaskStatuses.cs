namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTaskStatuses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskStatuses",
                c => new
                    {
                        TaskStatusId = c.Int(nullable: false, identity: true),
                        TaskStatusName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.TaskStatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TaskStatuses");
        }
    }
}
