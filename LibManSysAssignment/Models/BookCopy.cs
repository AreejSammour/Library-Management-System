namespace LibManSysAssignment.Models
{
    public class BookCopy
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; }
    }
}
