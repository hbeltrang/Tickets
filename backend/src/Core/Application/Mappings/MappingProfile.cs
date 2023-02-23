using AutoMapper;
using Tickets.Application.Features.Categories.Commands.CreateCategory;
using Tickets.Application.Features.Categories.Commands.UpdateCategory;
using Tickets.Application.Features.Categories.Vms;
using Tickets.Application.Features.Countries.Commands.CreateCountry;
using Tickets.Application.Features.Countries.Commands.UpdateCountry;
using Tickets.Application.Features.Countries.Vms;
using Tickets.Application.Features.States.Commands.CreateState;
using Tickets.Application.Features.States.Commands.UpdateState;
using Tickets.Application.Features.States.Vms;
using Tickets.Domain;

namespace Tickets.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryVm>();
            CreateMap<CreateCountryCommand, Country>();
            CreateMap<UpdateCountryCommand, Country>();

            CreateMap<Category, CategoryVm>();
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();

            CreateMap<State, StateVm>()
                 .ForMember(p => p.CountryName, x => x.MapFrom(a => a.Country!.Name));
            CreateMap<CreateStateCommand, State>();
            CreateMap<UpdateStateCommand, State>();

        }
    }
}
