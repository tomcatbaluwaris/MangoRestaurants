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
        try
        {
            var httpClient = HttpClient.CreateClient("MangoApi");
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Headers.Add("accept","application/json");
            requestMessage.RequestUri = new Uri(apiRequest.Url);
            httpClient.DefaultRequestHeaders.Clear();
            if (apiRequest.Data != null)
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8,"application/json");
            }

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
            var response = httpClient.SendAsync(requestMessage).Result;
            var content = await response.Content.ReadAsStringAsync();
            var deserializeObject = JsonConvert.DeserializeObject<object>(content);
            _responseDto.Result  = deserializeObject;
            if(_responseDto.Result != null)
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
