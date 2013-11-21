using PlainElastic.Net;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Data.Elasticsearch
{
    [Export("Elasticsearch", typeof(Adapter))]
    public class ElasticsearchAdapter : Adapter
    {
        private ElasticsearchWorkers workers;

        private ElasticConnection connection;
        private string indexName;

        public ElasticsearchAdapter() 
        {
            workers = NinjectConfiguration.Kernel.Get<ElasticsearchWorkers>();
        }

        public override int Delete(string tableName, SimpleExpression criteria)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IDictionary<string, object>> Find(string tableName, SimpleExpression criteria)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, object> Get(string tableName, params object[] keyValues)
        {
            return workers.Finder.Get(connection, indexName, tableName, keyValues[0].ToString());
        }

        public override IDictionary<string, object> GetKey(string tableName, IDictionary<string, object> record)
        {
            throw new NotImplementedException();
        }

        public override IList<string> GetKeyNames(string tableName)
        {
            return new List<string> { "_id" };
        }

        public override IDictionary<string, object> Insert(string tableName, IDictionary<string, object> data, bool resultRequired)
        {
            throw new NotImplementedException();
        }

        public override bool IsExpressionFunction(string functionName, params object[] args)
        {
            if(functionName == "TermsFacet")
                return true; //FIXME

            return false;
        }

        public override IEnumerable<IDictionary<string, object>> RunQuery(SimpleQuery query, out IEnumerable<SimpleQueryClauseBase> unhandledClauses)
        {
            return workers.QueryRunner.RunQuery(connection, indexName, query, out unhandledClauses);
        }

        public override int Update(string tableName, IDictionary<string, object> data, SimpleExpression criteria)
        {
            throw new NotImplementedException();
        }

        protected override void OnSetup()
        {
            connection = new ElasticConnection(Settings.Host, Settings.Port);
            indexName = Settings.IndexName;
        }

    }
}
