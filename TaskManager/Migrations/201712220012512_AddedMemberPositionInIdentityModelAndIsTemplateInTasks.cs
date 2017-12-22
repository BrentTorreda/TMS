namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMemberPositionInIdentityModelAndIsTemplateInTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "IsTemplate", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "MemberPositionId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "MemberPositionId");
            AddForeignKey("dbo.AspNetUsers", "MemberPositionId", "dbo.MemberPositions", "MemberPositionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "MemberPositionId", "dbo.MemberPositions");
            DropIndex("dbo.AspNetUsers", new[] { "MemberPositionId" });
            DropColumn("dbo.AspNetUsers", "MemberPositionId");
            DropColumn("dbo.Tasks", "IsTemplate");
        }
    }
}
