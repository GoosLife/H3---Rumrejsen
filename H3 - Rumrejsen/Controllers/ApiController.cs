using Microsoft.AspNetCore.Mvc;
using GoosApiKeyGenerator;
using H3___Rumrejsen.DataAccess;
using GoosLogger;
using H3___Rumrejsen.Api;

namespace H3___Rumrejsen.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ApiController : Controller
    {
        private readonly IConfiguration Configuration;

        public ApiController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet("[action]")]
        public string Get()
        {
            // Check if API key is set in header
            if (Request.Headers.TryGetValue("X-API-Key", out var apiKey))
            {
                try
                {
                    // Check if API key is valid
                    if (ApiKeyValidator.Validate(apiKey))
                    {
                        return "Hello World!";
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(e.Message);
                }
            }

            return ApiKeyGenerator.GenerateApiKey(Configuration.GetValue<int>("ApiKeyLength"));
        }
    }
}
