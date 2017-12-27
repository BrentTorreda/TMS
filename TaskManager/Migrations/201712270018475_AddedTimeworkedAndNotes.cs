namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTimeworkedAndNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubTasksLevel1", "TimeWorked", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.SubTasksLevel1", "Notes", c => c.String(maxLength: 1500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubTasksLevel1", "Notes");
            DropColumn("dbo.SubTasksLevel1", "TimeWorked");
        }
    }
}
