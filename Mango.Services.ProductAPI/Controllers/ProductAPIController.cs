using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers;

[Route("api/products")]
public class ProductAPIController : ControllerBase
{
    public ResponseDto response;

    public IProductRepository productRepository;

    public ProductAPIController(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
        this.response = new ResponseDto();
    }

    // GET
    [HttpGet]
    public async Task<object> Get()
    {
        try
        {
            var products = await productRepository.GetProducts();
            this.response.Result = products;
            return this.response.Result;
        }
        catch (Exception e)
        {
            this.response.IsSucess = false;
            this.response.ErrorMessages =
                new List<string>()
                {
                    e.Message.ToString()
                };
            Console.WriteLine(e);
            throw;
        }

    }

    [HttpGet]
    [Route("id")]
    public async Task<object> GetProductById(int id)
    {
        try
        {
            var product = await productRepository.GetProductById(id);
            if (product == null) throw new ArgumentNullException(nameof(product));
            this.response.Result = product;
            return this.response.Result;
        }
        catch (Exception e)
        {
            this.response.IsSucess = false;
            this.response.ErrorMessages =
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
            this.response.Result = updateProduct;
            return this.response.Result;
        }
        catch (Exception e)
        {
            this.response.IsSucess = false;
            this.response.ErrorMessages =
                new List<string>()
                {
                    e.Message.ToString()
                };
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<object> Post(ProductDto productDto)
    {
        try
        {
            var product = await productRepository.CreateUpdateProduct(productDto);
            if (product == null) throw new ArgumentNullException(nameof(product));
            this.response.Result = product;
            return this.response.Result;
        }
        catch (Exception e)
        {
            this.response.IsSucess = false;
            this.response.ErrorMessages =
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
                this.response.Result = isDeleteProduct;
                return this.response.Result;
            }
            catch (Exception e)
            {
                this.response.IsSucess = false;
                this.response.ErrorMessages =
                    new List<string>()
                    {
                        e.Message.ToString()
                    };
                Console.WriteLine(e);
                throw;
            }
        }
        
    
}


