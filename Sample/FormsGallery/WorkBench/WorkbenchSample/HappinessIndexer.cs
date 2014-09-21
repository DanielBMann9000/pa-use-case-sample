using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregationInterfaces.BinAggregation;
using AggregationInterfaces.Querying;
using AggregationInterfaces.Querying.Filters;
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
            DefineField("Color", typeof(string), FieldType.PivotKey);
            DefineField("ColorCount", typeof(int), FieldType.Data);
        }

        public override ExtractResult Extract(IStateBin tempStateBin,
            EnvelopeAttributes envelopeAttributes,
            Message message)
        {
            var returnExtract = false;
            if (message.ExtendedInformation.Any())
            {

                var key = message.ExtendedInformation.Find(ek => ek.Key == "Happiness");
                if (key != null && key.DataType == "decimal")
                {
                    double val = 0;
                    if (double.TryParse(key.Value, out val))
                    {
                        _logger.Log("HappinessIndexer", string.Format("Found decimal value of: {0}", val), null, LoggingLevel.Debug, null);

                        tempStateBin.AddValue(GetFieldKey(Namespace, "Sum"), val);
                        tempStateBin.AddValue(GetFieldKey(Namespace, "Count"), 1);
                        tempStateBin.AddValue(GetFieldKey(Namespace, "Min"), val);
                        tempStateBin.AddValue(GetFieldKey(Namespace, "Max"), val);
                        returnExtract = true;
                    }
                }

                key = message.ExtendedInformation.Find(ek => ek.Key == "Color");
                if (key != null)
                {
                    _logger.Log("HappinessIndexer", string.Format("Found color value of: {0}", key.Value), null, LoggingLevel.Debug, null);

                    tempStateBin.AddValue(GetFieldKey(Namespace, "Color"), key.Value);
                    tempStateBin.AddValue(GetFieldKey(Namespace, "ColorCount"), 1);
                    returnExtract = true;
                }
            }
            return new ExtractResult(returnExtract);   
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
                                    GetFieldKey(Namespace,"Sum"),
                                  

                                }


            },
            new OutputSchema("ColorSchema",this)
            {
                RequiredFields=new HashSet<FieldKey>
                {
                    GetFieldKey(Namespace,"ColorCount")
                },
                PivotKeys=new HashSet<FieldKey>
                {
                    GetFieldKey(Namespace,"Color")  
                }

            }
        };

        }

        public OutputSchema[] ExtendOutputSchemaWithPattern(OutputSchema[] outputSchemas)
        {

            return outputSchemas
                    .Where(s => !s.PivotKeys.Contains(GetFieldKey(Namespace, "Color")))
                    .Select(outputSchema =>
                    {
                        var pivotKeys = new HashSet<FieldKey>(outputSchema.PivotKeys)
                                                {
                                                    GetFieldKey(Namespace, "Color")
                                   
                                                };

                        return new OutputSchema(outputSchema.Name + "_Color", this)
                        {
                            RequiredFields = outputSchema.RequiredFields,
                            PivotKeys = pivotKeys

                        };
                    }).ToArray();
        }


        public FieldMetadata[] ExtendQuery(IQuery query)
        {

            return new[]
            {
                new FieldMetadata
                {
                    AssociatedFieldKey = GetFieldKey(Namespace, "Color"),
                    FieldName = "Color",
                    FriendlyName = "Color",
                    DataType = typeof(string),
                    IsOptional=true,
                    
                    Filters = new List<FilterMetadata>()
                    {
                        new FilterMetadata()
                        {
                            Type = FilterType.Many.ToString(),
                            Filter=new PickFilter(new object[0])
                        }
                    }
                }
            };
        }
}
}
