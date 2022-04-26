using Filter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void PutCorrecrtFlagTest1()
        {
            string[] args = { "output.txt", "--filter=@@minimumOrder==1" };

            var actual = Program.gettingPar(args);
            var expected = "@@minimumOrder==1";

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void PutInorrecrtFlagTest2()
        {

            string[] args = { "output.txt", "filter=@@minimumOrder==1" };

            var actual = Program.gettingPar(args);
            var expected = "";

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void FilterValidationWithCorrectDataTest3()
        {
            //If data is matched var expected should be > 0
            string thisJson = "{\"productId\":163939,\"isbn13\":\"9781933241159\",\"isbn10\":\"1933241152\"}";
            string filterText = "@@isbn10=='1933241152'";

            var actual = Program.Filtering(thisJson, filterText);
            var expected = "1";

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void FilterValidationWithCorrectDataTest4()
        {
            //If data is matched var expected should be > 0
            string thisJson = "{\"productId\":163939,\"isbn13\":\"9781933241159\",\"isbn10\":\"1933241152\"}";
            string filterText = "@@productId>0";

            var actual = Program.Filtering(thisJson, filterText);
            var expected = "1";

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void FilterValidationWithCorrectDataTest5()
        {
            //If data is matched var expected should be > 0
            string thisJson = "{\"productId\":163939,\"isbn13\":\"9781933241159\",\"isbn10\":\"1933241152\"}";
            string filterText = "@@productId>0 && @@isbn10=='1933241152'";

            var actual = Program.Filtering(thisJson, filterText);
            var expected = "1";

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void FilterValidationWithCorrectDataTest6()
        {
            //If data is matched var expected should be > 0
            string thisJson = "{\"productId\":163939,\"isbn13\":\"9781933241159\",\"isbn10\":\"1933241152\"}";
            string filterText = "@@productId==163939 || @@isbn10=='19332411'";

            var actual = Program.Filtering(thisJson, filterText);
            var expected = "1";

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void FilterValidationWithIncorrectDataTest7()
        {
            //If data is not matched var expected should be 0
            string thisJson = "{\"productId\":163939,\"isbn13\":\"9781933241159\",\"isbn10\":\"1933241152\"}";
            string filterText = "@@productId==1639391212 || @@isbn10=='19332411'";

            var actual = Program.Filtering(thisJson, filterText);
            var expected = "0";

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void FilterValidationWithIncorrectDataTest8()
        {
            //If data is matched var expected should be > 0
            string thisJson = "{\"productId\":163939,\"isbn13\":\"9781933241159\",\"isbn10\":\"1933241152\"}";
            string filterText = "@@productId<0 && @@isbn10=='19332411'";

            var actual = Program.Filtering(thisJson, filterText);
            var expected = "0";

            Assert.AreEqual(actual, expected);
        }
    }
}