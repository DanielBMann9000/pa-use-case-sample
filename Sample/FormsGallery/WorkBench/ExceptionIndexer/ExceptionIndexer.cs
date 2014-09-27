/***************************************************************************************
* 
* Copyright 2014 PreEmptive Solutions, LLC.
* 
* You may not use this file except in compliance with the PreEmptive Solutions License.
* You may obtain a copy of the License at http://www.preemptive.com/eula.
* This software is distributed on an "AS IS" basis.
* 
****************************************************************************************/

using System;
using System.Collections.Generic;
using AggregationInterfaces.BinAggregation;
using AggregationInterfaces.Schema;
using Common.Logging;
using PreEmptive.Components.Messaging.Schema.Public.V2;
using PreEmptive.Workbench.Interfaces;
using PreEmptive.Workbench.Interfaces.Indexing;
using PreEmptive.Workbench.Interfaces.Indexing.Scopes;
using ExceptionInformation = PreEmptive.Workbench.Interfaces.Indexing.ComplexFields.ExceptionInformation;

namespace PreEmptive.Analytics.Workbench.Plugins.Exception
{
    public class ExceptionIndexer : IndexerBase
    {
        public const string Namespace = "Exception";

        //PivotKeys
        public const string StackTrace = "StackTrace";

        //RequiredFields
        public const string ExceptionCount = "Count";

        public const string ThrownCount = "Thrown";
        public const string CaughtCount = "Caught";
        public const string UnhandledCount = "Unhandled";

        private IFunctionalLogger _log;
        public ExceptionIndexer(FieldKeyFactory fieldKeyFactory, MessageScope messageScope, SessionScope sessionScope, IFunctionalLogger logger)
            : base(fieldKeyFactory, Namespace, messageScope, sessionScope)
        {
            _log = logger;
        }

        protected override void DefineFields()
        {
            DefineField(StackTrace, typeof(ExceptionInformation), FieldType.PivotKey, MergeOption.Aggregate, true, true);
            DefineField(ExceptionCount, typeof(int), FieldType.Data);
            DefineField(ThrownCount, typeof(int), FieldType.Data);
            DefineField(CaughtCount, typeof(int), FieldType.Data);
            DefineField(UnhandledCount, typeof(int), FieldType.Data);
        }

        public override Type[] DefinePrerequisiteIndexers()
        {
            return null;
        }

        public override Type[] DefineMessageTypesToExtract()
        {
            return new[] { MessageType.Fault };
        }

        public override ExtractResult Extract(IStateBin tempStateBin, EnvelopeAttributes envelopeAttributes, Message message)
        {
            try
            {
                _log.Log("ExceptionIndexer", "Enter Exception Extract", null, LoggingLevel.Info);
                var faultMessage = message as FaultMessage;
                if (null == faultMessage)
                    return new ExtractResult(false);
                if (0 == faultMessage.Exceptions.Count)
                    return new ExtractResult(false);

                tempStateBin.AddValue(GetFieldKey(StackTrace), new ExceptionInformation(faultMessage.FaultEvent, faultMessage.Exceptions));
                tempStateBin.AddValue(GetFieldKey(ExceptionCount), 1);
                switch (faultMessage.FaultEvent)
                {
                    case FaultEventType.Caught:
                        tempStateBin.AddValue(GetFieldKey(CaughtCount), 1);
                        break;
                    case FaultEventType.Thrown:
                        tempStateBin.AddValue(GetFieldKey(ThrownCount), 1);
                        break;
                    case FaultEventType.Uncaught:
                        tempStateBin.AddValue(GetFieldKey(UnhandledCount), 1);
                        break;
                }
                _log.Log("ExceptionIndexer", "Exit Exception Extract", null, LoggingLevel.Info);
                return new ExtractResult(true);
            }
            catch(System.Exception ex)
            {
                _log.Log("ExceptionIndexer", ex.Message, ex, LoggingLevel.Fatal);
                return new ExtractResult(true);
            }
        }

        public override Transform[] DefineTransforms()
        {
            return null;
        }

        public override OutputSchema[] DefineOutputSchemas()
        {
            return new[]
            {
                    new OutputSchema("ExceptionSchema", this)
                    {
                        RequiredFields = new HashSet<FieldKey>
                                                { 
                                                    GetFieldKey(ExceptionCount),
                                                    GetFieldKey(ThrownCount),

                                                },
                        PivotKeys = new HashSet<FieldKey>
                                                { 
                                                    GetFieldKey(StackTrace)
                                                }
                    },
                    new OutputSchema("UhandledCount",this)
                    {
                        RequiredFields=new HashSet<FieldKey>
                        {
                            GetFieldKey(ExceptionCount), 
                            GetFieldKey(UnhandledCount)
                            
                        },
                        PivotKeys=new HashSet<FieldKey>
                        {
                            GetFieldKey(StackTrace)
                        }

                    },
                    new OutputSchema("CaughtCount",this)
                    {
                        RequiredFields=new HashSet<FieldKey>
                        {   
                            GetFieldKey(ExceptionCount), 
                            GetFieldKey(CaughtCount)
                        },
                        PivotKeys=new HashSet<FieldKey>
                        {
                            GetFieldKey(StackTrace)
                        }
                    }
            };
        }
    }
}
