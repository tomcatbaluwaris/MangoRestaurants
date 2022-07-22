using System.Net;
using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository;

public class ProductRepository: IProductRepository
{
    private ApplicationDbContext _applicationDbContext;

    private IMapper _mapper;

    public ProductRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var products = await _applicationDbContext.Products.ToListAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductById(int id)
    {
     return   _mapper.Map<ProductDto>(await _applicationDbContext.Products.FirstOrDefaultAsync(item => item.ProductId == id));
    }

    public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
    {
        var product = _mapper.Map<ProductDto, Product>(productDto);
        if (productDto.ProductId > 0)
        {
            _applicationDbContext.Update(product);
        }
        else
        {
            _applicationDbContext.Products.Add(product);
        }

        await _applicationDbContext.SaveChangesAsync();
        return _mapper.Map<Product, ProductDto>(product);
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        try
        {
            var product = await _applicationDbContext.Products.FirstOrDefaultAsync(item => item.ProductId == productId);
            if (product == null) return false;

            _applicationDbContext.Remove(product);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
       

    }
}