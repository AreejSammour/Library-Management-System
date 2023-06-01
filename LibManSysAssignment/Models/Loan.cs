namespace LibManSysAssignment.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime ReturnDate => LoanDate.AddMonths(1);
        public List<BookCopy> Copy { get; set; }
        public string? Status { get; set; }

    }
}
