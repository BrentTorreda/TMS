namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempRemovedMemberPositionInIdentityModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "MemberPositionId", "dbo.MemberPositions");
            DropIndex("dbo.AspNetUsers", new[] { "MemberPositionId" });
            DropColumn("dbo.AspNetUsers", "MemberPositionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "MemberPositionId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "MemberPositionId");
            AddForeignKey("dbo.AspNetUsers", "MemberPositionId", "dbo.MemberPositions", "MemberPositionId", cascadeDelete: true);
        }
    }
}
