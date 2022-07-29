using Mango.Web.Models;

namespace MangoWeb.Services;

public interface IProductService
{
    
    Task<object> GetAllProductsAsync<T>();

    Task<object> GetProductByIdAsync<T>(int id);
    //
    Task<object> CreateProductAsync<T>(ProductDto productDto);
    //
    Task<object> UpdateProductAsync<T>(ProductDto productDto);
    //
    Task<object?> DeleteProductAsync<T>(int productId);



}