using H3___Rumrejsen.Models;

namespace H3___Rumrejsen.DataAccess
{
    public interface IDbAccess
    {
        public GalacticRoute GetRoute(string name);
        public List<GalacticRoute> GetRoutes();
    }
}
