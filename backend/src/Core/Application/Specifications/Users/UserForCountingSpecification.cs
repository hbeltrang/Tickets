using Tickets.Domain;

namespace Tickets.Application.Specifications.Users
{
    public class UserForCountingSpecification : BaseSpecification<ApplicationUser>
    {
        public UserForCountingSpecification(UserSpecificationParams userParams) : base(
            x =>
            (string.IsNullOrEmpty(userParams.Search) || x.Name!.Contains(userParams.Search)
             || x.LastName!.Contains(userParams.Search) || x.Email!.Contains(userParams.Search)
            )
        )
        {
        }
    }
}
