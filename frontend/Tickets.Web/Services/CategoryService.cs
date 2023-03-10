using Newtonsoft.Json;
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

        public async Task<ApiResponse> GetAll()
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                _token = await _userService.GetLoginApiAdminToken();

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);
                //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", _token);
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", _token);

                var endpoint = "api/v1/categories";

                var response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<CategoryVm>>(jsonResponse);

                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "OK";
                    apiResponse.Result = result.ToList();
                }
                else
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = "Error categories: " + response.ReasonPhrase!;
                }
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            
            return apiResponse;
        }

        public async Task<ApiResponse> GetById(int id)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                _token = await _userService.GetLoginApiAdminToken();

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);
                httpClient.DefaultRequestHeaders.Add("Authorization", _token);

                var endpoint = $"api/v1/categories/{id}";

                var response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CategoryVm>(jsonResponse);

                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "OK";
                    apiResponse.Result = result;
                }
                else
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
            }
            
            return apiResponse;
        }

        public async Task<ApiResponse> Create(CategoryVm model)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                _token = await _userService.GetLoginApiAdminToken();

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);
                httpClient.DefaultRequestHeaders.Add("Authorization", _token);

                var endpoint = "api/v1/categories/create";

                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "OK";
                }
                else
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
            }
            
            return apiResponse;
        }

        public async Task<ApiResponse> Update(CategoryVm model)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                _token = await _userService.GetLoginApiAdminToken();

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);
                httpClient.DefaultRequestHeaders.Add("Authorization", _token);

                var endpoint = "api/v1/categories/update";

                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "OK";
                }
                else
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = response.ReasonPhrase;
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
                _token = await _userService.GetLoginApiAdminToken();

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_apiURL);
                httpClient.DefaultRequestHeaders.Add("Authorization", _token);

                var endpoint = $"api/v1/categories/{id}";

                var response = await httpClient.DeleteAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "OK";
                }
                else
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = response.ReasonPhrase;
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
