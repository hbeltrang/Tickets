using Tickets.Web.Models;

namespace Tickets.Web.Services
{
    public interface IApiService<T> where T : class
    {
        Task<ApiResponse> GetAllAsync(string endpoint, List<T> entity);
        Task<T> GetByIdAsync(string endpoint);

        //Task<ApiResponse> GetAllAsync(string endpoint);
        //Task<ApiResponse> GetByIdAsync(string endpoint);
        Task<ApiResponse> CreateAsync(string endpoint, T model);
        //Task<ApiResponse> UpdateAsync(string endpoint, T model);
        //Task<ApiResponse> DeleteAsync(string endpoint);
    }
}

