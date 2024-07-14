namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CityEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PosEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Telephone = c.String(),
                        CellPhone = c.String(),
                        Address = c.String(),
                        City_Id = c.Int(nullable: false),
                        Model = c.String(),
                        Brand = c.String(),
                        ConnType_Id = c.Int(nullable: false),
                        MorningOperning = c.String(),
                        MorningClosing = c.String(),
                        AfternoonOpening = c.String(),
                        AfternonClosing = c.String(),
                        DaysClosed = c.String(),
                        InsertDate = c.Long(nullable: false),
                        DeleteAt = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CityEntities", t => t.City_Id)
                .ForeignKey("dbo.ConnectionTypeEntities", t => t.ConnType_Id)
                .Index(t => t.City_Id)
                .Index(t => t.ConnType_Id);
            
            CreateTable(
                "dbo.ConnectionTypeEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConnectionType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IssueEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPos = c.Int(nullable: false),
                        IdType = c.Int(nullable: false),
                        IdSubType = c.Int(nullable: false),
                        IdProblem = c.Int(nullable: false),
                        Priority = c.String(),
                        IdStatus = c.Int(nullable: false),
                        Memo = c.String(),
                        IdUserCreated = c.Int(nullable: false),
                        IdUserType = c.Int(nullable: false),
                        Description = c.String(),
                        AssignedDate = c.Long(nullable: false),
                        CreationDate = c.Long(nullable: false),
                        ModifDate = c.Long(nullable: false),
                        Solotion = c.String(),
                        DeleteAt = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IssuesTypeEntities", t => t.IdType)
                .ForeignKey("dbo.PosEntities", t => t.IdPos)
                .ForeignKey("dbo.StatusEntities", t => t.IdStatus)
                .ForeignKey("dbo.UserEntities", t => t.IdUserCreated)
                .ForeignKey("dbo.UserTypeEntities", t => t.IdUserType)
                .Index(t => t.IdPos)
                .Index(t => t.IdType)
                .Index(t => t.IdStatus)
                .Index(t => t.IdUserCreated)
                .Index(t => t.IdUserType);
            
            CreateTable(
                "dbo.IssuesTypeEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueLevel = c.Int(nullable: false),
                        ParentIssues = c.String(),
                        Name = c.String(),
                        InsertDate = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LogEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdIssue = c.Int(nullable: false),
                        IdUser = c.Int(nullable: false),
                        Action = c.String(),
                        Notes = c.String(),
                        InsertDate = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IssueEntities", t => t.IdIssue)
                .ForeignKey("dbo.UserEntities", t => t.IdUser)
                .Index(t => t.IdIssue)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.UserEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Login = c.String(),
                        Telephone = c.String(),
                        UserTypeId = c.Int(nullable: false),
                        DeleteAt = c.Long(),
                        PasswordHash = c.Binary(),
                        Salt = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypeEntities", t => t.UserTypeId)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.UserTypeEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StatusEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IssueEntities", "IdUserType", "dbo.UserTypeEntities");
            DropForeignKey("dbo.IssueEntities", "IdUserCreated", "dbo.UserEntities");
            DropForeignKey("dbo.IssueEntities", "IdStatus", "dbo.StatusEntities");
            DropForeignKey("dbo.IssueEntities", "IdPos", "dbo.PosEntities");
            DropForeignKey("dbo.LogEntities", "IdUser", "dbo.UserEntities");
            DropForeignKey("dbo.UserEntities", "UserTypeId", "dbo.UserTypeEntities");
            DropForeignKey("dbo.LogEntities", "IdIssue", "dbo.IssueEntities");
            DropForeignKey("dbo.IssueEntities", "IdType", "dbo.IssuesTypeEntities");
            DropForeignKey("dbo.PosEntities", "ConnType_Id", "dbo.ConnectionTypeEntities");
            DropForeignKey("dbo.PosEntities", "City_Id", "dbo.CityEntities");
            DropIndex("dbo.UserEntities", new[] { "UserTypeId" });
            DropIndex("dbo.LogEntities", new[] { "IdUser" });
            DropIndex("dbo.LogEntities", new[] { "IdIssue" });
            DropIndex("dbo.IssueEntities", new[] { "IdUserType" });
            DropIndex("dbo.IssueEntities", new[] { "IdUserCreated" });
            DropIndex("dbo.IssueEntities", new[] { "IdStatus" });
            DropIndex("dbo.IssueEntities", new[] { "IdType" });
            DropIndex("dbo.IssueEntities", new[] { "IdPos" });
            DropIndex("dbo.PosEntities", new[] { "ConnType_Id" });
            DropIndex("dbo.PosEntities", new[] { "City_Id" });
            DropTable("dbo.StatusEntities");
            DropTable("dbo.UserTypeEntities");
            DropTable("dbo.UserEntities");
            DropTable("dbo.LogEntities");
            DropTable("dbo.IssuesTypeEntities");
            DropTable("dbo.IssueEntities");
            DropTable("dbo.ConnectionTypeEntities");
            DropTable("dbo.PosEntities");
            DropTable("dbo.CityEntities");
        }
    }
}
