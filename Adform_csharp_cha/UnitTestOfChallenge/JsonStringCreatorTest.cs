using System;
using Adform_csharp_cha;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestOfChallenge
{
    [TestClass]
    public class JsonStringCreatorTest
    {
        [TestMethod]
        [ExpectedException(typeof(JsonException))]
        public void CreateJsonString_NullObjectSend_ThowsException()
        {
            FakeFilter fakeFilter = new FakeFilter(null);
            JsonStringCreator jsCreator = new JsonStringCreator(fakeFilter, new string[] { },new string[] { });
            jsCreator.CreateJsonString(null);
        }

     }


    public class FakeFilter : IFilterTypeData
    {
        public FilterDate date { get; }

        public FakeFilter(FilterDate date)
        {
            this.date = date;
        }
    }
}
