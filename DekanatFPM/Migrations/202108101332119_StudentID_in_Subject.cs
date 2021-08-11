namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentID_in_Subject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "StudentID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "StudentID");
        }
    }
}
