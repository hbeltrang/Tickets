using Tickets.Domain;

namespace Tickets.Application.Specifications.States
{
    public class StateForCountingSpecification : BaseSpecification<State>
    {
        public StateForCountingSpecification(StateSpecificationParams stateParams)
            : base(
                x =>
                 (string.IsNullOrEmpty(stateParams.Search) || x.Name!.Contains(stateParams.Search)) &&
                (!stateParams.CountryId.HasValue || x.CountryId == stateParams.CountryId)
            )
        {

        }
    }
}
