using Tickets.Web.Areas.Admin.ViewModels;
using Tickets.Web.Models;

namespace Tickets.Web.Services
{
    public interface ICategoryService
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetById(int id);
        Task<ApiResponse> Create(CategoryVm model);
        Task<ApiResponse> Update(CategoryVm model);
        Task<ApiResponse> Delete(int id);

    }
}
