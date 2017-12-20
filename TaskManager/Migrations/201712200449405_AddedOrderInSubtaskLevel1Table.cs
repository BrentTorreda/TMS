namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderInSubtaskLevel1Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubTasksLevel1", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubTasksLevel1", "Order");
        }
    }
}
