namespace ContosoUniversity.Migrations
{
	using ContosoUniversity.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<ContosoUniversity.DAL.SchoolContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(ContosoUniversity.DAL.SchoolContext context)
		{
			var students = new List<Student>
			{
				new Student
				{
					FirstMidName = "Carson",
					LastName = "Alexander",
					EnrolmentDate = DateTime.Parse("2010-09-01")
				},
				new Student
				{
					FirstMidName = "Meredith",
					LastName = "Alonso",
					EnrolmentDate = DateTime.Parse("2012-09-01")
				},
				new Student
				{
					FirstMidName = "Arturo",
					LastName = "Anand",
					EnrolmentDate = DateTime.Parse("2013-09-01")
				},
				new Student
				{
					FirstMidName = "Gytis",
					LastName = "Barzdukas",
					EnrolmentDate = DateTime.Parse("2012-09-01")
				},
				new Student
				{
					FirstMidName = "Yan",
					LastName = "Li",
					EnrolmentDate = DateTime.Parse("2012-09-01")
				},
				new Student
				{
					FirstMidName = "Peggy",
					LastName = "Justice",
					EnrolmentDate = DateTime.Parse("2011-09-01")
				},
				new Student
				{
					FirstMidName = "Laura",
					LastName = "Norman",
					EnrolmentDate = DateTime.Parse("2013-09-01")
				},
				new Student
				{
					FirstMidName = "Nino",
					LastName = "Olivetto",
					EnrolmentDate = DateTime.Parse("2005-08-11")
				}
			};

			students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
			context.SaveChanges();

			var courses = new List<Course>
			{
				new Course
				{
					CourseID = 1050,
					Title = "Chemistry",
					Credits = 3
				},
				new Course
				{
					CourseID = 4022,
					Title = "Microeconomics",
					Credits = 3
				},
				new Course
				{
					CourseID = 4041,
					Title = "Macroeconomics",
					Credits = 3
				},
				new Course
				{
					CourseID = 1045,
					Title = "Calculus",
					Credits = 4
				},
				new Course
				{
					CourseID = 3141,
					Title = "Trigonometry",
					Credits = 4
				},
				new Course
				{
					CourseID = 2021,
					Title = "Composition",
					Credits = 3
				},
				new Course
				{
					CourseID = 2042,
					Title = "Literature",
					Credits = 4
				}
			};

			courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Title, s));
			context.SaveChanges();

			var Enrolments = new List<Enrolment>
			{
				new Enrolment
				{
					StudentID = students.Single(s => s.LastName == "Alexander").ID,
					CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
					Grade = Grade.A
				},
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Alexander").ID,
					CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
					Grade = Grade.C
				 },
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Alexander").ID,
					CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
					Grade = Grade.B
				 },
				 new Enrolment
				 {
					 StudentID = students.Single(s => s.LastName == "Alonso").ID,
					CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
					Grade = Grade.B
				 },
				 new Enrolment
				 {
					 StudentID = students.Single(s => s.LastName == "Alonso").ID,
					CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
					Grade = Grade.B
				 },
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Alonso").ID,
					CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
					Grade = Grade.B
				 },
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Anand").ID,
					CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
				 },
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Anand").ID,
					CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
					Grade = Grade.B
				 },
				new Enrolment
				{
					StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
					CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
					Grade = Grade.B
				 },
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Li").ID,
					CourseID = courses.Single(c => c.Title == "Composition").CourseID,
					Grade = Grade.B
				 },
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Justice").ID,
					CourseID = courses.Single(c => c.Title == "Literature").CourseID,
					Grade = Grade.B
				 }
			};

			foreach (Enrolment e in Enrolments)
			{
				var EnrolmentInDataBase = context.Enrolments.Where(s => s.Student.ID == e.StudentID && s.Course.CourseID == e.CourseID).SingleOrDefault();

				if (EnrolmentInDataBase == null)
				{
					context.Enrolments.Add(e);
				}
			}

			context.SaveChanges();
		}
	}
}