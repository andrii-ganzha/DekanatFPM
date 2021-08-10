namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YearsIndividualPlans_in_Group_byString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "YearsIndividualPlans", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "YearsIndividualPlans");
        }
    }
}
