using System.Diagnostics;
using WebDeveloperRating.Data;
using WebDeveloperRating.ViewModels;
using WebDeveloperRating.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebDeveloperRating.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            IndexHomeViewModel model = new IndexHomeViewModel()
            {
                UsersCount = await context.Users.CountAsync(),
                WebDevelopersCount = await context.WebDevelopers.CountAsync(),
                ReviewsCount = await context.Reviews.CountAsync()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
