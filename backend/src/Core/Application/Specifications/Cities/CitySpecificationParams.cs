namespace Tickets.Application.Specifications.Cities
{
    public class CitySpecificationParams: SpecificationParams
    {
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
    }
}
