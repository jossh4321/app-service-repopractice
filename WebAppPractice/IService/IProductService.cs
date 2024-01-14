using WebAppPractice.Model;

namespace WebAppPractice.IService
{
    public interface IProductService
    {
        public List<Product> GetAllProducts();
        public String GetSlogan();

        public Task<bool> IsFeatureFlagEnabled(string featureFlag);
    }
}
