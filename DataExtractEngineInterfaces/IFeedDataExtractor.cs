using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExtractEngineInterfaces
{
    public interface IFeedDataExtractor
    {
        IList<HorsePrice> GetHorsesFromSource(string sourceDirectoryPath);
    }
}
