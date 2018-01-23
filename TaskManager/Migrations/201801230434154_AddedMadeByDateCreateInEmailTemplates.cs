namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMadeByDateCreateInEmailTemplates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailTemplates", "MadeBy", c => c.String());
            AddColumn("dbo.EmailTemplates", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailTemplates", "DateCreated");
            DropColumn("dbo.EmailTemplates", "MadeBy");
        }
    }
}
