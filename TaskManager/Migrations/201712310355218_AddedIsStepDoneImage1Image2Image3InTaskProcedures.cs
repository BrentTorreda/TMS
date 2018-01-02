namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsStepDoneImage1Image2Image3InTaskProcedures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskProcedures", "IsStepDone", c => c.Boolean(nullable: false));
            AddColumn("dbo.TaskProcedures", "Image1", c => c.String());
            AddColumn("dbo.TaskProcedures", "Image2", c => c.String());
            AddColumn("dbo.TaskProcedures", "Image3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskProcedures", "Image3");
            DropColumn("dbo.TaskProcedures", "Image2");
            DropColumn("dbo.TaskProcedures", "Image1");
            DropColumn("dbo.TaskProcedures", "IsStepDone");
        }
    }
}
