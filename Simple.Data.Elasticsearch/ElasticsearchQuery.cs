using PlainElastic.Net;
using PlainElastic.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Data.Elasticsearch
{
    class ElasticsearchQuery
    {
        private SimpleQuery query;
        private string indexName;
        private bool isCountQuery;
        private IDictionary<Type, Action<SimpleQueryClauseBase>> queryAnalyzers;
        private SimpleExpression criteria;

        public IEnumerable<SimpleQueryClauseBase> UnhandledClauses { get; private set; }

        public ElasticsearchQuery(string indexName, SimpleQuery query)
        {
            this.indexName = indexName;
            this.query = query;
            this.queryAnalyzers = new Dictionary<Type, Action<SimpleQueryClauseBase>>
            {
                { typeof(SelectClause), c => AnalyzeSelectClause((SelectClause)c) },
                { typeof(WhereClause), c => AnalyzeWhereClause((WhereClause)c) }
            };

            analyzeQuery();
        }

        private void analyzeQuery()
        {
            var unhandledClauses = new List<SimpleQueryClauseBase>();
            foreach(SimpleQueryClauseBase clause in query.Clauses) 
            {
                Action<SimpleQueryClauseBase> processor;
                if (queryAnalyzers.TryGetValue(clause.GetType(), out processor))
                {
                    processor(clause);
                }
                else
                {
                    unhandledClauses.Add(clause);
                }
            }
            this.UnhandledClauses = unhandledClauses;
        }

        internal string BuildQuery()
        {
            if (criteria != null)
            {
                var rawQuery = new QueryFormatter(criteria, query.TableName).FormatQuery();

                var queryBody = rawQuery;
                if (!isCountQuery)
                {
                    queryBody = "{ \"query\": " + rawQuery + " }";
                }

                return queryBody;
            }
            return "";
        }

        internal string BuildCommand()
        {
            string tableName = query.TableName;
            if (tableName == "_")
            {
                tableName = null;
            }
            string command;
            if (isCountQuery)
            {
                command = new CountCommand(indexName, tableName);
            }
            else
            {
                command = new SearchCommand(indexName, tableName);
            }
            return command;
        }

        private void AnalyzeSelectClause(SelectClause selectClause)
        {
            var columns = selectClause.Columns.ToList();
            if (columns.Count == 1 && columns[0].GetType() == typeof(CountSpecialReference))
            {
                isCountQuery = true;
            }
            else
            {
                throw new NotImplementedException("Only count queries are supported at the moment.");
            }
        }

        private void AnalyzeWhereClause(WhereClause whereClause)
        {
            this.criteria = whereClause.Criteria;
        }

        internal IEnumerable<IDictionary<string, object>> QueryResultToSimpleDataResult(OperationResult jsonResults)
        {
            var serializer = new JsonNetSerializer();

            if (isCountQuery)
            {
                var count = serializer.ToCountResult(jsonResults).count;

                var enumerable = new List<IDictionary<string, object>>();
                var dictionary = new Dictionary<string, object>();
                dictionary["Count"] = count;
                enumerable.Add(dictionary);

                return enumerable;
            }
            else
            {
                return serializer.ToSearchResult<IDictionary<string, object>>(jsonResults).Documents;
            }
        }
    }
}
