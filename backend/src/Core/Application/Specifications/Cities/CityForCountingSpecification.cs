using Tickets.Domain;

namespace Tickets.Application.Specifications.Cities
{
    public class CityForCountingSpecification : BaseSpecification<City>
    {
        public CityForCountingSpecification(CitySpecificationParams stateParams)
            : base(
                x =>
                 (string.IsNullOrEmpty(stateParams.Search) || x.Name!.Contains(stateParams.Search)) &&
                (!stateParams.CountryId.HasValue || x.CountryId == stateParams.CountryId) &&
                (!stateParams.StateId.HasValue || x.StateId == stateParams.CountryId)
            )
        {

        }

    }
}
