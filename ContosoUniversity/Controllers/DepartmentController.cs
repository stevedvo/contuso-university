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
			ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName");
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

			ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName", model.InstructorID);
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
				InstructorID = department.InstructorID,
				RowVersion = department.RowVersion
			};

			ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName", model.InstructorID);
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
						ModelState.AddModelError("", "Unable to save changes. The department was deleted by another User.");
						ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName", model.InstructorID);
						return View(model);
					}

					if (department.RowVersion.SequenceEqual(model.RowVersion))
					{
						department.Name = model.Name;
						department.Budget = model.Budget;
						department.StartDate = model.StartDate;
						department.InstructorID = model.InstructorID;

						db.Entry(department).State = EntityState.Modified;
						await db.SaveChangesAsync();
						return RedirectToAction("Index");
					}
					else
					{
						if (department.Name != model.Name)
						{
							ModelState.AddModelError("Name", "Current Value: " + department.Name);
						}

						if (department.Budget != model.Budget)
						{
							ModelState.AddModelError("Budget", "Current Value: " + String.Format("{0:c}", department.Budget));
						}

						if (department.StartDate != model.StartDate)
						{
							ModelState.AddModelError("StartDate", "Current Value: " + String.Format("{0:yyyy-MM-dd}", department.StartDate));
						}

						if (department.InstructorID != model.InstructorID)
						{
							ModelState.AddModelError("InstructorID", "Current Value: " + db.Instructors.Find(department.InstructorID).FullName);
						}

						ModelState.AddModelError("", "The record you attempted to edit was modified by another User after you got the original value. The edit operation was cancelled and the current values in the database have been displayed. If you still want to edit this record, click the Save button again. Otherwise click the 'Back To List' hyperlink.");

						model.RowVersion = department.RowVersion;
					}
				}
			}
			catch (RetryLimitExceededException /* dex */)
			{
				//Log the error (uncomment dex variable name and add a line here to write a log.
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
			}

			ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName", model.InstructorID);
			return View(model);
		}

		// GET: Department/Delete/5
		public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Department department = await db.Departments.FindAsync(id);

			if (department == null)
			{
				if (concurrencyError.GetValueOrDefault())
				{
					return RedirectToAction("Index");
				}

				return HttpNotFound();
			}

			if (concurrencyError.GetValueOrDefault())
			{
				ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete was modified by another user after you got the original values. The delete operation was cancelled and the current values in the database have been displayed. If you still want to delete this record, click the Delete button again. Otherwise click the 'Back to List' hyperlink.";
			}

			return View(department);
		}

		// POST: Department/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(Department department)
		{
			try
			{
				db.Entry(department).State = EntityState.Deleted;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			catch (DbUpdateConcurrencyException)
			{
				return RedirectToAction("Delete", new {concurrencyError = true, id = department.DepartmentID});
			}
			catch (DataException /* dex */)
			{
				//Log the error (uncomment dex variable name after DataException and add a line here to write a log.
				ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
				return View(department);
			}
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
