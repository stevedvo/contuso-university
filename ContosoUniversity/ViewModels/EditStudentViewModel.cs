using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
	public class EditStudentViewModel
	{
		public int ID { get; set; }
		public string LastName { get; set; }
		public string FirstMidName { get; set; }
		public DateTime EnrolmentDate { get; set; }
	}
}