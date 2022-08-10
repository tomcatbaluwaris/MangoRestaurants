using Mango.Web.Models;

namespace MangoWeb.Services;

public interface IProductService
{
    
    Task<object> GetAllProductsAsync<T>(string token = "");

    Task<object> GetProductByIdAsync<T>(int id, string token = "");
    //
    Task<object> CreateProductAsync<T>(ProductDto productDto, string token = "");
    //
    Task<object> UpdateProductAsync<T>(ProductDto productDto, string token = "");
    //
    Task<object?> DeleteProductAsync<T>(int productId, string token = "");



}