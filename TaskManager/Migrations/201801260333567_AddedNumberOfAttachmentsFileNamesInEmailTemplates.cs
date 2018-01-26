namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNumberOfAttachmentsFileNamesInEmailTemplates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailTemplates", "NumberOfAttachments", c => c.Int(nullable: false));
            AddColumn("dbo.EmailTemplates", "FileNames", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailTemplates", "FileNames");
            DropColumn("dbo.EmailTemplates", "NumberOfAttachments");
        }
    }
}
