using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Data.ElasticsearchTest
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
    }

    class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
