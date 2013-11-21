using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Data.Elasticsearch
{
    class ElasticsearchWorkers
    {
        public ElasticsearchInserter Inserter { get; private set; }
        public ElasticsearchFinder Finder { get; private set; }
        public ElasticsearchQueryRunner QueryRunner { get; private set; }

        public ElasticsearchWorkers(ElasticsearchInserter inserter, ElasticsearchFinder finder, ElasticsearchQueryRunner queryRunner)
        {
            this.Inserter = inserter;
            this.Finder = finder;
            this.QueryRunner = queryRunner;
        }
    }
}
