namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Name_for_Group : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "Name");
            DropColumn("dbo.Groups", "Number");
        }
    }
}
