namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YearIndividualPlan_AddedToStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "YearIndividualPlanID", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "YearIndividualPlanID");
            AddForeignKey("dbo.Students", "YearIndividualPlanID", "dbo.YearIndividualPlans", "YearIndividualPlanID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "YearIndividualPlanID", "dbo.YearIndividualPlans");
            DropIndex("dbo.Students", new[] { "YearIndividualPlanID" });
            DropColumn("dbo.Students", "YearIndividualPlanID");
        }
    }
}
