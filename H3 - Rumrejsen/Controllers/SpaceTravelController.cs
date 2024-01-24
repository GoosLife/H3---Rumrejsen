using H3___Rumrejsen.DataAccess;
using H3___Rumrejsen.Models;
using Microsoft.AspNetCore.Mvc;

namespace H3___Rumrejsen.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SpaceTravelController : Controller
    {
        [HttpGet(Name = "GetSpaceTravel")]
        public List<GalacticRoute> GetGalacticRoutes()
        {
            return new JsonDb("DataAccess/Database/SpaceTravel.json").GetRoutes();
        }
    }
}
