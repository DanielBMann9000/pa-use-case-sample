using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AggregationInterfaces.BinAggregation;
using AggregationInterfaces.Querying;
using AggregationInterfaces.Querying.Filters;
using AggregationInterfaces.Querying.Metadata;
using AggregationInterfaces.Schema;
using Common.Logging;
using PreEmptive.Analytics.Workbench.Plugins.Session;
using PreEmptive.Components.Messaging.Schema.Public.V2;
using PreEmptive.Workbench.Interfaces;
using PreEmptive.Workbench.Interfaces.Indexing;
using PreEmptive.Workbench.Interfaces.Indexing.Scopes;

namespace WorkbenchSample
{
    /// <summary>
    /// 
    /// </summary>
    public class InstanceIndexer : IndexerBase, IIndexerPattern, IQueryPattern
    {
        public const string Namespace = "FormsGaller";

        //PivotKeys
        public const string MemoryBucket = "MemoryBucket";
        public const string InstanceId = "InstanceId";

        private readonly IFunctionalLogger _logger;

        public InstanceIndexer(FieldKeyFactory fieldKeyFactory, SessionScope sessionScope, IFunctionalLogger logger)
            : base(fieldKeyFactory, Namespace, sessionScope)
        {
            _logger = logger;
        }

        public override Type[] DefinePrerequisiteIndexers()
        {
            return new[] { typeof(SessionIndexer) };
        }

        public override Type[] DefineMessageTypesToExtract()
        {
            return new[] { MessageType.SessionLifeCycle };
        }

        protected override void DefineFields()
        {
            DefineField(InstanceId, typeof(string), FieldType.PivotKey);
        }

        public override ExtractResult Extract(IStateBin tempStateBin, 
            EnvelopeAttributes envelopeAttributes, 
            Message message)
        {
            
            tempStateBin.AddValue(GetFieldKey(InstanceId), envelopeAttributes.Serial);

         
            return new ExtractResult(true);
        }

        public override Transform[] DefineTransforms()
        {
            return null;
        }

        public override OutputSchema[] DefineOutputSchemas()
        {
            //Return null instead if using the sample pattern
            return null;
            //return new []
            //    {
            //        new OutputSchema("InstanceIdSchema", this)
            //        {
            //            RequiredFields = new HashSet<FieldKey> 
            //                            { 
            //                                GetFieldKey(SessionIndexer.Namespace, SessionIndexer.CompleteSessionStartCount)
            //                                //,
            //                                //GetFieldKey(SessionIndexer.Namespace, SessionIndexer.SessionLength),
            //                                //GetFieldKey(SessionIndexer.Namespace, SessionIndexer.MinSessionLength),
            //                                //GetFieldKey(SessionIndexer.Namespace, SessionIndexer.MaxSessionLength),
            //                            },
            //            PivotKeys = new HashSet<FieldKey>
            //                            {
            //                                GetFieldKey(InstanceId)
            //                            }
            //        }
            //    };
        }

        public OutputSchema[] ExtendOutputSchemaWithPattern(OutputSchema[] outputSchemas)
        {
            //Comment out the following line when you want to use the Sample Pattern
            //return null;

            return outputSchemas.Select(outputSchema =>
            {
                var pivotKeys = new HashSet<FieldKey>(outputSchema.PivotKeys)
                                {
                                    GetFieldKey(Namespace, InstanceId)
                                };

                return new OutputSchema(outputSchema.Name + "_InstanceId", this)
                {
                    RequiredFields = outputSchema.RequiredFields,
                    PivotKeys = pivotKeys
                };
            }).ToArray();
        }

        public FieldMetadata[] ExtendQuery(IQuery query)
        {
            //Comment out the following line when you want to use the Sample Pattern
            //return null;

            return new[]
            {
                new FieldMetadata
                {
                    AssociatedFieldKey = GetFieldKey(Namespace, InstanceId),
                    FieldName = "Serial",
                    FriendlyName = "Serial Number",
                    DataType = typeof(string),
                    Filters = new List<FilterMetadata>()
                    {
                        new FilterMetadata()
                        {
                            Type = FilterType.Many.ToString(),
                            Filter = new PickFilter(new object[0])
                        }
                    }
                }
            };
        }
    }
}
