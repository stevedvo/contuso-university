using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using System.Data.Entity.Infrastructure;

namespace ContosoUniversity.Controllers
{
	public class DepartmentController : Controller
	{
		private SchoolContext db = new SchoolContext();

		// GET: Department
		public async Task<ActionResult> Index()
		{
			var departments = db.Departments.Include(d => d.Administrator);
			return View(await departments.ToListAsync());
		}

		// GET: Department/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Department department = await db.Departments.FindAsync(id);

			if (department == null)
			{
				return HttpNotFound();
			}

			return View(department);
		}

		// GET: Department/Create
		public ActionResult Create()
		{
			ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CreateDepartmentViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Department department = new Department
					{
						Name = model.Name,
						Budget = model.Budget,
						StartDate = model.StartDate,
						InstructorID = model.InstructorID
					};

					db.Departments.Add(department);
					await db.SaveChangesAsync();
					return RedirectToAction("Index");
				}
			}
			catch (RetryLimitExceededException /* dex */)
			{
				//Log the error (uncomment dex variable name and add a line here to write a log.
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
			}

			ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", model.InstructorID);
			return View(model);
		}

		// GET: Department/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Department department = await db.Departments.FindAsync(id);

			if (department == null)
			{
				return HttpNotFound();
			}

			EditDepartmentViewModel model = new EditDepartmentViewModel
			{
				DepartmentID = department.DepartmentID,
				Name = department.Name,
				Budget = department.Budget,
				StartDate = department.StartDate,
				InstructorID = department.InstructorID
			};

			ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", model.InstructorID);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(EditDepartmentViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Department department = await db.Departments.FindAsync(model.DepartmentID);

					if (department == null)
					{
						return HttpNotFound();
					}

					department.Name = model.Name;
					department.Budget = model.Budget;
					department.StartDate = model.StartDate;
					department.InstructorID = model.InstructorID;

					db.Entry(department).State = EntityState.Modified;
					await db.SaveChangesAsync();
					return RedirectToAction("Index");
				}
			}
			catch (RetryLimitExceededException /* dex */)
			{
				//Log the error (uncomment dex variable name and add a line here to write a log.
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
			}

			ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", model.InstructorID);
			return View(model);
		}

		// GET: Department/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Department department = await db.Departments.FindAsync(id);

			if (department == null)
			{
				return HttpNotFound();
			}

			return View(department);
		}

		// POST: Department/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Department department = await db.Departments.FindAsync(id);
			db.Departments.Remove(department);
			await db.SaveChangesAsync();
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
