using FrontEnd.Models;
using FrontEnd.Utility;
using Newtonsoft.Json;
using System.Text;
using static FrontEnd.Utility.SD;
using System.Net;

namespace FrontEnd.Services.IServices
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public  async Task<ResponseDTO>? SendAsync(RequestDTO requestDTO)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("MangoAPI");
            HttpRequestMessage msg = new();
            msg.Method = HttpMethod.Post;
            msg.Headers.Add("Accept", "application/json");
            //Token

            msg.RequestUri = new Uri(requestDTO.URL);

            if (requestDTO.Data != null)
            {
                msg.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data),Encoding.UTF8, "application/json"); 
            }
            HttpResponseMessage? apiResponse = null;
            switch (requestDTO.ApiType)
            {
                case ApiType.POST:
                    msg.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    msg.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    msg.Method = HttpMethod.Delete;
                    break;
                default:
                    msg.Method = HttpMethod.Get;
                    break;
            }
            apiResponse =  await httpClient.SendAsync(msg);

            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new() { IsSuccess = false, ErrorMessage= "Not Found" };
                case HttpStatusCode.Forbidden:
                    return new() { IsSuccess = false, ErrorMessage = "Forbidden" };
                case HttpStatusCode.Unauthorized:
                    return new() { IsSuccess = false, ErrorMessage = "Un Authorized" };
                case HttpStatusCode.InternalServerError:
                    return new() { IsSuccess = false, ErrorMessage = "Internal Server Error" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                    return apiResponseDTO;
            }
        }
    }
}
