namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedMembersTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SubTasks", newName: "SubTasksLevel1");
            CreateTable(
                "dbo.MemberPositions",
                c => new
                    {
                        MemberPositionId = c.Int(nullable: false, identity: true),
                        PositionName = c.String(),
                    })
                .PrimaryKey(t => t.MemberPositionId);
            
            AddColumn("dbo.Members", "LastName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Members", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Members", "MemberPositionId", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "Email", c => c.String(maxLength: 255));
            AddColumn("dbo.Members", "Address", c => c.String(maxLength: 255));
            AddColumn("dbo.Members", "Phone", c => c.String(maxLength: 255));
            CreateIndex("dbo.Members", "MemberPositionId");
            AddForeignKey("dbo.Members", "MemberPositionId", "dbo.MemberPositions", "MemberPositionId", cascadeDelete: true);
            DropColumn("dbo.Members", "MemberName");
            DropColumn("dbo.Members", "SecurityLevelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "SecurityLevelId", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "MemberName", c => c.String(nullable: false));
            DropForeignKey("dbo.Members", "MemberPositionId", "dbo.MemberPositions");
            DropIndex("dbo.Members", new[] { "MemberPositionId" });
            DropColumn("dbo.Members", "Phone");
            DropColumn("dbo.Members", "Address");
            DropColumn("dbo.Members", "Email");
            DropColumn("dbo.Members", "MemberPositionId");
            DropColumn("dbo.Members", "FirstName");
            DropColumn("dbo.Members", "LastName");
            DropTable("dbo.MemberPositions");
            RenameTable(name: "dbo.SubTasksLevel1", newName: "SubTasks");
        }
    }
}
