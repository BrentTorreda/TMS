namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempRemovedRequiredFromIdentityModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 255));
            AlterColumn("dbo.AspNetUsers", "FistName", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "FistName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
