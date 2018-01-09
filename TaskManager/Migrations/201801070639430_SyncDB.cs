namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncDB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskProcedures", "FilePath1", c => c.String(maxLength: 1000));
            AlterColumn("dbo.TaskProcedures", "FilePath2", c => c.String(maxLength: 1000));
            AlterColumn("dbo.TaskProcedures", "FilePath3", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskProcedures", "FilePath3", c => c.String());
            AlterColumn("dbo.TaskProcedures", "FilePath2", c => c.String());
            AlterColumn("dbo.TaskProcedures", "FilePath1", c => c.String());
        }
    }
}
