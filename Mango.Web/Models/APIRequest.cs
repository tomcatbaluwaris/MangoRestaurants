namespace Mango.Web.Models;

public class ApiRequest
{
    public SD.ApiType ApiType { get; set; } = SD.ApiType.Get;

    public string Url { get; set; }
    
    public string AccessToken { get; set; }

    public object Data { get; set; }
    
    
}