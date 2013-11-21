using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Data.Elasticsearch
{
    public class ElasticsearchModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ElasticsearchQueryRunner>().ToSelf().InSingletonScope();
            Bind<ElasticsearchFinder>().ToSelf().InSingletonScope();
            Bind<ElasticsearchInserter>().ToSelf().InSingletonScope();
        }
    }
}
