namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YearIndividualPlan_Added : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "YearIndividualPlan_YearIndividualPlanID", "dbo.YearIndividualPlans");
            DropForeignKey("dbo.Subjects", "GroupID", "dbo.Groups");
            DropIndex("dbo.Subjects", new[] { "YearIndividualPlan_YearIndividualPlanID" });
            DropIndex("dbo.Subjects", new[] { "GroupID" });
            DropTable("dbo.Subjects");
            DropTable("dbo.YearIndividualPlans");
        }
    }
}
