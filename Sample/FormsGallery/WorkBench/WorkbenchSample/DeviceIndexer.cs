using System;
using System.Collections.Generic;

using AggregationInterfaces.Schema;
using AggregationInterfaces.BinAggregation;

using PreEmptive.Components.Messaging.Schema.Public.V2;
using PreEmptive.Workbench.Interfaces;
using PreEmptive.Workbench.Interfaces.Indexing;
using PreEmptive.Workbench.Interfaces.Indexing.Scopes;


namespace DeviceInfo
{
    public class DeviceIndexer : IndexerBase
    {
        public const string Namespace = "Device";

        public const string FIELDNAME_MANUFACTURER = "Manufacturer";
        public const string FIELDNAME_MODEL = "Model";
        public const string FIELDNAME_COUNT = "SessionCount";

        public DeviceIndexer(FieldKeyFactory fieldKeyFactory, SessionScope sessionScope)
            : base(fieldKeyFactory, Namespace, sessionScope)
        {
        }

        public override Type[] DefinePrerequisiteIndexers()
        {
            return null;
        }

        public override Type[] DefineMessageTypesToExtract()
        {
            return new[] { MessageType.SystemProfile };
        }

        protected override void DefineFields()
        {
            DefineField(FIELDNAME_MANUFACTURER, typeof(string), FieldType.PivotKey);
            DefineField(FIELDNAME_MODEL, typeof(string), FieldType.PivotKey);
            DefineField(FIELDNAME_COUNT, typeof(int), FieldType.Data);
        }

        public override ExtractResult Extract(IStateBin tempStateBin,
            EnvelopeAttributes envelopeAttributes,
            Message message)
        {
            var systemProfileMessage = message as SystemProfileMessage;
            if ( systemProfileMessage == null ) {
                return new ExtractResult(false);
            }

            string deviceManufacturer = "Not Reported";
            string deviceModel = "Not Reported";

            if (systemProfileMessage.Manufacturer != null)
            {
                if (systemProfileMessage.Manufacturer.Manufacturer != null)
                {
                    deviceManufacturer = systemProfileMessage.Manufacturer.Manufacturer.ToUpperInvariant();
                }
                if (systemProfileMessage.Manufacturer.Model != null)
                {
                    deviceModel = systemProfileMessage.Manufacturer.Model.ToUpperInvariant();
                }
            }
           
            tempStateBin.AddValue(GetFieldKey(FIELDNAME_MANUFACTURER), deviceManufacturer);
            tempStateBin.AddValue(GetFieldKey(FIELDNAME_MODEL), deviceModel);
            tempStateBin.AddValue(GetFieldKey(FIELDNAME_COUNT), 1, true);

            return new ExtractResult(true);

        }

        public override Transform[] DefineTransforms()
        {
            return null;
        }

        public override OutputSchema[] DefineOutputSchemas()
        {
            return new[]
                {
                    new OutputSchema("DeviceSessionCountSchema", this)
                    {
                        RequiredFields = new HashSet<FieldKey> 
                                        {                                             
                                            GetFieldKey(FIELDNAME_COUNT)
                                            
                                        },
                        PivotKeys = new HashSet<FieldKey>
                                        {
                                            GetFieldKey(FIELDNAME_MANUFACTURER),
                                            GetFieldKey(FIELDNAME_MODEL)
                                        }
                    }
                };
        }

    }
}
