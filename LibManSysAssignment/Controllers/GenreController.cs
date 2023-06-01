using LibManSysAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibManSysAssignment.Controllers
{
	public class GenreController : Controller
	{
        private readonly AppDbContext Context;

        public GenreController(AppDbContext _Context)
        {
            Context = _Context;
        }

        public IActionResult Index()
		{
			List<Genre> GenresList = Context.Genres.ToList();
            List<Genre> distinctList = GenresList.Where(x => x.Name != "-").GroupBy(x => x.Name).Select(x => x.First()).ToList();
            foreach (Genre item in distinctList)
            {
                var BooksList = Context.Entry(item)
                            .Collection(b => b.Books)
                            .Query()
                            .ToList();
                item.Books = new List<Book>();
                item.Books = BooksList;
            }

            return View(distinctList);
		}
	}
}
