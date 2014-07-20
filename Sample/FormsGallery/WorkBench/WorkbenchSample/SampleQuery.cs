using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregationInterfaces.Querying;
using AggregationInterfaces.Querying.Metadata;
using AggregationInterfaces.Schema;
using PreEmptive.Analytics.Workbench.Plugins.Session;
using PreEmptive.Workbench.Interfaces.Querying;

namespace WorkbenchSample
{
    public class SampleQuery : IQuery
    {
        private readonly FieldKeyFactory _fieldKeyFactory;
        public SampleQuery(FieldKeyFactory fieldKeyFactory)
        {
            _fieldKeyFactory = fieldKeyFactory;
        }

        public Type[] DefinePrerequisiteIndexers()
        {
            return new[] {typeof (SampleIndexer)};
        }

        public string Domain
        {
            get { return "PreEmptive.Sample"; }
        }

        public QueryMetadata QueryMetaData
        {
            get 
            {
                return new QueryMetadata
                {
                    Name = "Sessions By Memory",
                    Fields = new List<FieldMetadata>
                    {
                        new FieldMetadata
                        {
                            AssociatedFieldKey = _fieldKeyFactory.GetFieldKey(SampleIndexer.Namespace, SampleIndexer.MemoryBucket),
                            FieldName = "MemoryBucket",
                            FriendlyName = "Memory Amount",
                            DataType = typeof(string)
                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey = _fieldKeyFactory.GetFieldKey(SessionIndexer.Namespace, SessionIndexer.SessionStartCount),
                            FieldName = "SessionCount",
                            FriendlyName = "Session Count",
                            DataType = typeof(int)
                        }
                    }
                };
            }
        }
    }
}
