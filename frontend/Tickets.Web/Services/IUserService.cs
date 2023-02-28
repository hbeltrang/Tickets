using Tickets.Web.Models;

namespace Tickets.Web.Services
{
    public interface IUserService
    {
        Task<string> GetLoginApiAdminToken();
        Task<ApiResponse> LoginApiAdmin();
        Task<ApiResponse> Login(Login model);
        Task<ApiResponse> Register(UserRegister model);

    }
}
