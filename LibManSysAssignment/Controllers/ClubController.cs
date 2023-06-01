using LibManSysAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibManSysAssignment.Controllers
{
    public class ClubController : Controller
    {
        private readonly AppDbContext Context;

        public ClubController(AppDbContext _Context)
        {
            Context = _Context;
        }
        public IActionResult Index()
        {
            List<Club> ClubsList = Context.Clubs.ToList();
            foreach (Club club in ClubsList)
            {
                List<User> Users = Context.Entry(club)
                            .Collection(b => b.Users)
                            .Query()
                            .ToList();
                club.Users = new List<User>();
                club.Users = Users;
            }

            return View(ClubsList);
        }
   
        public IActionResult Create()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Club obj)
		{
            List<Club> clubs = Context.Clubs.ToList();
            foreach(Club club in clubs)
            {
                if (club.Name == obj.Name) // Check if this name is already exist
                {
                    return RedirectToAction("ErrorClub");
				}
            }
			// Only add the users in the system to the club
            // Or you will be redirected to open record for the new user first
			List<User> Users = Context.Users.ToList();
            bool InDataBase = false;
            foreach(User user in Users) 
            {
                if (user.Name == obj.Users[0].Name || user.Name == obj.Users[1].Name)
                {
                    InDataBase = true;
                }
            }
            if (!InDataBase )
            {
                if (obj.Users[0].Name != "-" || obj.Users[1].Name != "-")
                {
					return RedirectToAction("ErrorUser");
				}
			}

			Context.Clubs.Add(obj);
			Context.SaveChanges();
			TempData["success"] = "Book created successfully";

			return RedirectToAction("Index"/*, "Club", new { id = obj.Id }*/);
		}

        public IActionResult ErrorClub()
        {
            return View();
        }

        public IActionResult ErrorUser()
        {
            return View();
        }

        public IActionResult Delete(int? id)
        {
            Club club = Context.Clubs.FirstOrDefault(x => x.Id == id);

            return View(club);
        }

        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = Context.Clubs.FirstOrDefault(x => x.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            Context.Clubs.Remove(obj);
            Context.SaveChanges();
            TempData["success"] = "Book deleted successfully";
            return RedirectToAction("Index");
        }

        public IActionResult ManageUsers(int id)
        {
            Club club = Context.Clubs.FirstOrDefault( x => x.Id == id);
            List<User> Users = Context.Entry(club)
                            .Collection(b => b.Users)
                            .Query()
                            .ToList();
            club.Users = new List<User>();
            club.Users = Users;
            return View(club);
        }

        public IActionResult AddUser (int id)
        {
			Club club = Context.Clubs.FirstOrDefault(x => x.Id == id);
			List<User> Users = Context.Entry(club)
							.Collection(b => b.Users)
							.Query()
							.ToList();
			club.Users = new List<User>();
			club.Users = Users;
			return View(club);
		}
    }

}
