using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Web.Models;
using MangoWeb.Services;

namespace Mango.Web.Services;

public class CartService : BaseService, ICartService
{
    private readonly IHttpClientFactory _httpClient;
    
    public CartService(IHttpClientFactory httpClient) : base(httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<object> GetCartByUserId<T>(string userID, string token = null)
    {
        return await this.SendAsync<object>(new ApiRequest()
        {
            ApiType = SD.ApiType.Get,
            Url = SD.ProductApiBase + "/api/cart/"+userID,
            AccessToken = token
        });
    }

    public async Task<object> AddToCart<T>(CartDto cartDto, string token = null)
    {
        return await this.SendAsync<object>(new ApiRequest()
        {
            ApiType = SD.ApiType.Post,
            Url = SD.ProductApiBase + "/api/cart/",
            Data = cartDto,
            AccessToken = token
        });
    }

    public async Task<object> UpdateAsync<T>(CartDto cartDto, string token = null)
    {
        var apiCart = "/api/cart/";
        return await SendAsync<T>(new ApiRequest() 
            {
                ApiType = SD.ApiType.Post,
                Data = cartDto,
                Url = $"{SD.ProductApiBase}{apiCart}",
                AccessToken = token
            
            }
        );
    }

    public async Task<object> DeleteAsync<T>(int cartId, string token = null)
    {
        var apiCart = "/api/cart/";
        return await SendAsync<T>(new ApiRequest() 
            {
                ApiType = SD.ApiType.Delete,
                Url = $"{SD.ProductApiBase}{apiCart}",
                Data = cartId,
                AccessToken = token
            }
        );
    }

  
}