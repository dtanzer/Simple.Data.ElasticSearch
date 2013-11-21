using Newtonsoft.Json;
using PlainElastic.Net;
using PlainElastic.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Data.Elasticsearch
{
    class ElasticsearchFinder
    {
        internal IDictionary<string, object> Get(PlainElastic.Net.ElasticConnection connection, string index, string type, string id)
        {
            try
            {
                var result = connection.Get(new GetCommand(index, type, id));
                var serializer = new JsonNetSerializer();
                var queryResult = serializer.Deserialize<Dictionary<string, object>>(result);
                var source = queryResult["_source"];

                return serializer.Deserialize<Dictionary<string, object>>(source.ToString());
            }
            catch (OperationException e)
            {
                if(e.HttpStatusCode == 404)
                    return null;
                throw;
            }
        }
    }
}
