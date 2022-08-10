using System.Net.Http.Headers;
using System.Text;
using Mango.Web;
using Mango.Web.Models;
using Newtonsoft.Json;

namespace MangoWeb.Services;

public class BaseService : IBaseService, IDisposable
{
    public BaseService(IHttpClientFactory httpClient)
    {
        HttpClient = httpClient;
        _responseDto = new ResponseDto();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }

    public ResponseDto ResponseModel { get; set; }

    public IHttpClientFactory HttpClient { get; set; }

    private ResponseDto _responseDto;

    public async Task<object> SendAsync<T>(ApiRequest apiRequest)
    {
        string? apiRequestAccessToken = null;
        try
        {
            var httpClient = HttpClient.CreateClient("MangoApi");
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Headers.Add("accept","application/json");
            requestMessage.RequestUri = new Uri(apiRequest.Url);
            httpClient.DefaultRequestHeaders.Clear();
            if (apiRequest.Data != null)
            {
               
                if (apiRequest.Data != null)
                {
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
            }


            if (!string.IsNullOrEmpty(apiRequestAccessToken))
            {
                apiRequestAccessToken = apiRequest.AccessToken;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(apiRequestAccessToken);
            }
            HttpResponseMessage response = null;
            switch (apiRequest.ApiType)
            {
            
                case SD.ApiType.Post:
                    requestMessage.Method = HttpMethod.Post;
                    break;
                case SD.ApiType.Put:
                    requestMessage.Method = HttpMethod.Put;
                    break;
                case SD.ApiType.Delete:
                    requestMessage.Method = HttpMethod.Delete;
                    break;
                default:
                    requestMessage.Method = HttpMethod.Get;
                    break;
            };
            response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            _responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if(response != null)
            {
                _responseDto.IsSucess = true;
            }
            return _responseDto;
        }
        catch (Exception e)
        {
            var dto = new ResponseDto
            {
                DisplayMessage = "Error",
                ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                IsSucess = false
            };
            var res = JsonConvert.SerializeObject(dto);
            var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
            return apiResponseDto;
          
        }
    }
    }
