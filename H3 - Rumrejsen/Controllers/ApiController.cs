using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

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
                // Check if API key is valid
                if (apiKey == Configuration.GetValue<string>("ApiKey"))
                {
                    return "Hello World!";
                }
            }

            return GenerateApiKey();
        }

        private string GenerateApiKey()
        {
            var bytes = RandomNumberGenerator.GetBytes(Configuration.GetValue<int>("ApiKeyLength"));

            string base64String = Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_");

            var keyLength = 32 - "RR-".Length;

            return "RR-" + base64String[..keyLength];
        }

    }
}
