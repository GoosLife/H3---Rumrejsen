using H3___Rumrejsen.Models;
using Newtonsoft.Json.Linq;
using GoosLogger;
using H3___Rumrejsen.Utility;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Reflection;

namespace H3___Rumrejsen.DataAccess
{
    public class JsonDb : IDbAccess
    {
        private readonly string Path;
        private List<ApiKey> ApiKeys;

        public JsonDb()
        {
            // I still don't know how to get this folder in a more intuitive way.
            string rootFolder = System.IO.Directory.GetCurrentDirectory();

            Path = System.IO.Path.Combine(rootFolder, "Database\\galacticRoutes.json");

            PopulateApiKeys();
        }

        /// <summary>
        /// Create an instance of JsonDb pointing to a specific file.
        /// 
        /// Only use this if you have placed the database file in a different location than the default.
        /// Otherwise use the default constructor.
        /// 
        /// The default path is: "DataAccess/Database.json"
        /// </summary>
        /// <param name="path">The full path to the database file.</param>
        public JsonDb(string path)
        {
            Path = path;

            ApiKeys = new List<ApiKey>();
            JToken allKeys = GetApiKeys();

            PopulateApiKeys();
        }

        private void PopulateApiKeys()
        {
            ApiKeys = new List<ApiKey>();
            JToken allKeys = GetApiKeys();

            foreach (JToken apiKey in allKeys)
            {
                if (apiKey["type"]!.ToString() == "cadet")
                {
                    ApiKeys!.Add(apiKey.ToObject<CadetKey>()!);
                }
                else
                {
                    ApiKeys!.Add(apiKey.ToObject<ApiKey>()!);
                }
            }
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

                    // Get the galacticRoutes array from the JSON file, then deserialize it into a list of GalacticRoute objects.
                    JObject jsonObject = JObject.Parse(jsonString);

                    try
                    {
                        JToken jsonArray = jsonObject["galacticRoutes"];
                        return jsonArray.ToObject<List<GalacticRoute>>()!;
                    }
                    catch (Exception e)
                    {
                        Logger.Log(e.Message);
                        throw new InvalidDataException("No galactic routes found in database file.");
                    }

                }
                catch (FileNotFoundException e)
                {
                    throw e;
                }
            }
            throw new FileNotFoundException("Database file could not be found.");
        }

        public JToken GetApiKeys()
        {
            // Check if API key exists in galacticRoutes.json
            if (FileUtility.FileExists(Path))
            {
                try
                {
                    string jsonString = System.Text.Encoding.UTF8.GetString(FileUtility.GetFileContent(Path)!);

                    JObject jsonObject = JObject.Parse(jsonString);

                    try
                    {
                        return jsonObject["apiKeys"];
                    }
                    catch
                    {
                        throw new UnauthorizedAccessException("Unauthorized access: Invalid API key.");
                    }

                }
                catch (FileNotFoundException e)
                {
                    throw e;
                }
            }

            throw new FileNotFoundException("Database file could not be found.");
        }

        public ApiKey GetApiKey(string key)
        {
            try
            {
                int count = ApiKeys.Where(x => x.Key == key).Count();

                if (count > 1)
                {
                    throw new InvalidOperationException("Several API keys with the same key value exist. This should never happen.");
                }

                if (count == 0)
                {
                    throw new UnauthorizedAccessException("Unauthorized access: Invalid API key.");
                }

                return ApiKeys.Single(x => x.Key == key);
            }
            catch
            {
                throw;
            }

        }
    }
}
