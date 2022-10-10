using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            var dbCustomer = new Customer
            {
                Name = customer.Name,
                CountryId = customer.CountryId,
                CityId = customer.CityId
            };

            await _context.Customers.AddAsync(dbCustomer);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IEnumerable<SelectListItem> GetCities(int CountryId)
        {
            if(CountryId != 0)
            {
                List<SelectListItem> citiesSel = _context.Cities.Where(c => c.CountryId == CountryId)
                    .OrderBy(c => c.Name)
                    .Select(n => new SelectListItem
                    {
                        Value = n.City_Id.ToString(),
                        Text = n.Name
                    }).ToList();
                return citiesSel;
            }
            return Enumerable.Empty<SelectListItem>();
        }

        public IActionResult List()
        {
            var customers = _context.Customers
                .Include(u => u.Country)
                .Include(u => u.City)
                .ToList();

            return View(customers);
        }

        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            CustemerCreateModel model = new CustemerCreateModel();

            model.Customer = _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
            List<SelectListItem> counties = _context.Countries
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem { Value = c.Country_Id.ToString(), Text = c.Name }).ToList();

            model.Countries = counties;
            model.Cities = _context.Cities
                .Where(x=>x.CountryId == model.Customer.CountryId)
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem { Value = c.City_Id.ToString(), Text = c.Name }).ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(CustemerCreateModel model)
        {
            _context.Update(model.Customer);
            await _context.SaveChangesAsync();
     
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            _context.Remove(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction("List");
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