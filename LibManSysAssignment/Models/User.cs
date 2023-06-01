using System.ComponentModel.DataAnnotations;

namespace LibManSysAssignment.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

		[Range(100000, 9999999999, ErrorMessage = "Phone number must be between {1} and {2}")]

		public int PhoneNo { get; set; }
        public List<Loan> LoanList { get; set; }
        public List<Club> ClubList { get; set; }
    }
}
