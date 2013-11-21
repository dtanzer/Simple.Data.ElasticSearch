using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
