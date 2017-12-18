namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDateTimesToNullableInTasksTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.Tasks", "DateWorkStarted", c => c.DateTime());
            AlterColumn("dbo.Tasks", "TimeWorked", c => c.Time(precision: 7));
            AlterColumn("dbo.Tasks", "DateDue", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "DateDue", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tasks", "TimeWorked", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Tasks", "DateWorkStarted", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tasks", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
