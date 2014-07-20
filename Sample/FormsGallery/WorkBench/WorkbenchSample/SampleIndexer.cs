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
    /// Sample indexer that counts how many sessions occur in on computers with less than 1 gig of memory, 1-4 gigs of memory and greater than 4
    /// </summary>
    public class SampleIndexer : IndexerBase, IIndexerPattern, IQueryPattern
    {
        public const string Namespace = "Sample";

        //PivotKeys
        public const string MemoryBucket = "MemoryBucket";

        private readonly IFunctionalLogger _logger;

        public SampleIndexer(FieldKeyFactory fieldKeyFactory, SessionScope sessionScope, IFunctionalLogger logger)
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
            return new[] { MessageType.PerformanceProbe };
        }

        protected override void DefineFields()
        {
            DefineField(MemoryBucket, typeof(string), FieldType.PivotKey);
        }

        public override ExtractResult Extract(IStateBin tempStateBin, 
            EnvelopeAttributes envelopeAttributes, 
            Message message)
        {
            var performanceProbeMessage = message as PerformanceProbeMessage;
            if (null == performanceProbeMessage)
                return new ExtractResult(false);

            //Adjust memory into one of three buckets
            var availableMemoryInMb = performanceProbeMessage.MemoryMBAvailable;
            if (availableMemoryInMb < 1024)
                availableMemoryInMb = 0;
            else if (availableMemoryInMb < 4096)
                availableMemoryInMb = 1024;
            else availableMemoryInMb = 4096;

#if DEBUG
            _logger.Log("CustomSampleIndexer", "This is a sample logging message recording {Bytes}", 
                null, 
                LoggingLevel.Warn, 
                new KeyValuePair<string,object>("Bytes", availableMemoryInMb)); //By default, only Warn and above will be recorded
#endif

            tempStateBin.AddValue(GetFieldKey(MemoryBucket), availableMemoryInMb.ToString());
            return new ExtractResult(true);
        }

        public override Transform[] DefineTransforms()
        {
            return null;
        }

        public override OutputSchema[] DefineOutputSchemas()
        {
            //Return null instead if using the sample pattern
            return new []
                {
                    new OutputSchema("SampleSessionCountSchema", this)
                    {
                        RequiredFields = new HashSet<FieldKey> 
                                        { 
                                            GetFieldKey(SessionIndexer.Namespace, SessionIndexer.SessionStartCount)
                                        },
                        PivotKeys = new HashSet<FieldKey>
                                        {
                                            GetFieldKey(MemoryBucket)
                                        }
                    }
                };
        }

        public OutputSchema[] ExtendOutputSchemaWithPattern(OutputSchema[] outputSchemas)
        {
            //Comment out the following line when you want to use the Sample Pattern
            return null;

            return outputSchemas.Select(outputSchema =>
            {
                var pivotKeys = new HashSet<FieldKey>(outputSchema.PivotKeys)
                                {
                                    GetFieldKey(SampleIndexer.Namespace, SampleIndexer.MemoryBucket)
                                };

                return new OutputSchema(outputSchema.Name + "_MemoryPattern", this)
                {
                    RequiredFields = outputSchema.RequiredFields,
                    PivotKeys = pivotKeys
                };
            }).ToArray();
        }

        public FieldMetadata[] ExtendQuery(IQuery query)
        {
            //Comment out the following line when you want to use the Sample Pattern
            return null;

            return new[]
            {
                new FieldMetadata
                {
                    AssociatedFieldKey = GetFieldKey(SampleIndexer.Namespace, SampleIndexer.MemoryBucket),
                    FieldName = "MemoryAmount",
                    FriendlyName = "Amount of Memory",
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
