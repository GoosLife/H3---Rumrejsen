using H3___Rumrejsen.DataAccess;
using H3___Rumrejsen.Utility;
using Newtonsoft.Json.Linq;
using GoosLogger;

namespace H3___Rumrejsen.Api
{
    public static class ApiKeyValidator
    {
        private static JsonDb db;

        public static void Init()
        {
            db = new JsonDb();
        }

        public static bool Validate(string key)
        {
            try
            {
                var apiKey = db.GetApiKey(key);

                if (apiKey == null)
                {
                    return false;
                }

                return apiKey.IsValid();
            }
            catch (FileNotFoundException e)
            {
                Logger.Log(e.Message);
                return false;
            }
            catch { throw; }
        }
    }
}
