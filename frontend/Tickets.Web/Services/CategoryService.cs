using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Tickets.Web.Areas.Admin.ViewModels;
using Tickets.Web.Models;

namespace Tickets.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private static string _apiUser;
        private static string _apiPwd;
        private static string _apiURL;
        private static string _token;

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public CategoryService(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;

            _apiUser = configuration.GetValue<string>("ApiSettings:ApiUser");
            _apiPwd = configuration.GetValue<string>("ApiSettings:ApiPwd");
            _apiURL = configuration.GetValue<string>("ApiSettings:ApiURL");
            
        }        

        public async Task<ApiResponse> List()
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                apiResponse = await _userService.LoginApiAdmin();

                if (!apiResponse.IsSuccess)
                {
                    return apiResponse;
                }

                var authResponse = (AuthResponse)apiResponse.Result!;
                _token = authResponse.Token!;

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_token);

                var endpoint = "/api/v1/categories";

                var response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<CategoryVm>>(jsonResponse);

                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "OK";
                    apiResponse.Result = result.ToList();
                }
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
            }
            
            return apiResponse;
        }

        public async Task<ApiResponse> GetById(int id)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                apiResponse = await _userService.LoginApiAdmin();

                if (!apiResponse.IsSuccess)
                {
                    return apiResponse;
                }

                var authResponse = (AuthResponse)apiResponse.Result!;
                _token = authResponse.Token!;

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_token);

                var endpoint = $"/api/v1/categories/{id}";

                var response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CategoryVm>(jsonResponse);

                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "OK";
                    apiResponse.Result = result;
                }
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
            }
            
            return apiResponse;
        }

        public async Task<ApiResponse> Save(CategoryVm model)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                apiResponse = await _userService.LoginApiAdmin();

                if (!apiResponse.IsSuccess)
                {
                    return apiResponse;
                }

                var authResponse = (AuthResponse)apiResponse.Result!;
                _token = authResponse.Token!;

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_token);

                var endpoint = $"/api/v1/categories";

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
                apiResponse.Message = ex.Message;
            }
            
            return apiResponse;
        }

        public async Task<ApiResponse> Edit(CategoryVm model)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                apiResponse = await _userService.LoginApiAdmin();

                if (!apiResponse.IsSuccess)
                {
                    return apiResponse;
                }

                var authResponse = (AuthResponse)apiResponse.Result!;
                _token = authResponse.Token!;

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_token);

                var endpoint = $"/api/v1/categories";

                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "OK";
                }
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
            }

            
            return apiResponse;
        }

        public async Task<ApiResponse> Delete(int id)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                apiResponse = await _userService.LoginApiAdmin();

                if (!apiResponse.IsSuccess)
                {
                    return apiResponse;
                }

                var authResponse = (AuthResponse)apiResponse.Result!;
                _token = authResponse.Token!;

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_token);

                var endpoint = $"/api/v1/categories/{id}";

                var response = await httpClient.DeleteAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "OK";
                }
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
            }


            return apiResponse;
        }
    }
}
