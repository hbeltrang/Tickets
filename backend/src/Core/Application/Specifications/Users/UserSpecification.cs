using Tickets.Domain;

namespace Tickets.Application.Specifications.Users
{
    public class UserSpecification : BaseSpecification<ApplicationUser>
    {
        public UserSpecification(UserSpecificationParams userParams) : base(
            x =>
            (string.IsNullOrEmpty(userParams.Search) || x.Name!.Contains(userParams.Search)
             || x.LastName!.Contains(userParams.Search) || x.Email!.Contains(userParams.Search)
            )
        )
        {
            ApplyPaging(userParams.PageSize * (userParams.PageIndex - 1), userParams.PageSize);

            if (!string.IsNullOrEmpty(userParams.Sort))
            {
                switch (userParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(p => p.Name!);
                        break;

                    case "nameDesc":
                        AddOrderByDescending(p => p.Name!);
                        break;

                    case "lastnameAsc":
                        AddOrderBy(p => p.LastName!);
                        break;

                    case "lastnameDesc":
                        AddOrderByDescending(p => p.LastName!);
                        break;

                    default:
                        AddOrderBy(p => p.Name!);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(p => p.Name!);
            }
        }
    }
}
