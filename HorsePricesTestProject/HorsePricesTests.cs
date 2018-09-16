using DataExtractEngineInterfaces;
using HorsePrices.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;
namespace HorsePricesTestProject
{
    public class HorsePricesTests
    {
        [Fact]
        public void Get()
        {
            var feed = (new Mock<List<IFeedDataExtractor>>()).Object;
            var config = new Mock<IConfiguration>();
            var logging = (new Mock<ILogger<HorsePriceController>>()).Object;
            config.Setup(c => c.GetSection(It.IsAny<string>())).Returns(new ConfigurationSection(new ConfigurationRoot(new List<IConfigurationProvider>()),"test"));
            HorsePrices.Controllers.HorsePriceController testController =
                new HorsePrices.Controllers.HorsePriceController(feed, config.Object, logging);
            Assert.Equal(null, (testController.Get()).StatusCode);
        }
    }
}
