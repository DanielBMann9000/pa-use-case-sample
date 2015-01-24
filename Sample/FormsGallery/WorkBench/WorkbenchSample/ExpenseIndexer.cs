using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregationInterfaces.BinAggregation;
using AggregationInterfaces.Schema;
using Common.Logging;
using PreEmptive.Components.Messaging.Schema.Public.V2;
using PreEmptive.Workbench.Interfaces.Indexing;
using PreEmptive.Workbench.Interfaces.Indexing.Scopes;
using AggregationInterfaces;
using AggregationInterfaces.BinAggregation;
using AggregationInterfaces.Schema;
using AggregationInterfaces.Utilities;
using Common.Utilities;


namespace WorkbenchSample
{
    public class ExpenseIndexer : IndexerBase
    {
        public const string Namespace = "Expense";
        public const string CountField = "TotalCount";
        public const string ApprovedField = "TotalApproved";
        public const string RejectedField = "TotalRejected";
        public const string AmountField = "Amount";
        public const string ApprovedAmountField = "ApprovedAmount";
        public const string RejectedAmountField = "RejectedAmount";
        public const string RejectionReasonField = "RejectionReason";
        public const string RequestReasonField = "RequestReason";
        private IFunctionalLogger _log;
        public ExpenseIndexer(FieldKeyFactory fieldKeyFactory, FeatureScope scope, IFunctionalLogger logger)
            : base(fieldKeyFactory, Namespace, scope)
        {
            _log = logger;
        }
        protected override void DefineFields()
        {
            DefineField(RejectionReasonField, typeof(string), FieldType.PivotKey, MergeOption.Aggregate, false, true);
            DefineField(RequestReasonField, typeof(string), FieldType.PivotKey, MergeOption.Aggregate, false, true);
            DefineField(AmountField, typeof(double), FieldType.Data);
            DefineField(ApprovedAmountField, typeof(double), FieldType.Data);
            DefineField(RejectedAmountField, typeof(double), FieldType.Data);
            DefineField(RejectedField, typeof(int), FieldType.Data);
            DefineField(ApprovedField, typeof(int), FieldType.Data);
            DefineField(CountField, typeof(int), FieldType.Data);
        }

        public override Type[] DefineMessageTypesToExtract()
        {
            return new Type[] { MessageType.Feature };
        }

        public override AggregationInterfaces.Schema.OutputSchema[] DefineOutputSchemas()
        {
            return new[]
            {

                    new OutputSchema("ExpenseApprovedSchema",this)
                    {
                       
                          RequiredFields = new HashSet<FieldKey>
                                                { 
                                                    GetFieldKey(AmountField),
                                                    GetFieldKey(CountField),
                                                    GetFieldKey(ApprovedField),
                                                     GetFieldKey(RejectedField),
                                                     GetFieldKey(RejectedAmountField),
                                                     GetFieldKey(ApprovedAmountField)

                                                },
                        PivotKeys = new HashSet<FieldKey>
                                                { 
                                                    GetFieldKey(RequestReasonField)
                                                }
                                                
                    }
            };
        }

        public override Type[] DefinePrerequisiteIndexers()
        {
            return null;
        }

        public override AggregationInterfaces.Schema.Transform[] DefineTransforms()
        {
            return new[]
            {
                new Transform("ApprovedAmountField"){
                    RequiredFields=new []{
                        GetFieldKey(ApprovedField),
                        GetFieldKey(AmountField)
                    },
                    TransformActions=fields=>
                        {
                            var amt=fields[GetFieldKey(AmountField)].Value;
                            var approved = fields.GetEntry<int>(GetFieldKey(ApprovedField));
                            try
                            {
                                   
                                return new []{
                                      new TransformAction()
                                            {
                                                Key=GetFieldKey(ApprovedAmountField),
                                                Value=(approved==1? amt:0.00)

                                            },
                                            new TransformAction()
                                            {
                                                Key=GetFieldKey(RejectedAmountField),
                                                Value=(approved==0? amt:0.00)
                                            }
                             
                             };   }
                            catch(Exception e)
                            {
                                this._log.Log("ExpenseIndexer", "Error in Approved Ammount Transform", e, LoggingLevel.Error);
                                throw;
                            }
                        }
                        


                }
            };


        }

    
        

        public override AggregationInterfaces.BinAggregation.ExtractResult Extract(AggregationInterfaces.BinAggregation.IStateBin bin,
            PreEmptive.Workbench.Interfaces.EnvelopeAttributes envelopeAttributes, PreEmptive.Components.Messaging.Schema.Public.V2.Message message)
        {
            var msg = message as FeatureMessage;
            var extract = false;
            if (msg != null && msg.Name == "Expense Approval - Service")
            {
                _log.Log("ExpenseIndexer", "Extracting Expense Request", null, LoggingLevel.Info);
                bin.AddValue(GetFieldKey(CountField), 1);
                var approved = false;
                foreach (var key in msg.ExtendedInformation)
                {
                    switch (key.Key)
                    {
                        case "Requestor":
                            break;
                        case "Department":
                            break;
                        case "Amount":
                            bin.AddValue(GetFieldKey(AmountField), double.Parse(key.Value));
                            extract = true;
                            break;
                        case "Reason":
                            bin.AddValue(GetFieldKey(RequestReasonField), key.Value);
                            extract = true;
                            break;
                        case "Approved":


                            if (key.Value == "1")
                            {
                                approved = true;
                                bin.AddValue(GetFieldKey(ApprovedField), 1);
                                bin.AddValue(GetFieldKey(RejectedField), 0);
                            }
                            else
                            {
                                approved = false;
                                bin.AddValue(GetFieldKey(ApprovedField), 0);
                                bin.AddValue(GetFieldKey(RejectedField), 1);
                            }
                            extract = true;
                            break;
                        case "RejectedReason":
                            extract = true;
                            bin.AddValue(GetFieldKey(RejectionReasonField), key.Value);
                            break;
                    }
                }




            }
            _log.Log("ExpenseIndexer", "Finished Expense Request returning : " + extract , null, LoggingLevel.Info);
            return new ExtractResult(extract);
        }
    }
}
