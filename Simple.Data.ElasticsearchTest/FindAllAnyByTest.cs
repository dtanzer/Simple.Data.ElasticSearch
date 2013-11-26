using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Data.ElasticsearchTest
{
    [TestClass]
    public class FindAllAnyByTest
    {
        private dynamic db;

        [TestInitialize]
        public void SetUp()
        {
            DatabaseHelper.Reset();
            db = DatabaseHelper.Open();
        }

        [TestMethod]
        public void FindAllByInCompleteIndexReturnsCorrectEntries()
        {
            List<dynamic> products = db._.FindAllBy(Name: "ACME");

            Assert.AreEqual(3, products.Count());
            AssertResultContains(products, "ACME Hole in a box");
            AssertResultContains(products, "ACME Dynamite");
            AssertResultContains(products, "ACME Corporation");
        }

        private static void AssertResultContains(List<dynamic> result, string name)
        {
            var current = from r in result where r.Name == name select r;
            Assert.AreEqual(1, current.Count());
        }

    }
}
