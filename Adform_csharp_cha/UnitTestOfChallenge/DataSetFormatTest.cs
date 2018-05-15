using System;
using Adform_csharp_cha;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestOfChallenge
{
    [TestClass]
    public class DataSetFormatTest
    {
        [TestMethod]
        [ExpectedException(typeof (FormatException))]
        public void FormDataFromString_DateAndCountAreMixed_ThowsAnException()
        {
            DataSetFormat dataSetForTesting = new DataSetFormat();
            dataSetForTesting.FormDataFromString(new string[] { "120", "2018.01.01" }); 
        }

        [TestMethod]
        public void FormDataFromString_DateAndCountAreCorrect_ShouldConvertString()
        {
            DataSetFormat dataSetForTesting = new DataSetFormat();
            dataSetForTesting.FormDataFromString(new string[] {  "2018.01.01", "120" });
            DateTime TestDate = new DateTime(year: 2018, month: 01, day: 01);
            Assert.AreEqual(TestDate, dataSetForTesting.DateOfData);
            Assert.AreEqual(120, dataSetForTesting.DataAmount);

        }
    }
}
