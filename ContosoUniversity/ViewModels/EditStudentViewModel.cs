using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
	public class EditStudentViewModel
	{
		public int ID { get; set; }
		[StringLength(50)]
		public string LastName { get; set; }
		[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
		public string FirstMidName { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime EnrolmentDate { get; set; }
	}
}