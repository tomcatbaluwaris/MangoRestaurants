using Mango.Web;
using Mango.Web.Models;

namespace MangoWeb.Services;

public class ProductService : BaseService, IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductService(IHttpClientFactory httpClientFactory):base(httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    
    public async Task<object> GetAllProductsAsync<T>()
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.Get,
            Url = SD.ProductApiBase + "/api/products",
            AccessToken = ""
        });
    }
    
    public async Task<object> GetProductByIdAsync<T>(int id)
    {
        return await this.SendAsync<object>(new ApiRequest()
        {
            ApiType = SD.ApiType.Get,
            Url = SD.ProductApiBase + "/api/products/"+id,
            AccessToken = ""
        });
    }
    
    public async Task<object> CreateProductAsync<T>(ProductDto productDto)
    {
        var apiProducts = "/api/products";
        return await SendAsync<T>(new ApiRequest() 
            {
                ApiType = SD.ApiType.Post,
                Data = productDto,
                Url = $"{SD.ProductApiBase}{apiProducts}",
                AccessToken = ""
            
            }
        );
    }
    
    public async Task<object> UpdateProductAsync<T>(ProductDto productDto)
    {
        var apiProducts = "/api/products";
        return await SendAsync<T>(new ApiRequest() 
            {
                ApiType = SD.ApiType.Post,
                Data = productDto,
                Url = $"{SD.ProductApiBase}{apiProducts}",
                AccessToken = ""
            
            }
        );
    }
    //
    // public async Task<T> DeleteProductAsync<T>(int productId)
    // {
    //     var apiProducts = "/api/products/";
    //     return await SendAsync<T>(new ApiRequest() 
    //         {
    //             ApiType = SD.ApiType.Delete,
    //             Url = $"{SD.ProductApiBase}{apiProducts}{productId}",
    //             AccessToken = ""
    //             
    //         }
    //     );
    // }
}