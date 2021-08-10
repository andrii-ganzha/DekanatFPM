namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlanWithGroup_in_Student : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "PlanWithGroup", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "PlanWithGroup");
        }
    }
}
