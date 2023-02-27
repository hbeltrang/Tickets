using AutoMapper;
using Tickets.Application.Features.Categories.Commands.CreateCategory;
using Tickets.Application.Features.Categories.Commands.UpdateCategory;
using Tickets.Application.Features.Categories.Vms;
using Tickets.Application.Features.Cities.Commands.CreateCity;
using Tickets.Application.Features.Cities.Commands.UpdateCity;
using Tickets.Application.Features.Cities.Vms;
using Tickets.Application.Features.Companies.Commands.CreateCompany;
using Tickets.Application.Features.Companies.Commands.UpdateCompany;
using Tickets.Application.Features.Companies.Vms;
using Tickets.Application.Features.Countries.Commands.CreateCountry;
using Tickets.Application.Features.Countries.Commands.UpdateCountry;
using Tickets.Application.Features.Countries.Vms;
using Tickets.Application.Features.Privacy.Commands.CreatePrivacy;
using Tickets.Application.Features.Privacy.Commands.UpdatePrivacy;
using Tickets.Application.Features.Privacy.Vms;
using Tickets.Application.Features.Promoters.Commands.CreatePromoter;
using Tickets.Application.Features.Promoters.Commands.UpdatePromoter;
using Tickets.Application.Features.Promoters.Vms;
using Tickets.Application.Features.SocialImages.Vms;
using Tickets.Application.Features.Socials.Commands.CreateSocial;
using Tickets.Application.Features.Socials.Commands.UpdateSocial;
using Tickets.Application.Features.Socials.Vms;
using Tickets.Application.Features.States.Commands.CreateState;
using Tickets.Application.Features.States.Commands.UpdateState;
using Tickets.Application.Features.States.Vms;
using Tickets.Application.Features.Taxes.Commands.CreateTax;
using Tickets.Application.Features.Taxes.Commands.UpdateTax;
using Tickets.Application.Features.Taxes.Vms;
using Tickets.Application.Features.Terms.Commands.CreateTerm;
using Tickets.Application.Features.Terms.Commands.UpdateTerm;
using Tickets.Application.Features.Terms.Vms;
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

            CreateMap<City, CityVm>()
                 .ForMember(p => p.CountryName, x => x.MapFrom(a => a.Country!.Name))
                 .ForMember(p => p.StateName, x => x.MapFrom(a => a.State!.Name));
            CreateMap<CreateCityCommand, City>();
            CreateMap<UpdateCityCommand, City>();

            CreateMap<Tax, TaxVm>();
            CreateMap<CreateTaxCommand, Tax>();
            CreateMap<UpdateTaxCommand, Tax>();

            CreateMap<Term, TermVm>();
            CreateMap<CreateTermCommand, Term>();
            CreateMap<UpdateTermCommand, Term>();

            CreateMap<PrivacyPolicy, PrivacyVm>();
            CreateMap<CreatePrivacyCommand, PrivacyPolicy>();
            CreateMap<UpdatePrivacyCommand, PrivacyPolicy>();

            CreateMap<SocialImage, SocialImageVm>();
            CreateMap<CreateSocialImageCommand, SocialImage>();
            CreateMap<Social, SocialVm>();
            CreateMap<CreateSocialCommand, Social>();
            CreateMap<UpdateSocialCommand, Social>();

            CreateMap<CompanyImage, CompanyImageVm>();
            CreateMap<CreateCompanyImageCommand, CompanyImage>();
            CreateMap<Company, CompanyVm>();
            CreateMap<CreateCompanyCommand, Company>();
            CreateMap<UpdateCompanyCommand, Company>();

            CreateMap<Promoter, PromoterVm>();
            CreateMap<CreatePromoterCommand, Promoter>();
            CreateMap<UpdatePromoterCommand, Promoter>();


        }
    }
}
