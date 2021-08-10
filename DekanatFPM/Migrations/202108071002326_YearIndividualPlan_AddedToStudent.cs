namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YearIndividualPlan_AddedToStudent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "YearIndividualPlan_YearIndividualPlanID", "dbo.YearIndividualPlans");
            RenameColumn(table: "dbo.Subjects", name: "YearIndividualPlan_YearIndividualPlanID", newName: "YearIndividualPlan_StudentID");
            RenameIndex(table: "dbo.Subjects", name: "IX_YearIndividualPlan_YearIndividualPlanID", newName: "IX_YearIndividualPlan_StudentID");
            DropPrimaryKey("dbo.YearIndividualPlans");
            AddColumn("dbo.Students", "YearIndividualPlanID", c => c.Int(nullable: false));
            AddColumn("dbo.YearIndividualPlans", "StudentID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.YearIndividualPlans", "StudentID");
            CreateIndex("dbo.YearIndividualPlans", "StudentID");
            AddForeignKey("dbo.YearIndividualPlans", "StudentID", "dbo.Students", "StudentID");
            AddForeignKey("dbo.Subjects", "YearIndividualPlan_StudentID", "dbo.YearIndividualPlans", "StudentID");
            DropColumn("dbo.YearIndividualPlans", "YearIndividualPlanID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.YearIndividualPlans", "YearIndividualPlanID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Subjects", "YearIndividualPlan_StudentID", "dbo.YearIndividualPlans");
            DropForeignKey("dbo.YearIndividualPlans", "StudentID", "dbo.Students");
            DropIndex("dbo.YearIndividualPlans", new[] { "StudentID" });
            DropPrimaryKey("dbo.YearIndividualPlans");
            DropColumn("dbo.YearIndividualPlans", "StudentID");
            DropColumn("dbo.Students", "YearIndividualPlanID");
            AddPrimaryKey("dbo.YearIndividualPlans", "YearIndividualPlanID");
            RenameIndex(table: "dbo.Subjects", name: "IX_YearIndividualPlan_StudentID", newName: "IX_YearIndividualPlan_YearIndividualPlanID");
            RenameColumn(table: "dbo.Subjects", name: "YearIndividualPlan_StudentID", newName: "YearIndividualPlan_YearIndividualPlanID");
            AddForeignKey("dbo.Subjects", "YearIndividualPlan_YearIndividualPlanID", "dbo.YearIndividualPlans", "YearIndividualPlanID");
        }
    }
}
