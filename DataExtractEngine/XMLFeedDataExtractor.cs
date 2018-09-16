using DataExtractEngineInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace DataExtractEngine
{
    /// <summary>
    /// Extract data from XML Feed
    /// </summary>
    public class XMLFeedDataExtractor : IFeedDataExtractor
    {
        /// <summary>
        /// Extract XML Feed and return list of horse and price information
        /// </summary>
        /// <param name="sourceDirectoryPath"></param>
        /// <returns></returns>
        public IList<HorsePrice> GetHorsesFromSource(string sourceDirectoryPath)
        {

            if (string.IsNullOrWhiteSpace(sourceDirectoryPath))
            {
                throw new ArgumentNullException(nameof(sourceDirectoryPath));
            }
            List<HorsePrice> horses = new List<HorsePrice>();
            DirectoryInfo d = new DirectoryInfo(sourceDirectoryPath);
            FileInfo[] xmlFiles = d.GetFiles("*.xml");
            foreach (FileInfo file in xmlFiles)
            {
                var fileFullName = file.FullName;
                horses.AddRange(GetHorses(fileFullName));
            }

            return horses;
        }

        //Read content of file in the filepath and parse the xml 
        private List<HorsePrice> GetHorses(string filePath)
        {
            try
            {
                List<HorsePrice> horses = new List<HorsePrice>();
                using (var sourceFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var sourceMemoryStream = new MemoryStream())
                    {
                        sourceMemoryStream.SetLength(sourceFileStream.Length);
                        sourceFileStream.Read(sourceMemoryStream.GetBuffer(), 0, (int)sourceFileStream.Length);

                        var xmlSeriaizer = new XmlSerializer(typeof(Meeting));
                        Meeting meeting = (Meeting)xmlSeriaizer.Deserialize(XmlReader.Create(sourceMemoryStream));
                        foreach (Race race in meeting.Races.Race)
                        {
                            horses.AddRange(race.Horses.Horse.Select(h => new HorsePrice
                            {
                                Name = h.Name,
                                Price = Convert.ToDouble(race.Prices.Price.Horses.Horse.Where(hp => hp._Number == h.Number).FirstOrDefault()?.Price)
                            }));
                        }
                        return horses;
                    }
                }
            }
            catch
            {
                throw;
            }

        }
    }
}
