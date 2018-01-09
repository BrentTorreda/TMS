namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFilePathsToTaskProcedures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskProcedures", "FilePath1", c => c.String());
            AddColumn("dbo.TaskProcedures", "FilePath2", c => c.String());
            AddColumn("dbo.TaskProcedures", "FilePath3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskProcedures", "FilePath3");
            DropColumn("dbo.TaskProcedures", "FilePath2");
            DropColumn("dbo.TaskProcedures", "FilePath1");
        }
    }
}
