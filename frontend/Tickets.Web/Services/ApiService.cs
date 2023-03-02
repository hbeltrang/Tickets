using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using Tickets.Web.Models;

namespace Tickets.Web.Services
{
    public class ApiService<T> : IApiService<T> where T : class
    {
        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;

        public ApiService(IHttpClientFactory clientFactory, IUserService userService)
        {
            _userService = userService;
            _httpClient = clientFactory.CreateClient("ApiUrlBase"); //see program.cs
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<ApiResponse> GetAllAsync(string endpoint, List<T> entity)
        {
            ApiResponse apiResponse = new ApiResponse();
            //List<T> list = new List<T>();
            IEnumerable<T> list2;

            try
            {
                var _token = await _userService.GetLoginApiAdminToken();

                _httpClient.DefaultRequestHeaders.Add("Authorization", _token);

                var response = await _httpClient.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    //throw new HttpRequestException(response.StatusCode.ToString() + " " + response.ReasonPhrase);
                    apiResponse = new ApiResponse
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString() + " " + response.ReasonPhrase,
                    };
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    //var result = JsonConvert.DeserializeObject<entity>(jsonResponse);

                    apiResponse = new ApiResponse
                    {
                        IsSuccess = true,
                        Message = "OK",
                        Result = jsonResponse
                    };

                    //list.Add(new T(apiResponse));
                    //list2 = new[] { apiResponse };

                    
                }
            }
            catch (Exception ex)
            {
                apiResponse = new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

            return apiResponse;
        }

        public async Task<T> GetByIdAsync(string endpoint)
        {
            throw new NotImplementedException();
        }



        //public async Task<ApiResponse> GetAllAsync(string endpoint)
        //{
        //    ApiResponse apiResponse = new ApiResponse();

        //    try
        //    {
        //        var _token = await _userService.GetLoginApiAdminToken();

        //        _httpClient.DefaultRequestHeaders.Add("Authorization", _token);

        //        var response = await _httpClient.GetAsync(endpoint);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            //throw new HttpRequestException(response.StatusCode.ToString() + " " + response.ReasonPhrase);
        //            apiResponse = new ApiResponse
        //            {
        //                IsSuccess = false,
        //                Message = response.StatusCode.ToString() + " " + response.ReasonPhrase,
        //            };
        //        }

        //        var jsonResponse = await response.Content.ReadAsStringAsync();
        //        //var result = JsonConvert.DeserializeObject<IReadOnlyList<T>>(jsonResponse);

        //        apiResponse = new ApiResponse
        //        {
        //            IsSuccess = true,
        //            Message = "OK",
        //            Result = jsonResponse
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        apiResponse = new ApiResponse
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message,
        //        };
        //    }

        //    return apiResponse;
        //}

        //public async Task<ApiResponse> GetByIdAsync(string endpoint)
        //{
        //    ApiResponse apiResponse = new ApiResponse();

        //    try
        //    {
        //        var _token = await _userService.GetLoginApiAdminToken();

        //        _httpClient.DefaultRequestHeaders.Add("Authorization", _token);

        //        var response = await _httpClient.GetAsync(endpoint);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            apiResponse = new ApiResponse
        //            {
        //                IsSuccess = false,
        //                Message = response.StatusCode.ToString() + " " + response.ReasonPhrase,
        //            };
        //        }

        //        var jsonResponse = await response.Content.ReadAsStringAsync();

        //        apiResponse = new ApiResponse
        //        {
        //            IsSuccess = true,
        //            Message = "OK",
        //            Result = jsonResponse
        //        };

        //    }
        //    catch (Exception ex)
        //    {
        //        apiResponse = new ApiResponse
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message,
        //        };
        //    }

        //    return apiResponse;
        //}

        public async Task<ApiResponse> CreateAsync(string endpoint, T model)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                var _token = await _userService.GetLoginApiAdminToken();

                _httpClient.DefaultRequestHeaders.Add("Authorization", _token);

                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    apiResponse = new ApiResponse
                    {
                        IsSuccess = true,
                        Message = "OK",
                        Result = jsonResponse
                    };
                }
                else
                {
                    apiResponse = new ApiResponse
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString() + " " + response.ReasonPhrase,
                    };
                }
            }
            catch (Exception ex)
            {
                apiResponse = new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

            return apiResponse;
        }

            //public async Task<ApiResponse> UpdateAsync(string endpoint, object model)
            //{
            //    ApiResponse apiResponse = new ApiResponse();

            //    try
            //    {
            //        var _token = await _userService.GetLoginApiAdminToken();

            //        _httpClient.DefaultRequestHeaders.Add("Authorization", _token);

            //        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            //        var response = await _httpClient.PutAsync(endpoint, content);
            //        if (response.IsSuccessStatusCode)
            //        {
            //            var jsonResponse = await response.Content.ReadAsStringAsync();

            //            apiResponse = new ApiResponse
            //            {
            //                IsSuccess = true,
            //                Message = "OK",
            //                Result = jsonResponse
            //            };
            //        }
            //        else
            //        {
            //            apiResponse = new ApiResponse
            //            {
            //                IsSuccess = false,
            //                Message = response.StatusCode.ToString() + " " + response.ReasonPhrase,
            //            };
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        apiResponse = new ApiResponse
            //        {
            //            IsSuccess = false,
            //            Message = ex.Message,
            //        };
            //    }

            //    return apiResponse;
            //}

            //public async Task<ApiResponse> DeleteAsync(string endpoint)
            //{
            //    ApiResponse apiResponse = new ApiResponse();

            //    try
            //    {
            //        var _token = await _userService.GetLoginApiAdminToken();

            //        _httpClient.DefaultRequestHeaders.Add("Authorization", _token);

            //        var response = await _httpClient.DeleteAsync(endpoint);
            //        if (response.IsSuccessStatusCode)
            //        {
            //            var jsonResponse = await response.Content.ReadAsStringAsync();

            //            apiResponse = new ApiResponse
            //            {
            //                IsSuccess = true,
            //                Message = "OK",
            //                Result = jsonResponse
            //            };
            //        }
            //        else
            //        {
            //            apiResponse = new ApiResponse
            //            {
            //                IsSuccess = false,
            //                Message = response.StatusCode.ToString() + " " + response.ReasonPhrase,
            //            };
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        apiResponse = new ApiResponse
            //        {
            //            IsSuccess = false,
            //            Message = ex.Message,
            //        };
            //    }

            //    return apiResponse;
            //}




        }
}
