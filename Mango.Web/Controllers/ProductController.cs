using Mango.Web.Models;
using MangoWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
// ReSharper disable once RedundantUsingDirective
using System;

namespace MangoWeb.Controllers;

public class ProductController : Controller
{
    private IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    // GET
    public async Task<IActionResult> ProductIndex()
    {
        List<ProductDto> list = new();
        var response = await _productService.GetAllProductsAsync<ResponseDto>();
        if (response == null) throw new ArgumentNullException(nameof(response));
        if (response.IsSucess)
        {
            list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
        }

        return View(list);
    }
}