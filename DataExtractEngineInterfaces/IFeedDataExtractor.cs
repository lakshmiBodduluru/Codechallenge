using System.Collections.Generic;

namespace DataExtractEngineInterfaces
{
    public interface IFeedDataExtractor
    {
        IList<HorsePrice> GetHorsesFromSource(string sourceDirectoryPath);
    }
}
