using H3___Rumrejsen.DataAccess.Utility;
using H3___Rumrejsen.Models;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace H3___Rumrejsen.DataAccess
{
    public class JsonDb : IDbAccess
    {
        private readonly string Path;
        public JsonDb(string path)
        {
            Path = path;
        }

        public GalacticRoute? GetRoute(string name)
        {
            try
            {
                List<GalacticRoute> allRoutes = GetRoutes();
                return allRoutes.Find(route => route.Name == name) ?? null;
            }
            catch (FileNotFoundException e)
            {
                throw e;
            }
        }

        public List<GalacticRoute> GetRoutes()
        {
            if (FileUtility.FileExists(Path))
            {
                try
                {
                    string jsonString = System.Text.Encoding.UTF8.GetString(FileUtility.GetFileContent(Path)!);
                    return JsonConvert.DeserializeObject<List<GalacticRoute>>(jsonString)!;
                }
                catch (FileNotFoundException e)
                {
                    throw e;
                }
            }
            throw new FileNotFoundException("Database file could not be found.");
        }
    }
}
