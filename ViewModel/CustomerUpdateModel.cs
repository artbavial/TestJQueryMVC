using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TestJQueryMVC.Data;
using TestJQueryMVC.Models;

namespace TestJQueryMVC.ViewModel
{
    public class CustomerUpdateModel
    {
        private int? countryId;
        private int? cityId;

        public CustomerUpdateModel() { }

        public CustomerUpdateModel(Customer customer)
        {
            CustomerId = customer.CustomerId;
            Name = customer.Name;
            CountryId = customer.CountryId ?? -1;
            CityId = customer.CityId ?? -1;
        }

        public int CustomerId { get; set; }

        public string Name { get; set; }

        public int? CountryId { get => countryId; set => countryId = value < 0 ? null : value; }

        public int? CityId { get => cityId; set => cityId = value < 0 ? null : value; }

        public IEnumerable<SelectListItem>? Countries { get; set; }

        public IEnumerable<SelectListItem>? Cities { get; set; }

        public class Validator : AbstractValidator<CustomerUpdateModel>
        {
            public Validator(MyAppDbContext context)
            {
                RuleFor(x => x.Name)
                    .NotEmpty();

                RuleFor(x => x.CountryId)
                    .NotNull()
                    .When(x => x.CityId.HasValue);

                RuleFor(x => x.CustomerId)
                    .Must(id => context.Customers.Find(id) != null)
                    .WithMessage("Покупатель не найден");

                RuleFor(x => x.CountryId)
                    .Must(id => context.Countries.Find(id) != null)
                    .WithMessage("Страна не найдена");

                RuleFor(x => x.CityId)
                    .Must(id => !id.HasValue || context.Cities.Find(id) != null)
                    .WithMessage("Город не найден");

                RuleFor(x => x.CityId)
                    .Must((customer, cityId) => context.Cities.Find(cityId)?.CountryId == customer.CountryId)
                    .When(x => x.CityId.HasValue)
                    .WithMessage("Город относится к другой стране");
            }
        }
    }
}
