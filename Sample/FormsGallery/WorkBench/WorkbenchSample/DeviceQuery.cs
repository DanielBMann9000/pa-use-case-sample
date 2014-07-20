using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregationInterfaces.Querying;
using AggregationInterfaces.Querying.Metadata;
using AggregationInterfaces.Schema;
using PreEmptive.Workbench.Interfaces.Querying;

namespace DeviceInfo
{
    public class DeviceQuery : IQuery
    {
        private readonly FieldKeyFactory _fieldKeyFactory;
        public DeviceQuery(FieldKeyFactory fieldKeyFactory)
        {
            _fieldKeyFactory = fieldKeyFactory;
        }

        public Type[] DefinePrerequisiteIndexers()
        {
            return new[] { typeof(DeviceIndexer) };
        }

        public string Domain
        {
            get { return "PreEmptive.DeviceInfo"; }
        }

        public QueryMetadata QueryMetaData
        {
            get
            {
                return new QueryMetadata
                {
                    Name = "Sessions By Device",
                    Fields = new List<FieldMetadata>
                    {
                        new FieldMetadata
                        {
                            AssociatedFieldKey = _fieldKeyFactory.GetFieldKey(DeviceIndexer.Namespace, DeviceIndexer.FIELDNAME_MANUFACTURER),
                            FieldName = DeviceIndexer.FIELDNAME_MANUFACTURER,
                            FriendlyName = DeviceIndexer.FIELDNAME_MANUFACTURER,
                            DataType = typeof(string)
                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey = _fieldKeyFactory.GetFieldKey(DeviceIndexer.Namespace, DeviceIndexer.FIELDNAME_MODEL),
                            FieldName = DeviceIndexer.FIELDNAME_MODEL,
                            FriendlyName = DeviceIndexer.FIELDNAME_MODEL,
                            DataType = typeof(string)
                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey = _fieldKeyFactory.GetFieldKey(DeviceIndexer.Namespace, DeviceIndexer.FIELDNAME_COUNT),
                            FieldName = DeviceIndexer.FIELDNAME_COUNT,
                            FriendlyName = DeviceIndexer.FIELDNAME_COUNT,
                            DataType = typeof(int)
                        } 
                    }
                };
            }
        }
    }
}
