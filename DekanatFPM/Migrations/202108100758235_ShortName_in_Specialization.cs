namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShortName_in_Specialization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specializations", "ShortName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Specializations", "ShortName");
        }
    }
}
