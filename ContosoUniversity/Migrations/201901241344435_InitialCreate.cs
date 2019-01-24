namespace ContosoUniversity.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class InitialCreate : DbMigration
	{
		public override void Up()
		{
			CreateTable
			(
				"dbo.Course",
				c => new
				{
					CourseID = c.Int(nullable: false),
					Title = c.String(),
					Credits = c.Int(nullable: false)
				}
			)
			.PrimaryKey(t => t.CourseID);

			CreateTable
			(
				"dbo.Enrolment",
				c => new
				{
					EnrolmentID = c.Int(nullable: false, identity: true),
					CourseID = c.Int(nullable: false),
					StudentID = c.Int(nullable: false),
					Grade = c.Int()
				}
			)
			.PrimaryKey(t => t.EnrolmentID)
			.ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
			.ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
			.Index(t => t.CourseID)
			.Index(t => t.StudentID);

			CreateTable
			(
				"dbo.Student",
				c => new
				{
					ID = c.Int(nullable: false, identity: true),
					LastName = c.String(),
					FirstMidName = c.String(),
					EnrolmentDate = c.DateTime(nullable: false),
				}
			)
			.PrimaryKey(t => t.ID);
		}

		public override void Down()
		{
			DropForeignKey("dbo.Enrolment", "StudentID", "dbo.Student");
			DropForeignKey("dbo.Enrolment", "CourseID", "dbo.Course");
			DropIndex("dbo.Enrolment", new[] {"StudentID"});
			DropIndex("dbo.Enrolment", new[] {"CourseID"});
			DropTable("dbo.Student");
			DropTable("dbo.Enrolment");
			DropTable("dbo.Course");
		}
	}
}
