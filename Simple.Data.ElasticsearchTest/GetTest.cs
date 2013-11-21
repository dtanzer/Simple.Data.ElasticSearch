using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Simple.Data.Elasticsearch;

namespace Simple.Data.ElasticsearchTest
{
    [TestClass]
    public class GetTest
    {
        private dynamic db;

        [TestInitialize]
        public void SetUp()
        {
            DatabaseHelper.Reset();
            db = DatabaseHelper.Open();
        }
        
        [TestMethod]
        public void GetWithNonExistingKeyReturnsNull()
        {
            var product = db.Products.Get(-1);
            Assert.IsNull(product);
        }

        [TestMethod]
        public void GetWithExistingKeyReturnsTheProductObject()
        {
            var product = db.Products.Get(1);
            Assert.IsNotNull(product);
            Assert.AreEqual(1, product.Id);
            Assert.AreEqual("ACME Hole in a box", product.Name);
        }
    }
}
