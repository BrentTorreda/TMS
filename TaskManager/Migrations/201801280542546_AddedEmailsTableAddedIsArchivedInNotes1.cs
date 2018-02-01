namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailsTableAddedIsArchivedInNotes1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        MailId = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 100),
                        Sender = c.String(maxLength: 1500),
                        DateReceived = c.DateTime(nullable: false),
                        Subject = c.String(),
                        NumberOfAttachments = c.Int(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        IsMadeTask = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MailId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Emails");
        }
    }
}
