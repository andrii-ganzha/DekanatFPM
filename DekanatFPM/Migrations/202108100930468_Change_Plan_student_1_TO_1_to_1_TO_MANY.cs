namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Plan_student_1_TO_1_to_1_TO_MANY : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.YearIndividualPlans", "StudentID", "dbo.Students");
            DropPrimaryKey("dbo.YearIndividualPlans");
            AddColumn("dbo.YearIndividualPlans", "YearIndividualPlanID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.YearIndividualPlans", "YearIndividualPlanID");
            AddForeignKey("dbo.YearIndividualPlans", "StudentID", "dbo.Students", "StudentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YearIndividualPlans", "StudentID", "dbo.Students");
            DropPrimaryKey("dbo.YearIndividualPlans");
            DropColumn("dbo.YearIndividualPlans", "YearIndividualPlanID");
            AddPrimaryKey("dbo.YearIndividualPlans", "StudentID");
            AddForeignKey("dbo.YearIndividualPlans", "StudentID", "dbo.Students", "StudentID");
        }
    }
}
