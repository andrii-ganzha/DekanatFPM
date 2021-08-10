namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YearIndividualPlan_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        SpecializationID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.Specializations", t => t.SpecializationID, cascadeDelete: true)
                .Index(t => t.SpecializationID);
            
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        SpecializationID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SpecialtyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SpecializationID)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyID, cascadeDelete: true)
                .Index(t => t.SpecialtyID);
            
            CreateTable(
                "dbo.Specialties",
                c => new
                    {
                        SpecialtyID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TrainingDirectionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SpecialtyID)
                .ForeignKey("dbo.TrainingDirections", t => t.TrainingDirectionID, cascadeDelete: true)
                .Index(t => t.TrainingDirectionID);
            
            CreateTable(
                "dbo.TrainingDirections",
                c => new
                    {
                        TrainingDirectionID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TrainingDirectionID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        GroupID = c.Int(nullable: false),
                        YearIndividualPlanID = c.Int(nullable: false),
                        Name = c.String(),
                        Surname = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.YearIndividualPlans", t => t.YearIndividualPlanID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.YearIndividualPlanID);
            
            CreateTable(
                "dbo.YearIndividualPlans",
                c => new
                    {
                        YearIndividualPlanID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.YearIndividualPlanID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GroupID = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        ControlExam = c.Int(),
                        ControlCredit = c.Int(),
                        ControlCourseWork = c.Int(),
                        ControlIndividual = c.Int(),
                        YearIndividualPlan_YearIndividualPlanID = c.Int(),
                    })
                .PrimaryKey(t => t.SubjectID)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.YearIndividualPlans", t => t.YearIndividualPlan_YearIndividualPlanID)
                .Index(t => t.GroupID)
                .Index(t => t.YearIndividualPlan_YearIndividualPlanID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Students", "YearIndividualPlanID", "dbo.YearIndividualPlans");
            DropForeignKey("dbo.Subjects", "YearIndividualPlan_YearIndividualPlanID", "dbo.YearIndividualPlans");
            DropForeignKey("dbo.Subjects", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.Students", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.Specialties", "TrainingDirectionID", "dbo.TrainingDirections");
            DropForeignKey("dbo.Specializations", "SpecialtyID", "dbo.Specialties");
            DropForeignKey("dbo.Groups", "SpecializationID", "dbo.Specializations");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Subjects", new[] { "YearIndividualPlan_YearIndividualPlanID" });
            DropIndex("dbo.Subjects", new[] { "GroupID" });
            DropIndex("dbo.Students", new[] { "YearIndividualPlanID" });
            DropIndex("dbo.Students", new[] { "GroupID" });
            DropIndex("dbo.Specialties", new[] { "TrainingDirectionID" });
            DropIndex("dbo.Specializations", new[] { "SpecialtyID" });
            DropIndex("dbo.Groups", new[] { "SpecializationID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Subjects");
            DropTable("dbo.YearIndividualPlans");
            DropTable("dbo.Students");
            DropTable("dbo.TrainingDirections");
            DropTable("dbo.Specialties");
            DropTable("dbo.Specializations");
            DropTable("dbo.Groups");
        }
    }
}
