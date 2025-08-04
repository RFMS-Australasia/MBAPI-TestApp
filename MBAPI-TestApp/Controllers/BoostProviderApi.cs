using MBAPI_TestApp.Services;
using MeasureBoostApi.Interface;
using MeasureBoostApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MBAPI_TestApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoostProviderApi : ControllerBase
    {
        [HttpPost("customer/search")]
        public List<Customer> CustomerSearch(CustomerQuery query)
        {
            var data = new DataAccess();
            return data.CustomerSearch(query.searchString);
        }

        [HttpPost("product/search")]
        public List<Product> ProductSearch(ProductQuery query)
        {
            var data = new DataAccess();
            return data.ProductSearch(query);
        }

        [HttpPost("product/getmultiple")]
        public List<Product> GetProducts(List<string> keys)
        {
            var data = new DataAccess();
            return data.GetProducts(keys);
        }

    }
}
