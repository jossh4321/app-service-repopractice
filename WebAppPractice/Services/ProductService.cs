using Microsoft.FeatureManagement;
using System.Data.SqlClient;
using WebAppPractice.IService;
using WebAppPractice.Model;

namespace WebAppPractice.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        private readonly IFeatureManager _feature;

        public ProductService(IConfiguration config, IFeatureManager feat)
        {
            _configuration = config;
            _feature = feat;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration["SqlConnectionString"]);
        }

        public String GetSlogan()
        {
            return _configuration["slogan"];
        }

        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();
            SqlConnection connection = GetConnection();
            connection.Open();
            string query = "SELECT ProductID, ProductName, Quantity FROM Products";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productList.Add(new Product()
                {
                    ProductID = reader.GetString(0),
                    ProductName = reader.GetString(1),
                    Quantity = reader.GetInt32(2)
                });
            }
            connection.Close();
            return productList;
        }

        public async Task<bool> IsFeatureFlagEnabled(string featureFlag)
        {
            if (await _feature.IsEnabledAsync(featureFlag))
            {
                return true;
            }
            return false;
        }
    }
}
