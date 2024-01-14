using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppPractice.IService;
using WebAppPractice.Model;
using WebAppPractice.Services;

namespace WebAppPractice.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService productService;
        public List<Product> productList;
        public string slogan;
        public bool featureFlag;

        public IndexModel(IProductService service)
        {
            this.productService = service;
        }

        public async Task OnGet()
        {
            featureFlag = await productService.IsFeatureFlagEnabled("Beta-Features");
            slogan = productService.GetSlogan();
            productList = productService.GetAllProducts();
        }
    }
}