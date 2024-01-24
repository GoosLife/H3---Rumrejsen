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
        public List<GalacticRoute> GetGalacticRoutes(string apiKey)
        {
            JsonDb db = new JsonDb();

            if (db.ValidateApiKey(apiKey))
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
    }
}
