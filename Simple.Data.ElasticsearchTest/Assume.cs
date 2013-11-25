using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Data.ElasticsearchTest
{
    public class Assume
    {
        public static void AssumeEqual(Object expected, Object actual, string valueName = "value")
        {
            if (!Object.Equals(expected, actual))
            {
                Assert.Inconclusive("Assumed \"" + valueName + "\"==\"" + expected + "\", but was \"" + actual + "\".");
            }
        }
    }
}
