using Mango.Web.Models;

namespace MangoWeb.Services;

public interface IBaseService: IDisposable
{
    public ResponseDto ResponseModel { get; set; }
    public Task<T?> SendAsync<T>(ApiRequest apiRequest);
}