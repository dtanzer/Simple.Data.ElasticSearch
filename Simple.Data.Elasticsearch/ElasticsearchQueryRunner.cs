using PlainElastic.Net;
using PlainElastic.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Data.Elasticsearch
{
    class ElasticsearchQueryRunner
    {
        internal IEnumerable<IDictionary<string, object>> RunQuery(PlainElastic.Net.ElasticConnection connection, string indexName, SimpleQuery query, out IEnumerable<SimpleQueryClauseBase> unhandledClauses)
        {
            try
            {
                ElasticsearchQuery esQuery = new ElasticsearchQuery(indexName, query);
                unhandledClauses = esQuery.UnhandledClauses;

                var jsonResults = connection.Post(esQuery.BuildCommand(), esQuery.BuildQuery());

                return esQuery.QueryResultToSimpleDataResult(jsonResults);
            }
            catch (OperationException e)
            {
                if (e.HttpStatusCode == 404)
                {
                    unhandledClauses = new List<SimpleQueryClauseBase>();
                    return null;
                }
                throw;
            }
        }
    }
}
