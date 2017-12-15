namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPricesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        Amount = c.Single(nullable: false),
                        PriceDescription = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.PriceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Prices");
        }
    }
}
