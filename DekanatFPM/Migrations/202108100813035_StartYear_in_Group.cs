namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartYear_in_Group : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "StartYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "StartYear");
        }
    }
}
