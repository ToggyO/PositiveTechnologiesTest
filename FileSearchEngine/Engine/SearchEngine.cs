using System.Collections.Generic;
using System.IO;

namespace FileSearchEngine.Engine
{
    public class SearchEngine
    {
        private List<string> _validExtensions;

        public SearchEngine() {}
        
        public SearchEngine(IEnumerable<string> extensions)
        {
            SetValidExtensions(extensions);
        }

        public void SetValidExtensions(IEnumerable<string> extensions) => _validExtensions = new List<string>(extensions);
        
        public void ScanDirectoryForFiles(string path, List<SearchResult> result)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                string ext = Path.GetExtension(file);
                if (_validExtensions.Contains(ext))
                    result.Add(new SearchResult { FullPath = file, Extension = ext });
            }

            foreach (var directory in Directory.GetDirectories(path))
                ScanDirectoryForFiles(directory, result);
        }
    }
}