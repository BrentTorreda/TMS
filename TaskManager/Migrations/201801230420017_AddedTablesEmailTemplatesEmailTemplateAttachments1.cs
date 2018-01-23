namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTablesEmailTemplatesEmailTemplateAttachments1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailTemplateAttachments",
                c => new
                    {
                        AttachmentId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 1000),
                        EmailTemplateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AttachmentId)
                .ForeignKey("dbo.EmailTemplates", t => t.EmailTemplateId, cascadeDelete: true)
                .Index(t => t.EmailTemplateId);
            
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        MailTemplateId = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 1000),
                        Body = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.MailTemplateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailTemplateAttachments", "EmailTemplateId", "dbo.EmailTemplates");
            DropIndex("dbo.EmailTemplateAttachments", new[] { "EmailTemplateId" });
            DropTable("dbo.EmailTemplates");
            DropTable("dbo.EmailTemplateAttachments");
        }
    }
}
