using Mango.Web.Models;

namespace MangoWeb.Services;

public interface IBaseService: IDisposable
{
    public ResponseDto ResponseModel { get; set; }
    public Task<object> SendAsync<T>(ApiRequest apiRequest);
}