using Tickets.Domain;

namespace Tickets.Application.Specifications.Countries
{
    public class CountryForCountingSpecification : BaseSpecification<Country>
    {
        public CountryForCountingSpecification(CountrySpecificationParams countryParams)
            : base(
                x =>
                 (string.IsNullOrEmpty(countryParams.Search) || x.Name!.Contains(countryParams.Search)) &&
                (!countryParams.CountryId.HasValue || x.Id == countryParams.CountryId)
            )
        {

        }
    }
}
