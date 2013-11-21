using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Data.Elasticsearch
{
    public static class IDatabaseOpenerExtensions
    {
        public static dynamic OpenElasticsearch(this IDatabaseOpener opener, string host, int port, string indexName)
        {
            return opener.Open("Elasticsearch", new { Host = host, Port = port, IndexName = indexName });
        }
    }
}
