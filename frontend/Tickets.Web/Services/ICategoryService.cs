using Tickets.Web.Areas.Admin.ViewModels;
using Tickets.Web.Models;

namespace Tickets.Web.Services
{
    public interface ICategoryService
    {
        Task<ApiResponse> List();
        Task<ApiResponse> GetById(int id);
        Task<ApiResponse> Save(CategoryVm model);
        Task<ApiResponse> Edit(CategoryVm model);
        Task<ApiResponse> Delete(int id);

    }
}
