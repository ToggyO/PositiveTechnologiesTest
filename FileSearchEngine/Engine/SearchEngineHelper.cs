using System.Collections.Generic;

using FileSearchEngine.Constants;

namespace FileSearchEngine.Engine
{
    public static class SearchEngineHelper
    {
        public static IList<string> CreateValidExtensions(
            bool searchForUsers,bool searchForProducts)
        {
            var validExtensions = new List<string>(2);
            if (searchForUsers)
                validExtensions.Add(AppConstants.FileExtensions.User);
            if (searchForProducts)
                validExtensions.Add(AppConstants.FileExtensions.Product);

            return validExtensions;
        }
    }
}