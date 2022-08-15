
using Mango.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Web.Services;
using MangoWeb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using ProductDto = Mango.Web.Models.ProductDto;
using ResponseDto = Mango.Web.Models.ResponseDto;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
        List<ProductDto>? list = new List<ProductDto>();
            var response = (ResponseDto)await _productService.GetAllProductsAsync<ResponseDto>("");
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (response.IsSucess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oicd");
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(@"https://localhost:44365/account/login");
            //HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            //HttpResponseMessage httpResponse = await client.GetAsync(@"https://localhost:44365/Login");
            //return Ok();
            //Get the token from the Identity project
            var tokenAsync = HttpContext.GetTokenAsync("access token");
            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int productId)
        {
            ProductDto productDto = new ProductDto();
            ResponseDto response = (ResponseDto)await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (response.IsSucess)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }

            return View(productDto);
            
        }

        [HttpPost]
        [ActionName("Details")]
        public async Task<IActionResult> DetailsPost(ProductDto productDto)
        {
            // ProductDto model = new ProductDto();
            // ResponseDto response = (ResponseDto)await _productService.GetProductByIdAsync<ResponseDto>(productId);
            // if (response == null) throw new ArgumentNullException(nameof(response));
            // if (response.IsSucess)
            // {
            //     productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            // }
            CartDto cartDto = new() 
            {
            CartHeader = new CartHeaderDto
            {
                UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
            }};
            CartDetailsDto cartDetails = new CartDetailsDto()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId
            };
            var resp = (ResponseDto)await _productService.GetProductByIdAsync<ResponseDto>(productDto.ProductId, "");
            if (resp == null) throw new ArgumentNullException(nameof(resp));
            if (resp.IsSucess)
            {
                cartDetails.Product =
                    JsonConvert.DeserializeObject<Mango.Services.ShoppingCartAPI.Models.Dto.ProductDto>(
                        Convert.ToString(resp));
            }
            
            List<CartDetailsDto> cartDetailsDtos = new();
            cartDetailsDtos.Add(cartDetails);
            cartDto.CartDetails = cartDetailsDtos;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            ResponseDto addToCartResp = (ResponseDto)await _cartService.AddToCart<ResponseDto>(cartDto, accessToken);
            if (addToCartResp != null && addToCartResp.IsSucess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

    }
}