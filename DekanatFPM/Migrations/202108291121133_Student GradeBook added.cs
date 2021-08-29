namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentGradeBookadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "RecordBook", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "RecordBook");
        }
    }
}
