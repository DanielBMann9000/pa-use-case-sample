using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregationInterfaces.Querying;
using AggregationInterfaces.Querying.Metadata;
using AggregationInterfaces.Schema;

namespace WorkbenchSample
{

    class ColorQuery : IQuery
    {

        private readonly FieldKeyFactory _fieldKeyFactory;
        public ColorQuery(FieldKeyFactory fieldKeyFactory)
        {
            _fieldKeyFactory = fieldKeyFactory;
        }

        public Type[] DefinePrerequisiteIndexers()
        {
            return new[] { typeof(HappinessIndexer) };
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
                    Name = "Color",
                    Fields = new List<FieldMetadata>
                    {
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(HappinessIndexer.Namespace,"Color"),
                            FieldName="Color",
                            FriendlyName="Color",
                            DataType=typeof(string)



                        },
                                                new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(HappinessIndexer.Namespace,"ColorCount"),
                            FieldName="ColorCount",
                            FriendlyName="ColorCount",
                            DataType=typeof(int)



                        }

                    }
                };
            }
        }


    }
}
