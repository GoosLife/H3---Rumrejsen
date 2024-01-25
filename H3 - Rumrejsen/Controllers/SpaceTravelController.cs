using H3___Rumrejsen.Api;
using H3___Rumrejsen.DataAccess;
using H3___Rumrejsen.Models;
using Microsoft.AspNetCore.Mvc;

namespace H3___Rumrejsen.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SpaceTravelController : Controller
    {
        [HttpGet("[action]")]
        public List<GalacticRoute> GetGalacticRoutes()
        {
            if (Request.Headers.TryGetValue("X-API-Key", out var apiKey) && ApiKeyValidator.Validate(apiKey))
            {

                try
                {
                    return new JsonDb().GetRoutes();
                }
                catch (FileNotFoundException e)
                {
                    // Return error 500 if the database file is missing.
                    Response.StatusCode = 500;
                    return new List<GalacticRoute>();
                }
            }
            else
            {
                // Return unauthorized access
                Response.StatusCode = 401;
                return new List<GalacticRoute>();
            }
        }

        [HttpGet("[action]")]
        public GalacticRoute GetGalacticRoute(string name)
        {
            if (Request.Headers.TryGetValue("X-Api-Key", out var apiKey) && ApiKeyValidator.Validate(apiKey))
            {
                try
                {
                    return new JsonDb().GetRoute(name)!;
                }
                catch (FileNotFoundException e)
                {
                    // Return error 500 if the database file is missing.
                    Response.StatusCode = 500;
                    return new GalacticRoute();
                }
            }
            else
            {
                Response.StatusCode = 401;
                return new GalacticRoute();
            }
        }
    }
}
