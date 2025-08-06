using MBAPI_TestApp.Services;
using MeasureBoostApi.Interface;
using MeasureBoostApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MBAPI_TestApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() {
            var data = new DataAccess();
            var estimate = data.GetLastEstimateReceived();
            return Ok(estimate == null ? "No estimate received yet" : estimate);
        }
    }
}
