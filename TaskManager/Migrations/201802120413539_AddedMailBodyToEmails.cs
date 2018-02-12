namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMailBodyToEmails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emails", "MailBody", c => c.String());
            AlterColumn("dbo.Emails", "Subject", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emails", "Subject", c => c.String());
            DropColumn("dbo.Emails", "MailBody");
        }
    }
}
