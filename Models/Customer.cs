using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestJQueryMVC.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Cтрана")]
        public int? CountryId { get; set; }
        public virtual Country? Country { get; set; }

        [DisplayName("Город")]
        public int? CityId { get; set; }
        public virtual City? City { get; set; }
    }
}
