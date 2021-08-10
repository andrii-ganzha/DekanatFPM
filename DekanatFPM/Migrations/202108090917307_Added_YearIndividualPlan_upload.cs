namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_YearIndividualPlan_upload : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "YearIndividualPlan_StudentID", "dbo.YearIndividualPlans");
            DropIndex("dbo.Subjects", new[] { "YearIndividualPlan_StudentID" });
            DropColumn("dbo.Students", "YearIndividualPlanID");
            DropColumn("dbo.Subjects", "YearIndividualPlan_StudentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "YearIndividualPlan_StudentID", c => c.Int());
            AddColumn("dbo.Students", "YearIndividualPlanID", c => c.Int(nullable: false));
            CreateIndex("dbo.Subjects", "YearIndividualPlan_StudentID");
            AddForeignKey("dbo.Subjects", "YearIndividualPlan_StudentID", "dbo.YearIndividualPlans", "StudentID");
        }
    }
}
