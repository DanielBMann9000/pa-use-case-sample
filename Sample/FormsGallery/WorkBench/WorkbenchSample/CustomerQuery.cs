using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregationInterfaces.Querying;
using AggregationInterfaces.Querying.Metadata;
using AggregationInterfaces.Schema;
using PreEmptive.Analytics.Workbench.Plugins.Exception;
using PreEmptive.Analytics.Workbench.Plugins.Session;
using PreEmptive.Workbench.Interfaces.Querying;

namespace WorkbenchSample
{
    public class CustomerQuery : IQuery
    {
        private readonly FieldKeyFactory _fieldKeyFactory;
        public CustomerQuery(FieldKeyFactory fieldKeyFactory)
        {
            _fieldKeyFactory = fieldKeyFactory;
        }

        public Type[] DefinePrerequisiteIndexers()
        {
            return new[] {typeof (CustomerIndexer)};
        }

        public string Domain
        {
            get { return "PASample.CustomerPerformance"; }
        }

        public QueryMetadata QueryMetaData
        {
            get 
            {
                return new QueryMetadata
                {
                    Name = "Perf",
                    Fields = new List<FieldMetadata>
                    {
                        new FieldMetadata
                        {
                            AssociatedFieldKey = _fieldKeyFactory.GetFieldKey(CustomerIndexer.Namespace, CustomerIndexer.Customer),
                            FieldName = "Customer",
                            FriendlyName = "Customer",
                            DataType = typeof(string)
                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(CustomerIndexer.Namespace,"Department"),
                            FieldName="Department",
                            FriendlyName="Department",
                            DataType=typeof(string)

                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExceptionIndexer.Namespace,ExceptionIndexer.ExceptionCount),
                            FieldName="ExceptionCount",
                            FriendlyName="Exception Count",
                            DataType=typeof(int)
                            

                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey =  _fieldKeyFactory.GetFieldKey(SessionIndexer.Namespace, SessionIndexer.SessionStartCount),
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
