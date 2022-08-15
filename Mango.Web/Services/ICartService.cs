using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Web.Services;

public interface ICartService
{
    public Task<object> GetCartByUserId<T>(string userID, string token = null);
    public Task<object> AddToCart<T>(CartDto cartDto, string token = null);
    public Task<object> UpdateAsync<T>(CartDto cartDto, string token = null);
    public Task<object> DeleteAsync<T>(int cartId, string token = null);
}