using System;
using Xunit;
using DataExtractEngine;
namespace DataExtractEngineTestProject
{
    public class DataExtractEngineTests
    {

        [Fact]
        public void EmptyXMLFeedTest()
        {
            var xmlDataExtractEngine = new XMLFeedDataExtractor();
            Assert.Equal(0, xmlDataExtractEngine.GetHorsesFromSource("./FeedDataEmpty").Count);
        }

        [Fact]
        public void XMLFeedTest()
        {
            var xmlDataExtractEngine = new XMLFeedDataExtractor();
            Assert.Equal(2, xmlDataExtractEngine.GetHorsesFromSource("./FeedData").Count);
        }
        [Fact]
        public void EmptyJsonFeedTest()
        {
            var jsonDataExtractEngine = new JsonFeedDataExtractor();
            Assert.Equal(0, jsonDataExtractEngine.GetHorsesFromSource("./FeedDataEmpty").Count);
        }

        [Fact]
        public void JsonFeedTest()
        {
            var jsonDataExtractEngine = new JsonFeedDataExtractor();
            Assert.Equal(2, jsonDataExtractEngine.GetHorsesFromSource("./FeedData").Count);
        }
    }
}
