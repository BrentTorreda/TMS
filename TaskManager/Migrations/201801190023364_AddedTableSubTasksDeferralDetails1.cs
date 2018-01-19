namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTableSubTasksDeferralDetails1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubTasksDeferralDetails",
                c => new
                    {
                        DeferId = c.Int(nullable: false, identity: true),
                        DeferredOn = c.DateTime(nullable: false),
                        DeferredTo = c.DateTime(nullable: false),
                        DeferredFor = c.Time(nullable: false, precision: 7),
                        MemberId = c.Int(nullable: false),
                        DeferredReason = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.DeferId)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubTasksDeferralDetails", "MemberId", "dbo.Members");
            DropIndex("dbo.SubTasksDeferralDetails", new[] { "MemberId" });
            DropTable("dbo.SubTasksDeferralDetails");
        }
    }
}
