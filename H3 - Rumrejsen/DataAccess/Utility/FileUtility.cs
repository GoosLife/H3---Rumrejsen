namespace H3___Rumrejsen.DataAccess.Utility
{
    public class FileUtility
    {
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static byte[]? GetFileContent(string path)
        {
            return File.Exists(path) ? File.ReadAllBytes(path) : throw new FileNotFoundException("File not found");
        }
    }
}
