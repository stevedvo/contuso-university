using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.DAL
{
    public class SchoolInitialiser : System.Data.Entity.DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student{FirstMidName="Carson",LastName="Alexander",EnrolmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrolmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Arturo",LastName="Anand",EnrolmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrolmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Yan",LastName="Li",EnrolmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Peggy",LastName="Justice",EnrolmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstMidName="Laura",LastName="Norman",EnrolmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Nino",LastName="Olivetto",EnrolmentDate=DateTime.Parse("2005-09-01")}
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
            var courses = new List<Course>
            {
                new Course{CourseID=1050,Title="Chemistry",Credits=3},
                new Course{CourseID=4022,Title="Microeconomics",Credits=3},
                new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
                new Course{CourseID=1045,Title="Calculus",Credits=4},
                new Course{CourseID=3141,Title="Trigonometry",Credits=4},
                new Course{CourseID=2021,Title="Composition",Credits=3},
                new Course{CourseID=2042,Title="Literature",Credits=4}
            };

            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            var Enrolments = new List<Enrolment>
            {
                new Enrolment{StudentID=1,CourseID=1050,Grade=Grade.A},
                new Enrolment{StudentID=1,CourseID=4022,Grade=Grade.C},
                new Enrolment{StudentID=1,CourseID=4041,Grade=Grade.B},
                new Enrolment{StudentID=2,CourseID=1045,Grade=Grade.B},
                new Enrolment{StudentID=2,CourseID=3141,Grade=Grade.F},
                new Enrolment{StudentID=2,CourseID=2021,Grade=Grade.F},
                new Enrolment{StudentID=3,CourseID=1050},
                new Enrolment{StudentID=4,CourseID=1050},
                new Enrolment{StudentID=4,CourseID=4022,Grade=Grade.F},
                new Enrolment{StudentID=5,CourseID=4041,Grade=Grade.C},
                new Enrolment{StudentID=6,CourseID=1045},
                new Enrolment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };

            Enrolments.ForEach(s => context.Enrolments.Add(s));
            context.SaveChanges();
        }
    }
}