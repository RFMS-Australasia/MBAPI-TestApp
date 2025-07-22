using MBAPI_TestApp.Services;
using MeasureBoostApi.Interface;
using MeasureBoostApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MBAPI_TestApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoostApiReceiver : ControllerBase
    {
        [HttpPost("customer/search")]
        public List<Customer> CustomerSearch(CustomerQuery query)
        {
            var data = new DataAccess();
            return data.CustomerSearch(query.searchString);
        }




    }
}
