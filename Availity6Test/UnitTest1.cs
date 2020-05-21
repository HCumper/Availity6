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
        }
    }

}
