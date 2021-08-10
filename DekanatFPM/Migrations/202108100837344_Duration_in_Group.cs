namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Duration_in_Group : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "Duration");
        }
    }
}
