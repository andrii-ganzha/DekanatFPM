namespace DekanatFPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Statementadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statements",
                c => new
                    {
                        StatementID = c.Int(nullable: false, identity: true),
                        SubjectID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        Repass = c.Boolean(nullable: false),
                        Grade = c.Int(nullable: false),
                        Semester = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatementID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: false)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: false)
                .Index(t => t.SubjectID)
                .Index(t => t.StudentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Statements", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Statements", "StudentID", "dbo.Students");
            DropIndex("dbo.Statements", new[] { "StudentID" });
            DropIndex("dbo.Statements", new[] { "SubjectID" });
            DropTable("dbo.Statements");
        }
    }
}
