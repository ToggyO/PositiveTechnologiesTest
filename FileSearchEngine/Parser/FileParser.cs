using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

using FileSearchEngine.Engine;
using FileSearchEngine.Models;

namespace FileSearchEngine.Parser
{
    public static class FileParser
    {
        public static async Task<User> ParseJson(SearchResult result)
        {
            try
            {
                await using var readStream = new FileStream(result.FullPath, FileMode.Open, FileAccess.Read);
                return await JsonSerializer.DeserializeAsync<User>(readStream);
            }
            catch (Exception e)
            {
                return default;
            }
        }
        
        public static async Task<Soft> ParseXml(SearchResult result)
        {
            try
            {
                await using var readStream = new FileStream(result.FullPath, FileMode.Open, FileAccess.Read);
                var serializer = new XmlSerializer(typeof(Soft));
                return (Soft) await Task.FromResult(serializer.Deserialize(readStream));
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}