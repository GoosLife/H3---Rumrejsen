using H3___Rumrejsen.DataAccess.Utility;
using H3___Rumrejsen.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text.Json.Nodes;

namespace H3___Rumrejsen.DataAccess
{
    public class JsonDb : IDbAccess
    {
        private readonly string Path;

        public JsonDb()
        {
            // I still don't know how to get this folder in a more intuitive way.
            string rootFolder = System.IO.Directory.GetCurrentDirectory();

            Path = System.IO.Path.Combine(rootFolder, "Database\\galacticRoutes.json");
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
                    catch(Exception e)
                    {
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

        public bool ValidateApiKey(string key)
        {
            // Check if API key exists in galacticRoutes.json
            if (FileUtility.FileExists(Path))
            {
                try
                {
                    string jsonString = System.Text.Encoding.UTF8.GetString(FileUtility.GetFileContent(Path)!);

                    // Get the galacticRoutes array from the JSON file, then deserialize it into a list of GalacticRoute objects.
                    JObject jsonObject = JObject.Parse(jsonString);

                    try
                    {
                        JToken jsonArray = jsonObject["apiKeys"];

                        // Check if the key exists in the array. Each entry in jsonArray contains the properties key, type and expires, so we need to check the key property.
                        return jsonArray!.Any(x => x["key"]?.ToString() == key);
                    }
                    catch
                    {
                        throw new UnauthorizedAccessException("Unauthorized access: API key doesn't exist.");
                    }

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
