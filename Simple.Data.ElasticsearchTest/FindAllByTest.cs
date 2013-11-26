using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Simple.Data.ElasticsearchTest
{
    [TestClass]
    public class FindAllProductsByTest
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
            var products = db.Products.FindAllBy(Name: "ACME");

            Assert.AreEqual(2, products.Count());
        }

        [TestMethod]
        public void FindAllByNameReturnsCorrectProducts()
        {
            List<Product> products = db.Products.FindAllBy(Name: "ACME");

            Assume.AssumeEqual(2, products.Count);
            AssertResultContains(products, "ACME Hole in a box");
            AssertResultContains(products, "ACME Dynamite");
        }

        [TestMethod]
        public void FindInAllFieldsFindsAllProducts()
        {
            var products = db.Products.FindAllBy(_: "ACME");

            Assert.AreEqual(3, products.Count());
        }

        [TestMethod]
        public void FindInAllFieldsRetunsCorrectProducts()
        {
            List<Product> products = db.Products.FindAllBy(_: "ACME");

            Assume.AssumeEqual(3, products.Count);
            AssertResultContains(products, "ACME Hole in a box");
            AssertResultContains(products, "ACME Dynamite");
            AssertResultContains(products, "Laramie Cigarettes");
        }

        private static void AssertResultContains(List<Product> result, string productName)
        {
            var hole = from p in result where p.Name == productName select p;
            Assert.AreEqual(1, hole.Count());
        }
    }
}
