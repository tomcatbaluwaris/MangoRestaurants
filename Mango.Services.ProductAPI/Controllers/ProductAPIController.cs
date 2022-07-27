using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers;

[Route("api/products")]
public class ProductAPIController : ControllerBase
{

    public IProductRepository productRepository;
    private readonly ResponseDto _response;

    public ProductAPIController(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
        _response = new ResponseDto();
    }

    // GET
    [HttpGet]
    public async Task<object> Get()
    {
        try
        {
            var products = await productRepository.GetProducts();
            _response.Result = products;
            return _response;
        }
        catch (Exception e)
        {
            _response.IsSucess = false;
            _response.ErrorMessages =
                new List<string>()
                {
                    e.Message.ToString()
                };
            Console.WriteLine(e);
            throw;
        }

    }

    [HttpGet]
    [Route("{id}")]
    public async Task<object> GetProductById(int id)
    {
        try
        {
            var product = await productRepository.GetProductById(id);
            if (product == null) throw new ArgumentNullException(nameof(product));
            _response.Result = product;
            return _response;
        }
        catch (Exception e)
        {
            _response.IsSucess = false;
            _response.ErrorMessages =
                new List<string>()
                {
                    e.Message.ToString()
                };
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut]
    public async Task<object> Put(ProductDto product)
    {
        try
        {
            var updateProduct = await productRepository.CreateUpdateProduct(product);
            if (updateProduct == null) throw new ArgumentNullException(nameof(product));
            _response.Result = updateProduct;
            return _response;
        }
        catch (Exception e)
        {
            _response.IsSucess = false;
            _response.ErrorMessages =
                new List<string>()
                {
                    e.Message.ToString()
                };
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<object> Post([FromBody]  ProductDto productDto)
    {
        try
        {
            var product = await productRepository.CreateUpdateProduct(productDto);
            if (product == null) throw new ArgumentNullException(nameof(product));
            _response.Result = product;
            return _response;
        }
        catch (Exception e)
        {
            _response.IsSucess = false;
            _response.ErrorMessages =
                new List<string>()
                {
                    e.Message.ToString()
                };
            Console.WriteLine(e);
            throw;
        }

    }

        [HttpDelete]
        public async Task<object> Delete(ProductDto productDto)
        {
            try
            {
                var isDeleteProduct = await productRepository.DeleteProduct(productDto.ProductId);
                _response.Result = isDeleteProduct;
                return _response;
            }
            catch (Exception e)
            {
                _response.IsSucess = false;
                _response.ErrorMessages =
                    new List<string>()
                    {
                        e.Message.ToString()
                    };
                Console.WriteLine(e);
                throw;
            }
        }
        
    
}


