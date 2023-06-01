using LibManSysAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibManSysAssignment.Controllers
{
	public class LoanController : Controller
	{
		private readonly AppDbContext Context;

		public LoanController(AppDbContext _Context)
		{
			Context = _Context;
		}
		public IActionResult Index()
		{
			List<Loan> LoansList = Context.Loans.ToList();
			foreach (Loan loan in LoansList)
			{
                List<BookCopy> CopyList = Context.Entry(loan)
                            .Collection(b => b.Copy)
                            .Query()
                            .ToList();

				if (CopyList != null)
				{
                    foreach (BookCopy copy in CopyList)
                    {
                        Book book = Context.Books.FirstOrDefault(x => x.Id == copy.BookId);
                    }
                }
				loan.Copy = CopyList;

				User user = Context.Users.FirstOrDefault(x => x.Id == loan.UserId);
				loan.User = user;
            }
				
            return View(LoansList);
		}
	
		public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Create(UserLoanView obj)
		{
			User user = Context.Users.FirstOrDefault(x => x.Name == obj.UserName);
			if (user == null)
			{
				return RedirectToAction("UserNameError");
			}
			else
			{
				List<BookCopy> bookCopies = new List<BookCopy>();
				Loan NewLoan = new Loan();
				for (int i=0; i<5; i++)
				{
					Book book = Context.Books.Include(x => x.Copys).FirstOrDefault(x => x.Title == obj.Books[i]);
					if (book == null)
					{
						obj.Books[i] = obj.Books[i] + ": There is no book with this name. ";
					}
					else
					{
						BookCopy copy = Context.BookCopy
							.FirstOrDefault(x => x.BookId == book.Id && x.IsAvailable == true);
						if (copy == null)
						{
							obj.Books[i] = obj.Books[i] + "unavailable inhouse";
						}
						else
						{
							copy.LoanId = NewLoan.Id;
							copy.IsAvailable = false;
							bookCopies.Add(copy);
							Context.BookCopy.Update(copy);
						}
					}
				}
				
				NewLoan.UserId = user.Id;
				NewLoan.Status = "Checkout"; 
				NewLoan.Copy = bookCopies;

				Context.Loans.Add(NewLoan);
				Context.SaveChanges();
			}
            return RedirectToAction("Index");
        }

		public IActionResult UserNameError()
		{
			return View();
		}

		public IActionResult Detail(int id)
		{
			Loan loan = Context.Loans.Include(x => x.User)
                            .Include(x => x.Copy).FirstOrDefault(x => x.Id == id);

			if (loan.Copy != null)
			{
				foreach (var copy in loan.Copy)
				{
					Book book = Context.Books.FirstOrDefault(x => x.Id == copy.BookId);
					copy.Book = book;
                }
				
			}
            return View(loan);
		}
        
        public IActionResult ChangeStatus(int id)
		{
			Loan loan = Context.Loans.FirstOrDefault(x => x.Id == id);
			if (loan != null)
			{
                List<BookCopy> bookCopies = Context.BookCopy.Where(x => x.LoanId == loan.Id).ToList();
                loan.Status = new string("CheckIn");

                foreach (var copy in bookCopies)
                {
                    copy.IsAvailable = true;
                    Context.BookCopy.Update(copy);
                    Context.SaveChanges();
                }
				loan.Copy = new List<BookCopy>();
				loan.Copy.AddRange(bookCopies);
            }
			

			Context.Loans.Update(loan);
			Context.SaveChanges();

            return RedirectToAction("Index");
        }

		public IActionResult Confirmation(Loan obj) 
		{ 
			return View(obj);
		}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult StatusInhouse(Loan obj)
        //{
        //	Loan loan = Context.Loans.Include(x => x.User)
        //                          .Include(x => x.Copy).FirstOrDefault(x => x.Id == obj.Id);

        //	User user = Context.Users.Include(x => x.LoanList).FirstOrDefault(x => x.Id == obj.UserId);

        //	List<BookCopy> LoansList = Context.BookCopy.Where( x => x.LoanId == obj.Id).ToList();

        //	loan.UserId = new int();
        //	loan.UserId = user.Id;

        //	loan.LoanDate = DateTime.Now;
        //	loan.LoanDate = obj.LoanDate;

        //	loan.Status = new string("");
        //	loan.Status = "CheckIn";

        //          foreach (BookCopy bookCopy in LoansList)
        //	{
        //		bookCopy.IsAvailable = true;
        //		Context.BookCopy.Update(bookCopy);
        //	}

        //	loan.Copy = new List<BookCopy>();
        //	loan.Copy = LoansList;

        //	Context.Loans.Update(loan);
        //	Context.SaveChanges();
        //	return RedirectToAction("Index");
        //}
    }
}
