namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCompaniesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 255),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Companies");
        }
    }
}
