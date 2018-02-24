namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUserRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@" 
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'cc15fa9e-d8dc-4407-94e5-dfa0fa3799e2', N'CanAddTasks');
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'be9bee80-6885-4f48-8191-4ffb9b43bb0d', N'CanChangeSettings');
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'b05d653d-af15-4a51-aed5-9b9eb0809699', N'CanReassignTasks');
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'b9c9344a-1cf7-4f29-9483-02cf8ff6fe20', N'CanSendEmails');
     
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a80fbf84-ea2a-4953-84db-2aeb5bff2b2e', N'b05d653d-af15-4a51-aed5-9b9eb0809699')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a80fbf84-ea2a-4953-84db-2aeb5bff2b2e', N'b9c9344a-1cf7-4f29-9483-02cf8ff6fe20')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a80fbf84-ea2a-4953-84db-2aeb5bff2b2e', N'cc15fa9e-d8dc-4407-94e5-dfa0fa3799e2')

");
        }
        
        public override void Down()
        {
        }
    }
}
