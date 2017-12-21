namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubtaskIdInTaskprocedures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskProcedures", "SubTaskId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskProcedures", "SubTaskId");
        }
    }
}
