namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateDueAndPriority : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Priority", c => c.String());
            AddColumn("dbo.Tasks", "DateDue", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "DateDue");
            DropColumn("dbo.Tasks", "Priority");
        }
    }
}
