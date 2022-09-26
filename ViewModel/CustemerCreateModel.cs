using Microsoft.AspNetCore.Mvc.Rendering;
using TestJQueryMVC.Models;

namespace TestJQueryMVC.ViewModel
{
    public class CustemerCreateModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}
