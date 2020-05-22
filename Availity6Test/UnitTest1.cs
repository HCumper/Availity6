using System.Collections.Generic;
using Availity6;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Availity6Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string testFile = @"..\..\..\TestData.csv";
            ProcessCSVFiles fileProcessor = new ProcessCSVFiles();
            IList<CSVRow> rows = fileProcessor.ParseCSV(testFile);
            fileProcessor.BreakUpFeed(rows);
            var prudential = fileProcessor.ParseCSV(@"..\..\..\Prudential.csv");
            Assert.AreEqual(prudential.Count, 6);
            Assert.AreEqual(prudential[2].FullName, "Joe Root");
            Assert.AreEqual(prudential[2].UserId, "9375");
            Assert.AreEqual(prudential[4].Version, 7);
            var universal = fileProcessor.ParseCSV(@"..\..\..\Universal.csv");
            Assert.AreEqual(universal[2].FullName, "Jos Buttler");
            Assert.AreEqual(universal[0].UserId, "36");
            var royalLothian = fileProcessor.ParseCSV(@"..\..\..\Royal Lothian.csv");
            Assert.AreEqual(royalLothian.Count, 1);
        }
    }

}
