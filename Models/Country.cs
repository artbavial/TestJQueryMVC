using System.ComponentModel.DataAnnotations;

namespace TestJQueryMVC.Models
{
    public class Country
    {
        [Key]
        public int Country_Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
