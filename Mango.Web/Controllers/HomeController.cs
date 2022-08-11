
using Mango.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MangoWeb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
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
    }
}