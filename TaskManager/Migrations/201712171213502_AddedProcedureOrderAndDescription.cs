namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProcedureOrderAndDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskProcedures", "TaskProcedureOrder", c => c.Int(nullable: false));
            AddColumn("dbo.TaskProcedures", "TaskProcedureDescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskProcedures", "TaskProcedureDescription");
            DropColumn("dbo.TaskProcedures", "TaskProcedureOrder");
        }
    }
}
