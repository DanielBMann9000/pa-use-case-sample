using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregationInterfaces.Querying;
using AggregationInterfaces.Schema;
using PreEmptive.Workbench.Interfaces.Indexing;

namespace WorkbenchSample
{
    public class HappinessIndexer : IndexerBase, IIndexerPattern, IQueryPattern
    {
    }
}
