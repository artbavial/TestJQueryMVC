using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestJQueryMVC.Models
{
    public class City
    {
        [Key]
        public int City_Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
