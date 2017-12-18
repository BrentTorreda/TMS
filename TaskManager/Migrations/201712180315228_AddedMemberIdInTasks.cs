namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMemberIdInTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "MemberId", c => c.Int());
            CreateIndex("dbo.Tasks", "MemberId");
            AddForeignKey("dbo.Tasks", "MemberId", "dbo.Members", "MemberId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "MemberId", "dbo.Members");
            DropIndex("dbo.Tasks", new[] { "MemberId" });
            DropColumn("dbo.Tasks", "MemberId");
        }
    }
}
