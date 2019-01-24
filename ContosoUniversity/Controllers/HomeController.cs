using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
		private SchoolContext db = new SchoolContext();

		public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
			IQueryable<EnrolmentDateGroup> data = from student in db.Students
												  group student by student.EnrolmentDate into dateGroup
												  select new EnrolmentDateGroup()
												  {
													  EnrolmentDate = dateGroup.Key,
													  StudentCount = dateGroup.Count()
												  };

            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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