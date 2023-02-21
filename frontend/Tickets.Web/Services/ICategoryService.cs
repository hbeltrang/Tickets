using Tickets.Web.Models;

namespace Tickets.Web.Services
{
    public interface ICategoryService
    {
        Task<ApiResponse> List();
        Task<ApiResponse> GetById(int id);
        Task<ApiResponse> Save(Category model);
        Task<ApiResponse> Edit(Category model);
        Task<ApiResponse> Delete(int id);

    }
}
