using Mango.Web.Models;

namespace MangoWeb.Services;

public interface IProductService
{
    
    Task<T?> GetAllProductsAsync<T>();

    Task<T?> GetProductByIdAsync<T>(int id);

    Task<T?> CreateProductAsync<T>(ProductDto productDto);
    
    Task<T?> UpdateProductAsync<T>(ProductDto productDto);

    Task<T?> DeleteProductAsync<T>(int productId);



}