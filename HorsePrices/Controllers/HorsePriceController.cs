using DataExtractEngineInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using HorsePrice = DataExtractEngineInterfaces.HorsePrice;

namespace HorsePrices.Controllers
{
    /// <summary>
    /// HorsePrice API is a public API that gets list of horses with prices in ascending order of price
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HorsePriceController : ControllerBase
    {
        private readonly IEnumerable<IFeedDataExtractor> _dataExtractors;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public HorsePriceController(IEnumerable<IFeedDataExtractor> dataExtractors,
            IConfiguration configuration,
            ILogger<HorsePriceController> logger)
        {
            _dataExtractors = dataExtractors;
            _configuration = configuration;
            _logger = logger;
        }
        /// <summary>
        /// Get Horse Price from races feed in ascending price order
        /// This is a public API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                var path = _configuration.GetSection("MySettings")["FeedDataPath"];
                if (string.IsNullOrWhiteSpace(path))
                {
                    _logger.LogError("No feed data available");
                    return new JsonResult(new EmptyResult());
                }
                List<HorsePrice> horsePrices = new List<HorsePrice>();
                foreach (IFeedDataExtractor extractor in _dataExtractors)
                {
                    horsePrices.AddRange(extractor.GetHorsesFromSource(path));
                }
                return new JsonResult(horsePrices.OrderBy(h => h.Price).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Inner Exception: { 0}", "Stack Trace: { 1}", ex.InnerException, ex.StackTrace);
                return new JsonResult(new BadRequestResult());
            }
        }
    }
}
