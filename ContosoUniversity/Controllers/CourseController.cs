using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers
{
	public class CourseController : Controller
	{
		private SchoolContext db = new SchoolContext();

		// GET: Course
		public ActionResult Index()
		{
			var courses = db.Courses.Include(c => c.Department);
			return View(courses.ToList());
		}

		// GET: Course/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Course course = db.Courses.Find(id);

			if (course == null)
			{
				return HttpNotFound();
			}

			return View(course);
		}

		// GET: Course/Create
		public ActionResult Create()
		{
			ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateCourseViewModel model)
		{
			if (ModelState.IsValid)
			{
				Course course = new Course
				{
					CourseID = model.CourseID,
					Title = model.Title,
					Credits = model.Credits,
					DepartmentID = model.DepartmentID
				};

				db.Courses.Add(course);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", model.DepartmentID);
			return View(model);
		}

		// GET: Course/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Course course = db.Courses.Find(id);

			if (course == null)
			{
				return HttpNotFound();
			}

			EditCourseViewModel model = new EditCourseViewModel
			{
				CourseID = course.CourseID,
				Title = course.Title,
				Credits = course.Credits,
				DepartmentID = course.DepartmentID
			};

			ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", model.DepartmentID);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EditCourseViewModel model)
		{
			if (ModelState.IsValid)
			{
				Course course = db.Courses.Find(model.CourseID);

				if (course == null)
				{
					return HttpNotFound();
				}

				course.Title = model.Title;
				course.Credits = model.Credits;
				course.DepartmentID = model.DepartmentID;

				db.Entry(course).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", model.DepartmentID);
			return View(model);
		}

		// GET: Course/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Course course = db.Courses.Find(id);
			if (course == null)
			{
				return HttpNotFound();
			}

			return View(course);
		}

		// POST: Course/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Course course = db.Courses.Find(id);
			db.Courses.Remove(course);
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
	}
}
