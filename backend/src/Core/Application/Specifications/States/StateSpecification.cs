using Tickets.Domain;

namespace Tickets.Application.Specifications.States
{
    public class StateSpecification : BaseSpecification<State>
    {
        public StateSpecification(StateSpecificationParams stateParams)
            : base(
                x =>
                 (string.IsNullOrEmpty(stateParams.Search) || x.Name!.Contains(stateParams.Search)) &&
                (!stateParams.CountryId.HasValue || x.CountryId == stateParams.CountryId)
            )
        {

            AddInclude(p => p.Country!);

            ApplyPaging(stateParams.PageSize * (stateParams.PageIndex - 1), stateParams.PageSize);

            if (!string.IsNullOrEmpty(stateParams.Sort))
            {
                switch (stateParams.Sort)
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
