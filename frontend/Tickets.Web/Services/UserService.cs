using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;
using Tickets.Web.Models;

namespace Tickets.Web.Services
{
    public class UserService : IUserService
    {
        private static string _apiUser;
        private static string _apiPwd;
        private static string _apiURL;
        private static string _token;

        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;

            _apiUser = configuration.GetValue<string>("ApiSettings:ApiUser");
            _apiPwd = configuration.GetValue<string>("ApiSettings:ApiPwd");
            _apiURL = configuration.GetValue<string>("ApiSettings:ApiURL");
        }

        public async Task<ApiResponse> LoginApiAdmin()
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                var endpoint = "api/v1/Users/login";

                var apiCredentialAdmin = new Login()
                {
                    Email = _apiUser,
                    Password = _apiPwd,
                };

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);

                var content = new StringContent(JsonConvert.SerializeObject(apiCredentialAdmin), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<AuthResponse>(jsonResponse);
                    if (result != null)
                    {
                        apiResponse.IsSuccess = false;
                        apiResponse.Message = "OK";
                        apiResponse.Result = result;
                    }
                }

            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "Error Service LoginApiAdmin" + ex.Message;
            }

            return apiResponse;
        }

        public async Task<ApiResponse> Login(Login model)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                var endpoint = "api/v1/Users/login";

                var apiCredentialAdmin = new Login()
                {
                    Email = model.Email,
                    Password = model.Password,
                };

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);

                var content = new StringContent(JsonConvert.SerializeObject(apiCredentialAdmin), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<AuthResponse>(jsonResponse);
                    if (result != null)
                    {
                        apiResponse.IsSuccess = false;
                        apiResponse.Message = "OK";
                        apiResponse.Result = result;
                    }
                }

            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "Error Service Login" + ex.Message;
            }

            return apiResponse;
        }

        public async Task<ApiResponse> Register(UserRegister model)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                apiResponse = await LoginApiAdmin();

                if (!apiResponse.IsSuccess)
                {
                    return apiResponse;
                }
                
                var authResponse = (AuthResponse)apiResponse.Result!;
                _token = authResponse.Token!;

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_token);

                var endpoint = $"/api/v1/Users/register";

                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "OK";
                }

            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "Error Service Register" + ex.Message;
            }

            return apiResponse;
        }
    }
}
