using Tickets.Web.Areas.Admin.ViewModels;
using Tickets.Web.Models;

namespace Tickets.Web.Services
{
    public interface ICountryService
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetById(int id);
        Task<ApiResponse> Create(CountryVm model);
        Task<ApiResponse> Update(CountryVm model);
        Task<ApiResponse> Delete(int id);
    }
}
