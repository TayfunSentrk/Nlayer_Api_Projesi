using Microsoft.AspNetCore.Mvc;
using Nlayer.Core.Services;

namespace Nlayer.Web.Controllers
{
    /// <summary>
    /// Ürünlerle ilgili işlemleri yöneten kontrolcü.
    /// </summary>
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        /// <summary>
        /// <see cref="ProductsController"/> sınıfının yeni bir örneğini başlatır.
        /// </summary>
        /// <param name="productService">Ürün servis örneği.</param>
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Ürünlerin ve kategorilerinin listesini görüntüler.
        /// </summary>
        /// <returns>Ürünlerin ve kategorilerinin listesi ile görünümü döner.</returns>
        public async Task<IActionResult> Index()
        {
            var response = await productService.GetProductsWithCategoryAsync();
            return View(response.Data);
        }
    }

}
