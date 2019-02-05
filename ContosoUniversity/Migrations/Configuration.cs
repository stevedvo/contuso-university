namespace ContosoUniversity.Migrations
{
	using ContosoUniversity.DAL;
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

			var instructors = new List<Instructor>
			{
				new Instructor
				{
					FirstMidName = "Kim",
					LastName = "Abercrombie",
					HireDate = DateTime.Parse("1995-03-11")
				},
				new Instructor
				{
					FirstMidName = "Fadi",
					LastName = "Fakhouri",
					HireDate = DateTime.Parse("2002-07-06")
				},
				new Instructor
				{
					FirstMidName = "Roger",
					LastName = "Harui",
					HireDate = DateTime.Parse("1998-07-01")
				},
				new Instructor
				{
					FirstMidName = "Candace",
					LastName = "Kapoor",
					HireDate = DateTime.Parse("2001-01-15")
				},
				new Instructor
				{
					FirstMidName = "Roger",
					LastName = "Zheng",
					HireDate = DateTime.Parse("2004-02-12")
				}
			};

			instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.LastName, s));
			context.SaveChanges();

			var departments = new List<Department>
			{
				new Department
				{
					Name = "English",
					Budget = 350000,
					StartDate = DateTime.Parse("2007-09-01"),
					InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
				},
				new Department
				{
					Name = "Mathematics",
					Budget = 100000,
					StartDate = DateTime.Parse("2007-09-01"),
					InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
				},
				new Department
				{
					Name = "Engineering",
					Budget = 350000,
					StartDate = DateTime.Parse("2007-09-01"),
					InstructorID = instructors.Single(i => i.LastName == "Harui").ID
				},
				new Department
				{
					Name = "Economics",
					Budget = 100000,
					StartDate = DateTime.Parse("2007-09-01"),
					InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID
				}
			};

			departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
			context.SaveChanges();

			var courses = new List<Course>
			{
				new Course
				{
					CourseID = 1050,
					Title = "Chemistry",
					Credits = 3,
					DepartmentID = departments.Single(s => s.Name == "Engineering").DepartmentID,
					Instructors = new List<Instructor>()
				},
				new Course
				{
					CourseID = 4022,
					Title = "Microeconomics",
					Credits = 3,
					DepartmentID = departments.Single(s => s.Name == "Economics").DepartmentID,
					Instructors = new List<Instructor>()
				},
				new Course
				{
					CourseID = 4041,
					Title = "Macroeconomics",
					Credits = 3,
					DepartmentID = departments.Single(s => s.Name == "Economics").DepartmentID,
					Instructors = new List<Instructor>()
				},
				new Course
				{
					CourseID = 1045,
					Title = "Calculus",
					Credits = 4,
					DepartmentID = departments.Single(s => s.Name == "Mathematics").DepartmentID,
					Instructors = new List<Instructor>()
				},
				new Course
				{
					CourseID = 3141,
					Title = "Trigonometry",
					Credits = 4,
					DepartmentID = departments.Single(s => s.Name == "Mathematics").DepartmentID,
					Instructors = new List<Instructor>()
				},
				new Course
				{
					CourseID = 2021,
					Title = "Composition",
					Credits = 3,
					DepartmentID = departments.Single(s => s.Name == "English").DepartmentID,
					Instructors = new List<Instructor>()
				},
				new Course
				{
					CourseID = 2042,
					Title = "Literature",
					Credits = 4,
					DepartmentID = departments.Single(s => s.Name == "English").DepartmentID,
					Instructors = new List<Instructor>()
				}
			};

			courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
			context.SaveChanges();

			var officeAssignments = new List<OfficeAssignment>
			{
				new OfficeAssignment
				{
					InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID,
					Location = "Smith 17"
				},
				new OfficeAssignment
				{
					InstructorID = instructors.Single(i => i.LastName == "Harui").ID,
					Location = "Gowan 27"
				},
				new OfficeAssignment
				{
					InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID,
					Location = "Thompson 304"
				}
			};

			officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
			context.SaveChanges();

			AddOrUpdateInstructor(context, "Chemistry", "Kapoor");
			AddOrUpdateInstructor(context, "Chemistry", "Harui");
			AddOrUpdateInstructor(context, "Microeconomics", "Zheng");
			AddOrUpdateInstructor(context, "Macroeconomics", "Zheng");

			AddOrUpdateInstructor(context, "Calculus", "Fakhouri");
			AddOrUpdateInstructor(context, "Trigonometry", "Harui");
			AddOrUpdateInstructor(context, "Composition", "Abercrombie");
			AddOrUpdateInstructor(context, "Literature", "Abercrombie");

			context.SaveChanges();

			var Enrolments = new List<Enrolment>
			{
				new Enrolment
				{
					StudentID = students.Single(s => s.LastName == "Alexander").ID,
					CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
					Grade = Grade.A
				},
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Alexander").ID,
					CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
					Grade = Grade.C
				 },
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Alexander").ID,
					CourseID = courses.Single(c => c.Title == "Macroeconomics").CourseID,
					Grade = Grade.B
				 },
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Alonso").ID,
					CourseID = courses.Single(c => c.Title == "Calculus").CourseID,
					Grade = Grade.B
				 },
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Alonso").ID,
					CourseID = courses.Single(c => c.Title == "Trigonometry").CourseID,
					Grade = Grade.B
				 },
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Alonso").ID,
					CourseID = courses.Single(c => c.Title == "Composition").CourseID,
					Grade = Grade.B
				 },
				 new Enrolment
				 {
					StudentID = students.Single(s => s.LastName == "Anand").ID,
					CourseID = courses.Single(c => c.Title == "Chemistry").CourseID
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

		void AddOrUpdateInstructor(SchoolContext context, string courseTitle, string instructorName)
		{
			var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
			var inst = crs.Instructors.SingleOrDefault(i => i.LastName == instructorName);

			if (inst == null)
			{
				crs.Instructors.Add(context.Instructors.Single(i => i.LastName == instructorName));
			}
		}
	}
}