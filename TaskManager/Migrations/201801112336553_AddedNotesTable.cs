namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNotesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CreatedBy = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        AssignedTo = c.String(nullable: false),
                        Subject = c.String(maxLength: 500),
                        Body = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notes");
        }
    }
}
