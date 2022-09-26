using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TestJQueryMVC.Data;
using TestJQueryMVC.Models;
using TestJQueryMVC.ViewModel;

namespace TestJQueryMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyAppDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyAppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Create()
        {
            CustemerCreateModel model = new CustemerCreateModel();

            model.Customer = new Customer();
            List<SelectListItem> counties = _context.Countries
                .OrderBy(c=>c.Name)
                .Select(c=> new SelectListItem { Value = c.Country_Id.ToString(), Text= c.Name}).ToList();

            model.Countries = counties;
            model.Cities = new List<SelectListItem>();

            return View(model);
        }

        [HttpGet]
        public IActionResult GetCities(int CountryId)
        {
            if(CountryId != 0)
            {
                List<SelectListItem> citiesSel = _context.Cities.Where(c => c.CountryId == CountryId)
                    .OrderBy(c => c.Name)
                    .Select(n => new SelectListItem
                    {
                        Value = n.CountryId.ToString(),
                        Text = n.Name
                    }).ToList();
                return Json(citiesSel);
            }
            return null;
        }

        public IActionResult List()
        {
            List<Customer> customers = _context.Customers.ToList();
            return View(customers);
        }

        public IActionResult Index()
        {
            return View();
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