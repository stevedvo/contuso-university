using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers
{
	public class InstructorController : Controller
	{
		private SchoolContext db = new SchoolContext();

		// GET: Instructor
		public ActionResult Index(int? id, int? courseID)
		{
			InstructorIndexData viewModel = new InstructorIndexData();
			viewModel.Instructors = db.Instructors
				.Include(i => i.OfficeAssignment)
				.Include(i => i.Courses.Select(c => c.Department))
				.OrderBy(i => i.LastName);

			if (id != null)
			{
				ViewBag.InstructorID = id.Value;
				viewModel.Courses = viewModel.Instructors.SingleOrDefault(i => i.ID == id.Value) == null ? null : viewModel.Instructors.SingleOrDefault(i => i.ID == id.Value).Courses;
			}

			if (viewModel.Courses != null)
			{
				if (courseID != null)
				{
					ViewBag.CourseID = courseID.Value;
					// lazy loading
					viewModel.Enrolments = viewModel.Courses.SingleOrDefault(x => x.CourseID == courseID) == null ? null : viewModel.Courses.SingleOrDefault(x => x.CourseID == courseID).Enrolments;

					// explicit loading
					//var selectedCourse = viewModel.Courses.SingleOrDefault(x => x.CourseID == courseID);

					//if (selectedCourse != null)
					//{
					//	db.Entry(selectedCourse).Collection(x => x.Enrolments).Load();

					//	foreach (Enrolment enrolment in selectedCourse.Enrolments)
					//	{
					//		db.Entry(enrolment).Reference(x => x.Student).Load();
					//	}

					//	viewModel.Enrolments = selectedCourse.Enrolments;
					//}
					//else
					//{
					//	viewModel.Enrolments = null;
					//}
				}
			}

			return View(viewModel);
		}

		// GET: Instructor/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Instructor instructor = db.Instructors.Find(id);

			if (instructor == null)
			{
				return HttpNotFound();
			}
			return View(instructor);
		}

		// GET: Instructor/Create
		public ActionResult Create()
		{
			ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location");
			return View();
		}

		// POST: Instructor/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,HireDate")] Instructor instructor)
		{
			if (ModelState.IsValid)
			{
				db.Instructors.Add(instructor);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.ID);
			return View(instructor);
		}

		// GET: Instructor/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Instructor instructor = db.Instructors
				.Include(i => i.OfficeAssignment)
				.Include(i => i.Courses)
				.Single(i => i.ID == id);

			if (instructor == null)
			{
				return HttpNotFound();
			}

			EditInstructorViewModel model = new EditInstructorViewModel
			{
				ID = instructor.ID,
				LastName = instructor.LastName,
				FirstMidName = instructor.FirstMidName,
				HireDate = instructor.HireDate,
				Courses = instructor.Courses,
				OfficeAssignment = instructor.OfficeAssignment
			};

			PopulateAssignedCourseData(model);

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EditInstructorViewModel model, string[] selectedCourses)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Instructor instructor = db.Instructors
						.Include(i => i.OfficeAssignment)
						.Include(i => i.Courses)
						.Single(i => i.ID == model.ID);

					instructor.LastName = model.LastName;
					instructor.FirstMidName = model.FirstMidName;
					instructor.HireDate = model.HireDate;
					instructor.OfficeAssignment = string.IsNullOrWhiteSpace(model.OfficeAssignment.Location) ? null : model.OfficeAssignment;

					UpdateInstructorCourses(selectedCourses, instructor);

					db.Entry(instructor).State = EntityState.Modified;
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			catch (RetryLimitExceededException /* dex */)
			{
				//Log the error (uncomment dex variable name and add a line here to write a log.
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
			}

			PopulateAssignedCourseData(model);

			return View(model);
		}

		// GET: Instructor/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Instructor instructor = db.Instructors.Find(id);
			if (instructor == null)
			{
				return HttpNotFound();
			}
			return View(instructor);
		}

		// POST: Instructor/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Instructor instructor = db.Instructors
				.Include(i => i.OfficeAssignment)
				.Single(i => i.ID == id);

			db.Instructors.Remove(instructor);

			var department = db.Departments
				.Where(d => d.InstructorID == id)
				.SingleOrDefault();

			if (department != null)
			{
				department.InstructorID = null;
			}

			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private void PopulateAssignedCourseData(EditInstructorViewModel model)
		{
			var allCourses = db.Courses;
			var instructorCourses = new HashSet<int>(model.Courses.Select(c => c.CourseID));
			var viewModel = new List<AssignedCourseData>();

			foreach (var course in allCourses)
			{
				viewModel.Add(new AssignedCourseData
				{
					CourseID = course.CourseID,
					Title = course.Title,
					Assigned = instructorCourses.Contains(course.CourseID)
				});
			}

			ViewBag.Courses = viewModel;
		}

		private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
		{
			if (selectedCourses == null)
			{
				instructorToUpdate.Courses = new List<Course>();
				return;
			}

			var selectedCoursesHS = new HashSet<string>(selectedCourses);
			var instructorCourses = new HashSet<int>(instructorToUpdate.Courses.Select(c => c.CourseID));

			foreach (var course in db.Courses)
			{
				if (selectedCoursesHS.Contains(course.CourseID.ToString()))
				{
					if (!instructorCourses.Contains(course.CourseID))
					{
						instructorToUpdate.Courses.Add(course);
					}
				}
				else
				{
					if (instructorCourses.Contains(course.CourseID))
					{
						instructorToUpdate.Courses.Remove(course);
					}
				}
			}
		}
	}
}
