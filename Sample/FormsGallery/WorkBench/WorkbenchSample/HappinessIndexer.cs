using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregationInterfaces.BinAggregation;
using AggregationInterfaces.Querying;
using AggregationInterfaces.Querying.Metadata;
using AggregationInterfaces.Schema;
using Common.Logging;
using PreEmptive.Analytics.Workbench.Plugins.Feature;
using PreEmptive.Analytics.Workbench.Plugins.Session;
using PreEmptive.Components.Messaging.Schema.Public.V2;
using PreEmptive.Workbench.Interfaces;
using PreEmptive.Workbench.Interfaces.Indexing;
using PreEmptive.Workbench.Interfaces.Indexing.Scopes;

namespace WorkbenchSample
{
    public class HappinessIndexer: IndexerBase
    {
        public static readonly string Namespace = "PASample";

        public const string Happiness = "Happiness";

        private readonly IFunctionalLogger _logger;

        public HappinessIndexer(FieldKeyFactory fieldKeyFactory, FeatureScope scope, IFunctionalLogger logger)
            : base(fieldKeyFactory, Namespace, scope)
        {
            _logger = logger;
        }

        public override Type[] DefinePrerequisiteIndexers()
        {
            return new[] { typeof(FeatureIndexer) };
        }

        public override Type[] DefineMessageTypesToExtract()
        {
            return new[] { MessageType.Feature };
        }

        protected override void DefineFields()
        {
            DefineField("Count", typeof(Int32), FieldType.Data);
            DefineField("Sum", typeof(double), FieldType.Data);
            DefineField("Min", typeof(double), FieldType.Data, MergeOption.Min);
            DefineField("Max", typeof(double), FieldType.Data, MergeOption.Max);
        }

        public override ExtractResult Extract(IStateBin tempStateBin,
            EnvelopeAttributes envelopeAttributes,
            Message message)
        {
            if (message.ExtendedInformation.Any())
            {
                var key=message.ExtendedInformation.Find(ek => ek.Key == "Happiness");
                if (key != null && key.DataType=="decimal")
                {
                    double val = 0;
                    if (double.TryParse(key.Value, out val))
                    {
                        _logger.Log("HappinessIndexer", string.Format("Found decimal value of: {0}", val), null, LoggingLevel.Debug, null);

                        tempStateBin.AddValue(GetFieldKey(Namespace,"Sum"), val);
                        tempStateBin.AddValue(GetFieldKey(Namespace,"Count"), 1);
                        tempStateBin.AddValue(GetFieldKey(Namespace,"Min"), val);
                        tempStateBin.AddValue(GetFieldKey(Namespace,"Max"), val);
                        return new ExtractResult(true);
                    }
                }
            }
            return new ExtractResult(false);   
        }



        public override Transform[] DefineTransforms()
        {
            return null;
        }

        public override OutputSchema[] DefineOutputSchemas()
        {
            //Return null instead if using the sample pattern
            return new[]
        {
            new OutputSchema("HappinessSchema", this)
            {
                RequiredFields = new HashSet<FieldKey> 
                                { 
                                    GetFieldKey(Namespace, "Count"),
                                    GetFieldKey(Namespace,"Min"),
                                    GetFieldKey(Namespace,"Max"),
                                    GetFieldKey(Namespace,"Sum")
                                }

            }
        };

        }

        public OutputSchema[] ExtendOutputSchemaWithPattern(OutputSchema[] outputSchemas)
        {

            return null;
        }


        public FieldMetadata[] ExtendQuery(IQuery query)
        {

            return null;
        }
}
}
