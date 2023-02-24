using Tickets.Domain;

namespace Tickets.Application.Specifications.Countries
{
    public class CountrySpecification : BaseSpecification<Country>
    {
        public CountrySpecification(CountrySpecificationParams countryParams)
            : base(
                x =>
                 (string.IsNullOrEmpty(countryParams.Search) || x.Name!.Contains(countryParams.Search)) &&
                (!countryParams.CountryId.HasValue || x.Id == countryParams.CountryId)
            )
        {
            ApplyPaging(countryParams.PageSize * (countryParams.PageIndex - 1), countryParams.PageSize);

            if (!string.IsNullOrEmpty(countryParams.Sort))
            {
                switch (countryParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(p => p.Name!);
                        break;

                    case "nameDesc":
                        AddOrderByDescending(p => p.Name!);
                        break;
                    default:
                        AddOrderBy(p => p.CreatedDate!);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(p => p.CreatedDate!);
            }

        }
    }
}
