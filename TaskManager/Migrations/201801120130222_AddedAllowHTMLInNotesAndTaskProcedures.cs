namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAllowHTMLInNotesAndTaskProcedures : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "Body", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "Body", c => c.String(maxLength: 1000));
        }
    }
}
