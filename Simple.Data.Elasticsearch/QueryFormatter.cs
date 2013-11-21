using PlainElastic.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Data.Elasticsearch
{
    class QueryFormatter
    {
        private SimpleExpression criteria;
        private string typeName;

        public QueryFormatter(SimpleExpression criteria, string typeName)
        {
            this.criteria = criteria;
            this.typeName = typeName;
        }

        internal string FormatQuery()
        {
            var serializer = new JsonNetSerializer();
            return serializer.Serialize(FormatQuery(criteria));
        }

        private object FormatQuery(SimpleExpression expression)
        {
            switch (expression.Type)
            {
                case SimpleExpressionType.Equal:
                    return EqualQueryExpression(expression);
            }
            throw new NotImplementedException();
        }

        private IDictionary<string, object> EqualQueryExpression(SimpleExpression expression)
        {
            var leftHandName = GetLeftHandName(expression);
            var matchQuery = new Dictionary<string, object>();

            var matchContent = new Dictionary<String, object>();
            matchQuery["match_phrase"] = matchContent;

            matchContent[leftHandName] = expression.RightOperand;

            return matchQuery;
        }

        private string GetLeftHandName(SimpleExpression expression)
        {
            var leftHandName = ((ObjectReference)expression.LeftOperand).GetAllObjectNamesDotted();
            if (!leftHandName.StartsWith(typeName))
            {
                throw new InvalidOperationException("At the moment, equals is only supported for fields of the \"type name\" of the query. Type Name: \"" + typeName + "\", left hand operand: \"" + leftHandName + "\".");
            }
            return leftHandName.Substring(typeName.Length+1);
        }
    }
}
