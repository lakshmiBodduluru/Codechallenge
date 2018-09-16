using DataExtractEngineInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataExtractEngine
{
    /// <summary>
    /// Extract data from Json Feed
    /// </summary>
    public class JsonFeedDataExtractor : IFeedDataExtractor
    {
        /// <summary>
        /// Extract Json Feed and return list of horse and price information
        /// </summary>
        /// <param name="sourceDirectoryPath"></param>
        /// <returns></returns>
        public IList<HorsePrice> GetHorsesFromSource(string sourceDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(sourceDirectoryPath))
            {
                throw new ArgumentNullException(nameof(sourceDirectoryPath));
            }
            List<HorsePrice> horsePriceList = new List<HorsePrice>();
            DirectoryInfo d = new DirectoryInfo(sourceDirectoryPath);
            FileInfo[] jsonFiles = d.GetFiles("*.json");
            foreach (FileInfo file in jsonFiles)
            {
                var fileFullName = file.FullName;
                horsePriceList.AddRange(GetHorses(fileFullName));
            }

            return horsePriceList;
        }

        //Read content of file in the filepath and parse json
        private List<HorsePrice> GetHorses(string filePath)
        {
            try
            {
                List<HorsePrice> horsePriceList = new List<HorsePrice>();
                string raceJson = File.ReadAllText(filePath);

                RaceObject race = JsonConvert.DeserializeObject<RaceObject>(raceJson);
                race.RawData.Markets.ForEach(market => market.Selections.ForEach(horse => horsePriceList.Add(new HorsePrice
                {
                    Name = horse.Tags.name,
                    Price = horse.Price
                })));
                return horsePriceList;
            }
            catch
            {
                throw;
            }
        }
    }
}
