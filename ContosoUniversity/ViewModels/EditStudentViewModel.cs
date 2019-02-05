using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
	public class EditStudentViewModel
	{
		public int ID { get; set; }
		[StringLength(50, MinimumLength = 1)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Required]
		[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
		[Column("FirstName")]
		[Display(Name = "First Name")]
		public string FirstMidName { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Enrolment Date")]
		public DateTime EnrolmentDate { get; set; }

		[Display(Name = "Full Name")]
		public string FullName
		{
			get
			{
				return LastName + ", " + FirstMidName;
			}
		}
	}
}