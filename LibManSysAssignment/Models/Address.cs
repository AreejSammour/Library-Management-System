using System.ComponentModel.DataAnnotations;

namespace LibManSysAssignment.Models
{
    public class Address
	{
		public int Id { get; set; }
		public string Street { get; set; }
		[Range(10000, 99999, ErrorMessage = "PostCode must be a 5-digit number")]
		public int PostCode { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
