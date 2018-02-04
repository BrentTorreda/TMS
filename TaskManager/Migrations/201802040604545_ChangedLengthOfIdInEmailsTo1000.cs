namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedLengthOfIdInEmailsTo1000 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Emails", "Id", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emails", "Id", c => c.String(maxLength: 100));
        }
    }
}
