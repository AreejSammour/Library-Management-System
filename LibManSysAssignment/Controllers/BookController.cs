using Microsoft.AspNetCore.Mvc;
using LibManSysAssignment.Models;
using static System.Reflection.Metadata.BlobBuilder;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace LibManSysAssignment.Controllers
{
	public class BookController : Controller
	{
		private readonly AppDbContext Context;

		public BookController(AppDbContext _Context)
		{
			Context = _Context;
		}
		public IActionResult Index()
		{
			List<Book> books = Context.Books.ToList();
			foreach (Book item in books)
			{
				var AuthorsList = Context.Entry(item)
							.Collection(b => b.Authors)
							.Query()
							.ToList();
				item.Authors = new List<Author>();
				item.Authors = AuthorsList;
			}
			foreach (Book item in books)
			{
				var GenresList = Context.Entry(item)
							.Collection(b => b.Genres)
							.Query()
							.ToList();
				item.Genres = new List<Genre>();
				item.Genres = GenresList;
			}
			return View(books);
		}

		public IActionResult Create()
		{
			return View();
		}
        
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Book obj)
		{
			if (obj.Quantity != 0)
			{
				List<BookCopy> CopysList = new List<BookCopy>();
				for(int i = 0; i< obj.Quantity; i++)
				{
					BookCopy copy = new BookCopy();
					copy.BookId = obj.Id;
					copy.IsAvailable = true;
					copy.LoanId = 1;
					CopysList.Add(copy);
				}
				obj.Copys = new List<BookCopy>();
				obj.Copys = CopysList;
			}
			
			Context.Books.Add(obj);
			Context.SaveChanges();
            TempData["success"] = "Book created successfully";

			return RedirectToAction("Edit", "Book", new { id = obj.Id });
		}

		//Get
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Book book = Context.Books.Include(x => x.Authors)
				.Include(x => x.Genres)
				.Include(x => x.Copys).FirstOrDefault(x => x.Id == id);

			var AuthorsList = Context.Entry(book)
							.Collection(b => b.Authors)
							.Query()
							.ToList();

			while (AuthorsList.Count < 3)
			{
				Author author = new Author();
				author.Name = "-";
				AuthorsList.Add(author);
			}

			book.Authors = new List<Author>();
			book.Authors = AuthorsList;

			var GenresList = Context.Entry(book)
							.Collection(b => b.Genres)
							.Query()
							.ToList();

			while (GenresList.Count < 3)
			{
				Genre genre = new Genre();
				genre.Name = "-";
				GenresList.Add(genre);
			}

			book.Genres = new List<Genre>();
			book.Genres = GenresList;

			List<BookCopy> CopysList = Context.BookCopy.Where(x => x.BookId == book.Id).ToList();

			if (CopysList != null)
			{
				book.Copys = new List<BookCopy> ();
				book.Copys = CopysList;
			}
			else
			{
				for (int i = 0; i < book.Quantity; i++)
				{
					BookCopy bookCopy = new BookCopy();
					bookCopy.BookId = book.Id;
					bookCopy.IsAvailable = true;
					bookCopy.LoanId = 1;
					CopysList.Add(bookCopy);
				}
				book.Copys = new List<BookCopy>();
				book.Copys = CopysList;
			}

			if (book == null)
			{
				return NotFound();
			}
			return View(book);
		}
		
		//Post
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Book obj)
		{
			Book book = Context.Books.Include(x => x.Authors)
				.Include(x => x.Genres)
				.Include(x => x.Copys).FirstOrDefault(x => x.Id == obj.Id);

			book.Authors = new List<Author>();
			book.Authors = obj.Authors;

			book.Genres = new List<Genre>();
			book.Genres = obj.Genres;

			book.Title = obj.Title;
			book.ISBN = obj.ISBN;
			book.Quantity = book.Quantity;

			Context.Books.Update(book);
			Context.SaveChanges();
		
			return RedirectToAction("Index");
		}

		public IActionResult AddCopies(Book obj)
		{
			Book book = Context.Books.Include(x => x.Authors)
							.Include(x => x.Genres)
							.Include(x => x.Copys).FirstOrDefault(x => x.Id == obj.Id);

			book.Authors = new List<Author>();
			book.Authors = obj.Authors;

			book.Genres = new List<Genre>();
			book.Genres = obj.Genres;

			book.Title = obj.Title;
			book.ISBN = obj.ISBN;
			book.Quantity++;

			BookCopy bookCopy = new BookCopy();
			bookCopy.BookId = book.Id;
			bookCopy.IsAvailable = true;
			bookCopy.LoanId = 1;

			List<BookCopy> copys = new List<BookCopy>();
			if (book.Copys != null)
			{
				copys = book.Copys;
				copys.Add(bookCopy);
			}
			else
			{
				copys.Add(bookCopy);
				
			}
			book.Copys = new List<BookCopy>();
			book.Copys = copys;

			Context.Books.Update(book);
			Context.SaveChanges();

			return RedirectToAction("Edit", "Book", new { id = book.Id });
		}

		//Get
		public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var book = Context.Books.FirstOrDefault(x => x.Id == id);
			var AuthorsList = Context.Entry(book)
							.Collection(b => b.Authors)
							.Query()
							.ToList();
			while (AuthorsList.Count < 3)
			{
				Author author = new Author();
				author.Name = "-";
				AuthorsList.Add(author);
			}

			book.Authors = new List<Author>();
			book.Authors = AuthorsList;

			var GenresList = Context.Entry(book)
							.Collection(b => b.Genres)
							.Query()
							.ToList();

			while (GenresList.Count < 3)
			{
				Genre genre = new Genre();
				genre.Name = "-";
				GenresList.Add(genre);
			}

			book.Genres = new List<Genre>();
			book.Genres = GenresList;

			if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        
		//Post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
			var obj = Context.Books.FirstOrDefault(x => x.Id ==id);

			if (obj == null)
			{
				return NotFound();
			}
            Context.Books.Remove(obj);
            Context.SaveChanges();
            TempData["success"] = "Book deleted successfully";
            return RedirectToAction("Index");

			return View(obj);
        }

		public IActionResult Detail(int id)
		{
			var book = Context.Books.FirstOrDefault(x=> x.Id ==id);
            var AuthorsList = Context.Entry(book)
                            .Collection(b => b.Authors)
                            .Query()
                            .ToList();
			book.Authors = new List<Author>();
			book.Authors = AuthorsList;

			var GenresList = Context.Entry(book)
                            .Collection(b => b.Genres)
                            .Query()
                            .ToList();
			book.Genres = new List<Genre>();
			book.Genres = GenresList;

			List<BookCopy> CopyList = Context.BookCopy.Where(x => x.BookId == book.Id).ToList();

			if (CopyList.Count < book.Quantity)
			{
				for (int i=0; i<book.Quantity; i++)
				{
					if ( i >= CopyList.Count)
					{
						BookCopy copy = new BookCopy();
						copy.BookId = book.Id;
						copy.LoanId = 1;
						copy.IsAvailable = true;
						CopyList.Add(copy);
						Context.BookCopy.Add(copy);
                        Context.SaveChanges();
                    }
				}
			}
			
			book.Copys = new List<BookCopy>();
			book.Copys = CopyList;

			Context.Books.Update(book);
			Context.SaveChanges();

			return View(book);

        }
    
		public IActionResult SearchBook(string term = "")
		{
			term = string.IsNullOrEmpty(term)?"":term.ToLower();
			Book FindBook = Context.Books.FirstOrDefault(x => x.Title.ToLower() == term);
			if (FindBook == null)
			{
				return View("NotFound");
			}
            return RedirectToAction("Detail", "Book", new { id = FindBook.Id });
        }

		public IActionResult NotFound()
		{
			return View();
		}


    }
}
