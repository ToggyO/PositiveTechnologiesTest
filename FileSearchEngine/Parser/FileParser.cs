using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

using FileSearchEngine.Engine;
using FileSearchEngine.Models;

namespace FileSearchEngine.Parser
{
    public class FileParser
    {
        public async Task<User> ParseJson(SearchResult result)
        {
            FileStream readStream = null;
            try
            {
                readStream = new FileStream(result.FullPath, FileMode.Open, FileAccess.Read);
                return await JsonSerializer.DeserializeAsync<User>(readStream);
            }
            catch
            {
                return default;
            }
            finally
            {
                readStream?.Dispose();
            }
        }
        
        public async Task<Soft> ParseXml(SearchResult result)
        {
            try
            {
                await using var readStream = new FileStream(result.FullPath, FileMode.Open, FileAccess.Read);
                var serializer = new XmlSerializer(typeof(Soft));
                return (Soft) await Task.FromResult(serializer.Deserialize(readStream));
            }
            catch
            {
                return default;
            }
        }
    }
}