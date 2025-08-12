using MBAPI_TestApp.Services;
using MeasureBoostApi.Interface;
using MeasureBoostApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MBAPI_TestApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> HandleWebhook()
        {
            try
            {
                // Signature is already verified by middleware, just read the body
                using var reader = new StreamReader(Request.Body);
                var rawBody = await reader.ReadToEndAsync();

                // Get the event type
                if (!Request.Headers.TryGetValue("X-Event-Type", out var eventType))
                {
                    return BadRequest("Missing X-Event-Type header");
                }

                // Route to appropriate handler based on event type
                return eventType.ToString() switch
                {
                    "verify" => HandleVerify(rawBody),
                    "customer.search" => HandleCustomerSearch(rawBody),
                    "customer.save" => HandleCustomerSave(rawBody),
                    "product.search" => HandleProductSearch(rawBody),
                    "product.get" => HandleProductGetMultiple(rawBody),
                    "estimate.save" => HandleEstimateSave(rawBody),
                    _ => BadRequest($"Unknown event type: {eventType}")
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        private IActionResult HandleVerify(string rawBody)
        {
            var result = new VerifyResult() {
                IsSuccess = true,
                Message = "OK"
            };
            return Ok(result);
        }

        private IActionResult HandleCustomerSearch(string rawBody)
        {
            var query = System.Text.Json.JsonSerializer.Deserialize<CustomerQuery>(rawBody);
            if (query == null)
                return BadRequest("Invalid customer query");

            var data = new DataAccess();
            var results = data.CustomerSearch(query.searchString);
            return Ok(results);
        }

        private IActionResult HandleProductSearch(string rawBody)
        {
            var query = System.Text.Json.JsonSerializer.Deserialize<ProductQuery>(rawBody);
            if (query == null)
                return BadRequest("Invalid product query");

            var data = new DataAccess();
            var results = data.ProductSearch(query);
            return Ok(results);
        }

        private IActionResult HandleProductGetMultiple(string rawBody)
        {
            var keys = System.Text.Json.JsonSerializer.Deserialize<List<string>>(rawBody);
            if (keys == null)
                return BadRequest("Invalid product keys");

            var data = new DataAccess();
            var results = data.GetProducts(keys);
            return Ok(results);
        }

        private IActionResult HandleEstimateSave(string rawBody)
        {
            var estimate = System.Text.Json.JsonSerializer.Deserialize<Estimate>(rawBody);
            if (estimate == null)
                return BadRequest("Invalid estimate data");

            var data = new DataAccess();
            var result = data.SaveEstimate(estimate);
            return Ok(result);
        }

        private IActionResult HandleCustomerSave(string rawBody)
        {
            var customer = System.Text.Json.JsonSerializer.Deserialize<Customer>(rawBody);
            if (customer == null)
                return BadRequest("Invalid customer");

            var data = new DataAccess();

            if (customer.Id == 0) {
                var result = data.CreateCustomer(customer);
                if (result == null)
                    return BadRequest("Unable to create customer");
                else
                    return Ok(result);
            } else {
                var result = data.SaveCustomer(customer);
                if (result == null)
                    return BadRequest("Unable to update customer");
                else
                    return Ok(result);
            }
        }

        // Health check endpoint (bypassed by middleware)
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { status = "ok", timestamp = DateTime.UtcNow });
        }
    }
}