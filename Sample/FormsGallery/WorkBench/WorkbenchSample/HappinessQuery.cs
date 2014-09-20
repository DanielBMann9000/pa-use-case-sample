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
    class HappinessQuery: IQuery
    {
        private readonly FieldKeyFactory _fieldKeyFactory;
        public HappinessQuery(FieldKeyFactory fieldKeyFactory)
        {
            _fieldKeyFactory = fieldKeyFactory;
        }

        public Type[] DefinePrerequisiteIndexers()
        {
            return new[] {typeof (HappinessIndexer)};
        }

        public string Domain
        {
            get { return "PASample.Happiness"; }
        }

        public QueryMetadata QueryMetaData
        {
            get 
            {
                return new QueryMetadata
                {
                    Name = "Happiness",
                    Fields = new List<FieldMetadata>
                    {
                        new FieldMetadata
                        {
                            AssociatedFieldKey = _fieldKeyFactory.GetFieldKey(HappinessIndexer.Namespace, "Sum"),
                            FieldName = "Sum",
                            FriendlyName = "Sum",
                            DataType = typeof(double)
                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(HappinessIndexer.Namespace,"Min"),
                            FieldName="Min",
                            FriendlyName="Min",
                            DataType=typeof(double)

                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(HappinessIndexer.Namespace,"Max"),
                            FieldName="Max",
                            FriendlyName="Max",
                            DataType=typeof(double)
                            

                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey =  _fieldKeyFactory.GetFieldKey(HappinessIndexer.Namespace, "Count"),
                            FieldName = "Count",
                            FriendlyName = "Count",
                            DataType = typeof(int)
                        }

                    }
                };
            }
        }
    }
}
