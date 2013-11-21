
using PlainElastic.Net;
using PlainElastic.Net.Serialization;
using Simple.Data.Elasticsearch;
using System;

namespace Simple.Data.ElasticsearchTest
{
    class DatabaseHelper
    {
        public static dynamic Open()
        {
            return Database.Opener.OpenElasticsearch("localhost", 9200, "testindex");
        }

        public static void Reset()
        {
            var connection = new ElasticConnection("localhost", 9200);
            TryDeleteIndex(connection);
            var serializer = new JsonNetSerializer();

            var manufacturers = InsertManufacturers(connection, serializer);
            InsertProducts(connection, serializer, manufacturers);

            connection.Post("_flush");
        }

        private static Manufacturer[] InsertManufacturers(ElasticConnection connection, JsonNetSerializer serializer)
        {
            var manufacturers = new[]
            {
                new Manufacturer { Id=1, Name="ACME Corporation"},
                new Manufacturer { Id=2, Name="Laramie Inc."}
            };

            foreach (Manufacturer manufacturer in manufacturers)
            {
                IndexObject(connection, serializer, manufacturer);
            }

            return manufacturers;
        }

        private static void InsertProducts(ElasticConnection connection, JsonNetSerializer serializer, Manufacturer[] manufacturers)
        {
            var products = new[] 
            {
                new Product { Id=1, Name="ACME Hole in a box", Tags=new[] {"escape", "useful", "fun"}},
                new Product { Id=2, Name="ACME Dynamite", Tags=new[] {"dangerous", "fun", "burning"}},
                new Product { Id=3, Name="Laramie Cigarettes", Tags=new[] {"cancer", "burning"}, Description="Do not use with ACME lighter!"}
            };

            foreach (Product product in products)
            {
                IndexObject(connection, serializer, product);
            }
        }

        private static void IndexObject(ElasticConnection connection, JsonNetSerializer serializer, dynamic toIndex)
        {
            connection.Put(new IndexCommand("testindex", toIndex.GetType().Name + "s", toIndex.Id.ToString()), serializer.ToJson((object) toIndex));
        }

        private static void TryDeleteIndex(ElasticConnection connection)
        {
            try
            {
                connection.Delete(new DeleteCommand("testindex"));
            }
            catch (Exception)
            {
                //Nothing to do here, index probably does not exist.
            }
        }
    }
}
