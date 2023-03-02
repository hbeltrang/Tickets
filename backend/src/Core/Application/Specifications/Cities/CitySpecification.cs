using Tickets.Domain;

namespace Tickets.Application.Specifications.Cities
{
    public class CitySpecification : BaseSpecification<City>        
    {
        public CitySpecification(CitySpecificationParams stateParams)
            : base(
                x =>
                 (string.IsNullOrEmpty(stateParams.Search) || x.Name!.Contains(stateParams.Search)) &&
                (!stateParams.StateId.HasValue || x.StateId == stateParams.StateId)
            )
        {
            
            AddInclude(p => p.State!);
            AddInclude(p => p.State!.Country!);

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
