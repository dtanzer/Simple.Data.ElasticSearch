using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Data.ElasticsearchTest
{
    [TestClass]
    public class FacetTest
    {
        private dynamic db;

        [TestInitialize]
        public void SetUp()
        {
            DatabaseHelper.Reset();
            db = DatabaseHelper.Open();
        }
        
        [TestMethod]
        public void QueryWithFacetsReturnsTheSameSearchResultsAsQueryWithoutFacets()
        {
            dynamic facets;
            IEnumerable<Product> products = db.Products.FindAllBy(Search:"ACME", Tags:db.Products.Tags.TermsFacet(out facets)).Cast<Product>();

            Assert.AreEqual(2, products.Count());
        }
    }
}
