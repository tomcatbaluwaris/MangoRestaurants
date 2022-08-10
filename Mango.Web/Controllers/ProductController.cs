using Mango.Web.Models;
using MangoWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
// ReSharper disable once RedundantUsingDirective
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

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
        var token = await GetToken();
        List<ProductDto>? list = new List<ProductDto>();
        var response = await _productService.GetAllProductsAsync<ResponseDto>(token);
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
            var token = await HttpContext.GetTokenAsync("access token");
            model = await _productService.CreateProductAsync<ResponseDto>(productDto, token);
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
        var token = await GetToken();
      var  response = await _productService.GetProductByIdAsync<ResponseDto>(productId, token);
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
            var token = await GetToken();
            ResponseDto responseDto = await _productService.UpdateProductAsync<ResponseDto>(productDto, token) as ResponseDto;
            if (responseDto != null && responseDto.IsSucess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }
        return View(productDto);
    }

    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ProductDelete(int productId)
    {
        var  response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
        var token = await GetToken();
        ResponseDto responseDto =   (ResponseDto)response;
        ProductDto productDto = new ProductDto();
        if (responseDto != null && responseDto.IsSucess)
        {
            productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
            return View(productDto);
        }

        return NotFound();
    }

    // [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductDelete(ProductDto productDto)
    {
    // try
        // {
            if (productDto.ProductId > 0)
            {
                var token = await GetToken();
                ResponseDto? responseDto = (await _productService.DeleteProductAsync<ResponseDto>(productDto.ProductId, token)) as ResponseDto;
                if (responseDto != null && responseDto.IsSucess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(productDto);
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e);
        //     throw;
        // }

        return NotFound();

    }


    public async Task<string?> GetToken()
    {
        string? token = await HttpContext.GetTokenAsync("access token");
        if (token != null) 
        {return token;}

        return null;
    }
    
}