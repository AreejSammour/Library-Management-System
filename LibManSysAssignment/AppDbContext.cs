using Azure;
using LibManSysAssignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace LibManSysAssignment
{
    public class AppDbContext : DbContext
	{
		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<User> Users { get; set; }	
		public DbSet<Address> Address { get; set; }
		public DbSet<BookCopy> BookCopy { get; set; }
		public DbSet<Loan> Loans { get; set; }
		public DbSet<Club> Clubs { get; set; }
		
		public AppDbContext(DbContextOptions<AppDbContext> options)
			 : base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Seed-Data
			//Book book = new Book() { Id = 1, Title = "", ISBN = "", Quantity = 1};

			Book book1 = new Book() { Id = 1, Title = "Book1", ISBN = "11111111", Quantity = 1 };
			Book book2 = new Book() { Id = 2, Title = "Book2", ISBN = "22222222", Quantity = 2 };
			Book book3 = new Book() { Id = 3, Title = "Book3", ISBN = "33333333", Quantity = 3 };
			Book book4 = new Book() { Id = 4, Title = "Book4", ISBN = "44444444", Quantity = 4 };
			Book book5 = new Book() { Id = 5, Title = "Book5", ISBN = "55555555", Quantity = 5 };
			Book book6 = new Book() { Id = 6, Title = "Book6", ISBN = "66666666", Quantity = 6 };	
			Book book7 = new Book() { Id = 7, Title = "Book7", ISBN = "77777777", Quantity = 7 };
			Book book8 = new Book() { Id = 8, Title = "Book8", ISBN = "88888888", Quantity = 8 };
			Book book9 = new Book() { Id = 9, Title = "Book9", ISBN = "99999999", Quantity = 9 };
			Book book10 = new Book() { Id = 10, Title = "Book10", ISBN = "1010101010", Quantity = 10 };

			modelBuilder.Entity<Book>().HasData(book1, book2, book3, book4, book5, book6, book7, book8, book9, book10);

			Author author1 = new Author() { Id = 1, Name = "Author1" };
			Author author2 = new Author() { Id = 2, Name = "Author2" };
			Author author3 = new Author() { Id = 3, Name = "Author3" };
			Author author4 = new Author() { Id = 4, Name = "Author4" };
			Author author5 = new Author() { Id = 5, Name = "Author5" };
			Author author6 = new Author() { Id = 6, Name = "Author6" };
			Author author7 = new Author() { Id = 7, Name = "Author7" };
			Author author8 = new Author() { Id = 8, Name = "Author8" };
			Author author9 = new Author() { Id = 9, Name = "Author9" };
			Author author10 = new Author() { Id = 10, Name = "Author10" };

			modelBuilder.Entity<Author>().HasData(author1, author2, author3, author4, author5, author6, author7, author8, author9, author10);

			modelBuilder.Entity("AuthorBook").HasData(
				new { AuthorsId = 1, BooksId = 1 },
				new { AuthorsId = 2, BooksId = 2 },
				new { AuthorsId = 3, BooksId = 3 },
				new { AuthorsId = 4, BooksId = 4 },
				new { AuthorsId = 5, BooksId = 5 },
				new { AuthorsId = 6, BooksId = 6 },
				new { AuthorsId = 7, BooksId = 7 },
				new { AuthorsId = 8, BooksId = 8 },
				new { AuthorsId = 9, BooksId = 9 },
				new { AuthorsId = 10, BooksId = 10 },
				new { AuthorsId = 1, BooksId = 5 },
				new { AuthorsId = 4, BooksId = 8 },
				new { AuthorsId = 9, BooksId = 1 });

			Genre genre1 = new Genre() { Id = 1, Name = "Genre1" };
			Genre genre2 = new Genre() { Id = 2, Name = "Genre2" };
			Genre genre3 = new Genre() { Id = 3, Name = "Genre3" };
			Genre genre4 = new Genre() { Id = 4, Name = "Genre4" };
			Genre genre5 = new Genre() { Id = 5, Name = "Genre5" };
			Genre genre6 = new Genre() { Id = 6, Name = "Genre6" };

			modelBuilder.Entity<Genre>().HasData(genre1, genre2, genre3, genre4, genre5, genre6);

			modelBuilder.Entity("BookGenre").HasData(
				new { BooksId = 1,  GenresId = 1 },
				new { BooksId = 2,  GenresId = 2 },
				new { BooksId = 3,  GenresId = 3 },
				new { BooksId = 4,  GenresId = 4 },
				new { BooksId = 5,  GenresId = 5 },
				new { BooksId = 6,  GenresId = 6 },
				new { BooksId = 7,  GenresId = 1 },
				new { BooksId = 8,  GenresId = 2 },
				new { BooksId = 9,  GenresId = 3 },
				new { BooksId = 10, GenresId = 4 });

			User user1 = new User() { Id = 1, Name = "User1", PhoneNo = 0123456789 };
			User user2 = new User() { Id = 2, Name = "User2", PhoneNo = 0123456789 };
			User user3 = new User() { Id = 3, Name = "User3", PhoneNo = 0123456789 };
			User user4 = new User() { Id = 4, Name = "User4", PhoneNo = 0123456789 };
            User user5 = new User() { Id = 5, Name = "User5", PhoneNo = 0123456789 };
            User user6 = new User() { Id = 6, Name = "User6", PhoneNo = 0123456789 };
            User user7 = new User() { Id = 7, Name = "User7", PhoneNo = 0123456789 };
            User user8 = new User() { Id = 8, Name = "User8", PhoneNo = 0123456789 };
			User user9 = new User() { Id = 9, Name = "User9", PhoneNo = 0123456789 };
            User user10 = new User() { Id = 10, Name = "User10", PhoneNo = 0123456789 };

            modelBuilder.Entity<User>().HasData(user1, user2, user3, user4, user5, user6, user7, user8, user9, user10);

			Address address1 = new Address() { Id = 1, Street = "Street1", PostCode = 12345 , City ="City1", Country = "Country1", UserId = 1};
			Address address2 = new Address() { Id = 2, Street = "Street2", PostCode = 12345, City = "City2", Country = "Country2", UserId = 2 };
			Address address3 = new Address() { Id = 3, Street = "Street3", PostCode = 12345, City = "City3", Country = "Country3", UserId = 3 };
			Address address4 = new Address() { Id = 4, Street = "Street4", PostCode = 12345, City = "City4", Country = "Country4", UserId = 4 };
            Address address5 = new Address() { Id = 5, Street = "Street5", PostCode = 12345, City = "City5", Country = "Country5", UserId = 5 };
            Address address6 = new Address() { Id = 6, Street = "Street6", PostCode = 12345, City = "City6", Country = "Country6", UserId = 6 };
            Address address7 = new Address() { Id = 7, Street = "Street7", PostCode = 12345, City = "City7", Country = "Country7", UserId = 7 };
            Address address8 = new Address() { Id = 8, Street = "Street8", PostCode = 12345, City = "City8", Country = "Country8", UserId = 8 };
            Address address9 = new Address() { Id = 9, Street = "Street9", PostCode = 12345, City = "City9", Country = "Country9", UserId = 9 };
            Address address10 = new Address() { Id = 10, Street = "Street10", PostCode = 12345, City = "City10", Country = "Country10", UserId = 10 };

            modelBuilder.Entity<Address>().HasData(address1, address2, address3, address4, address5, address6, address7, address8, address9, address10);

			Loan loan1 = new Loan() { Id = 1, UserId = 1, LoanDate = DateTime.Now ,Status ="CheckIn"};
			Loan loan2 = new Loan() { Id = 2, UserId = 2, LoanDate = DateTime.Now ,Status ="Checkout"};
			Loan loan3 = new Loan() { Id = 3, UserId = 3, LoanDate = DateTime.Now ,Status ="Checkout"};
			Loan loan4 = new Loan() { Id = 4, UserId = 4, LoanDate = DateTime.Now ,Status ="Checkout"};
            Loan loan5 = new Loan() { Id = 5, UserId = 5, LoanDate = DateTime.Now, Status = "CheckIn" };
            Loan loan6 = new Loan() { Id = 6, UserId = 6, LoanDate = DateTime.Now, Status = "Checkout" };
            Loan loan7 = new Loan() { Id = 7, UserId = 7, LoanDate = DateTime.Now, Status = "CheckIn" };
            Loan loan8 = new Loan() { Id = 8, UserId = 8, LoanDate = DateTime.Now, Status = "Checkout" };
            Loan loan9 = new Loan() { Id = 9, UserId = 9, LoanDate = DateTime.Now, Status = "CheckIn" };
            Loan loan10 = new Loan() { Id = 10, UserId = 10, LoanDate = DateTime.Now, Status = "Checkout" };

            modelBuilder.Entity<Loan>().HasData(loan1, loan2, loan3, loan4, loan5, loan6, loan7, loan8, loan9, loan10);  

			BookCopy bookCopy1 = new BookCopy() { Id = 1, IsAvailable = true, BookId = 1   , LoanId =1};
																							
            BookCopy bookCopy2 = new BookCopy() { Id = 2, IsAvailable = true, BookId = 2   , LoanId =1};
            BookCopy bookCopy3 = new BookCopy() { Id = 3, IsAvailable = false, BookId = 2  , LoanId =2};
																							
            BookCopy bookCopy4 = new BookCopy() { Id = 4, IsAvailable = true, BookId = 3   , LoanId =1};
            BookCopy bookCopy5 = new BookCopy() { Id = 5, IsAvailable = false, BookId = 3  , LoanId =2};
            BookCopy bookCopy6 = new BookCopy() { Id = 6, IsAvailable = false, BookId = 3   , LoanId =3};
																							
            BookCopy bookCopy7 = new BookCopy() { Id = 7, IsAvailable = true, BookId = 4   , LoanId =1};
            BookCopy bookCopy8 = new BookCopy() { Id = 8, IsAvailable = false, BookId = 4   , LoanId =2};
            BookCopy bookCopy9 = new BookCopy() { Id = 9, IsAvailable = false, BookId = 4  , LoanId =3};
            BookCopy bookCopy10 = new BookCopy() { Id = 10, IsAvailable = false, BookId = 4, LoanId =4};

            BookCopy bookCopy11 = new BookCopy() { Id = 11, IsAvailable = true, BookId = 5, LoanId = 5 };
            BookCopy bookCopy12 = new BookCopy() { Id = 12, IsAvailable = false, BookId = 6, LoanId = 6 };
            BookCopy bookCopy13 = new BookCopy() { Id = 13, IsAvailable = true, BookId = 7, LoanId = 7 };
            BookCopy bookCopy14 = new BookCopy() { Id = 14, IsAvailable = false, BookId = 8, LoanId = 8 };
            BookCopy bookCopy15 = new BookCopy() { Id = 15, IsAvailable = true, BookId = 9, LoanId = 9 };
            BookCopy bookCopy16 = new BookCopy() { Id = 16, IsAvailable = false, BookId = 10, LoanId = 10 };

            modelBuilder.Entity<BookCopy>().HasData(bookCopy1, bookCopy2, bookCopy3, bookCopy4, bookCopy5, bookCopy6, bookCopy7, bookCopy8, bookCopy9, bookCopy10, bookCopy11, bookCopy12, bookCopy13, bookCopy14, bookCopy15, bookCopy16);
        
			Club club1 = new Club() { Id = 1, Name = "Club1" };
            Club club2 = new Club() { Id = 2, Name = "Club2" };
            Club club3 = new Club() { Id = 3, Name = "Club3" };
            Club club4 = new Club() { Id = 4, Name = "Club4" };
            Club club5 = new Club() { Id = 5, Name = "Club5" };

			modelBuilder.Entity<Club>().HasData(club1, club2, club3, club4, club5);

            modelBuilder.Entity("ClubUser").HasData(
                new { ClubListId = 1, UsersId = 1 },
                new { ClubListId = 2, UsersId = 2 },
                new { ClubListId = 3, UsersId = 3 },
                new { ClubListId = 4, UsersId = 4 },
                new { ClubListId = 5, UsersId = 5 });
        }
	}
}
