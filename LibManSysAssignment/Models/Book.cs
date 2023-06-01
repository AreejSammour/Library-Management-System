using System.ComponentModel.DataAnnotations;

namespace LibManSysAssignment.Models
{
	public class Book
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
        [StringLength(10, MinimumLength = 10)]
        public string ISBN { get; set; }
		public int Quantity { get; set; }
		public List<Author> Authors { get; set; }
		public List<Genre> Genres { get; set; }
		public List<BookCopy> Copys { get; set; }
	}
}
