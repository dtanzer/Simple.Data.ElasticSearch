using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Simple.Data.ElasticsearchTest
{
    [TestClass]
    public class FindAllByTest
    {
        private dynamic db;

        [TestInitialize]
        public void SetUp()
        {
            DatabaseHelper.Reset();
            db = DatabaseHelper.Open();
        }
        
        [TestMethod]
        public void FindAllByNameOnlySearchesProductName()
        {
            var products = db.Products.FindAllByName("ACME");

            Assert.AreEqual(2, products.Count());
        }

        [TestMethod]
        public void FindAllByNameReturnsCorrectProducts()
        {
            List<Product> products = db.Products.FindAllByName("ACME");

            Assume.AssumeEqual(2, products.Count);
            AssertResultContains(products, "ACME Hole in a box");
            AssertResultContains(products, "ACME Dynamite");
        }

        private static void AssertResultContains(List<Product> result, string productName)
        {
            var hole = from p in result where p.Name == productName select p;
            Assert.AreEqual(1, hole.Count());
        }
    }
}
