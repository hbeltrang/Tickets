using AutoMapper;
using Tickets.Application.Features.Categories.Vms;
using Tickets.Application.Features.Countries.Vms;
using Tickets.Domain;

namespace Tickets.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryVm>();
            CreateMap<Category, CategoryVm>();
        }
    }
}
