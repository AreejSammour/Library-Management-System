using LibManSysAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibManSysAssignment.Controllers
{
    public class UserController : Controller
	{
        private readonly AppDbContext Context;

        public UserController(AppDbContext _Context)
        {
            Context = _Context;
        }
        public IActionResult Index()
		{
			List<User> users = Context.Users.ToList();
            foreach (var user in users)
            {
                user.Address = Context.Address.FirstOrDefault(x => x.UserId == user.Id);
            }

            return View(users);
		}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User obj)
        {
            Context.Users.Add(obj);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			User user = Context.Users.FirstOrDefault(x => x.Id == id);
			Address	address = Context.Address.FirstOrDefault(x => x.UserId == id);

            user.Address = address;

			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		//Post
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(User obj)
		{
			User user = Context.Users.Include(x => x.Address).FirstOrDefault(x => x.Id == obj.Id);

			if (user.Address != obj.Address)
			{
				user.Address = new Address();
				user.Address = obj.Address;
			}
			
			user.Name = obj.Name;
			user.PhoneNo = obj.PhoneNo;

			Context.Users.Update(user);
			Context.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			User user = Context.Users.FirstOrDefault(x => x.Id == id);
			Address address = Context.Address.FirstOrDefault(x => x.UserId == id);

			user.Address = address;

			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		//Post
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			var obj = Context.Users.FirstOrDefault(x => x.Id == id);

			if (obj == null)
			{
				return NotFound();
			}
			Context.Users.Remove(obj);
			Context.SaveChanges();
			TempData["success"] = "Book deleted successfully";
			return RedirectToAction("Index");
		}

        public IActionResult Detail(int id)
        {
			User user = Context.Users.FirstOrDefault(x => x.Id == id);

			Address address = Context.Address.FirstOrDefault(x => x.UserId == id);
			user.Address = address;

			List<Loan> LoansList = Context.Loans.Where(x => x.UserId == user.Id).ToList();
			user.LoanList = LoansList;

            return View(user);

        }

        public IActionResult SearchUser(string term = "")
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            User FindUser = Context.Users.FirstOrDefault(x => x.Name.ToLower() == term);
            if (FindUser == null)
            {
                return View("NotFound");
            }
            return RedirectToAction("Detail", "User", new { id = FindUser.Id });
        }

        public IActionResult NotFound()
        {
            return View();
        }
    }

	
}
