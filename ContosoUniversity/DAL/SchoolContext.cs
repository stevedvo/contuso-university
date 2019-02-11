﻿using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ContosoUniversity.DAL
{
	public class SchoolContext : DbContext
	{
		public DbSet<Course> Courses { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Enrolment> Enrolments { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			modelBuilder.Entity<Course>()
				.HasMany(c => c.Instructors).WithMany(i => i.Courses)
				.Map(t => t.MapLeftKey("CourseID")
					.MapRightKey("InstructorID")
					.ToTable("CourseInstructor"));

			modelBuilder.Entity<Department>().MapToStoredProcedures();
		}
	}
}