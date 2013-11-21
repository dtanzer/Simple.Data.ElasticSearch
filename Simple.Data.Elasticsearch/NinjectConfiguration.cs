using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Data.Elasticsearch
{
    public class NinjectConfiguration
    {
        public static IKernel Kernel { get; set; }
        public static ElasticsearchModule Module { get; private set; }

        static NinjectConfiguration()
        {
            Module = new ElasticsearchModule();
            Kernel = new StandardKernel(Module);
        }
    }
}
