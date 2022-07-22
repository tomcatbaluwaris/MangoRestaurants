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
        ResponseDto = new ResponseDto();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }

    public ResponseDto ResponseModel { get; set; }

    public IHttpClientFactory HttpClient { get; set; }
    public ResponseDto ResponseDto { get; }

    public async Task<T> SendAsync<T>(ApiRequest apiRequest)
    {
        try
        {
            var httpClient = HttpClient.CreateClient("MangoApi");
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Headers.Add("accept","application/json");
            requestMessage.RequestUri = new Uri(apiRequest.Url);
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
            var deserializeObject = JsonConvert.DeserializeObject<T>(content);
            return deserializeObject;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
    }
