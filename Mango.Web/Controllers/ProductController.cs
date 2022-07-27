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
    
    public async Task<IActionResult> ProductIndex()
    {
        List<ProductDto>? list = new List<ProductDto>();
        var response = await _productService.GetAllProductsAsync<ResponseDto>();
        ResponseDto responseDto = (ResponseDto)response;
        var jsonString = Convert.ToString(responseDto.Result);
        if (responseDto != null && responseDto.IsSucess)
        {
          list = JsonConvert.DeserializeObject<List<ProductDto>>(jsonString);
        }

        return View(list);
    }
    public async Task<IActionResult> ProductCreate()
    {
        return View();
    }
    public async Task<IActionResult> ProductCreate(ProductDto productDto)
    {
        object model = null;
        if (ModelState.IsValid)
        {
            model = await _productService.CreateProductAsync<ResponseDto>(productDto);
            ResponseDto responseDto =   (ResponseDto)model;
            if (responseDto != null && responseDto.IsSucess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }
        return View(model);
    }

    public async Task<IActionResult> ProductEdit(int productId)
    {
      var  response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            ResponseDto responseDto =   (ResponseDto)response;
            ProductDto productDto = new ProductDto();
            if (responseDto != null && responseDto.IsSucess)
            {
              productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
              return View(productDto);
            }

            return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductEdit(ProductDto productDto)
    {
        object model = null;
        if (ModelState.IsValid)
        {
            ResponseDto responseDto = await _productService.UpdateProductAsync<ResponseDto>(productDto) as ResponseDto;
            if (responseDto != null && responseDto.IsSucess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }
        return View(productDto);
    }

    public IActionResult Delete()
    {
        throw new NotImplementedException();
    }
}