// Copyright (c) 2014 PreEmptive Solutions; All Right Reserved, http://www.preemptive.com/
//
// This source is subject to the Microsoft Public License (MS-PL).
// Please see the License.txt file for more information.
// All other rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CustomerIndexer : IndexerBase, IIndexerPattern, IQueryPattern
    {
        public static readonly string Namespace = "PASample";
        private Dictionary<string, string> _customers;
        private const string  _customerFile="CustomerList.txt";
        //PivotKeys
        public const string Customer = "Customer";

        private readonly IFunctionalLogger _logger;

        public CustomerIndexer(FieldKeyFactory fieldKeyFactory, SessionScope sessionScope, IFunctionalLogger logger)
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
            return new[] { MessageType.ApplicationLifeCycle,MessageType.Feature };
        }

        protected override void DefineFields()
        {
            DefineField(Customer, typeof(string), FieldType.PivotKey);
        }

        public override ExtractResult Extract(IStateBin tempStateBin,
            EnvelopeAttributes envelopeAttributes,
            Message message)
        {
            string custID;

            var appMessage = message as ApplicationLifeCycle;
            if (null == appMessage)
            {
                var key = message.ExtendedInformation.Find(ek => ek.Key == "Requestor");
                if (key!=null)
                {
                    custID = key.Value;
                }
                else
                {
                    return new ExtractResult(false);
                }
            }
            else
            {
                custID = envelopeAttributes.Serial;
            }


            tempStateBin.AddValue(GetFieldKey(Customer), MapSerial(custID));


            return new ExtractResult(true);
        }

        private string MapSerial(string serial)
        {
            try
            {
                var customers = _customers ?? LoadCustomers();
                return _customers[serial];
            }
            catch(Exception ex)
            {
                _logger.Log("CustomerIndexer", "Unable to lookup customer", ex, LoggingLevel.Warn, new KeyValuePair<string, object>("Serial", serial));
                return "Unknown";
            }
        }

        private Dictionary<string, string> LoadCustomers()
        {
            var lines=System.IO.File.ReadAllLines(GetFilePath());
            _customers = new Dictionary<string, string>(10);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                _customers.Add(parts[0].Trim(), parts[1].Trim());
            }
            return _customers;
        }

        private string GetFilePath()
        {
            return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(this.GetType()).Location), _customerFile);
        }

        public override Transform[] DefineTransforms()
        {
            return null;
        }

        public override OutputSchema[] DefineOutputSchemas()
        {
            //Return null instead if using the sample pattern
            return null;

        }

        public OutputSchema[] ExtendOutputSchemaWithPattern(OutputSchema[] outputSchemas)
        {

            return outputSchemas
                .Where(s => !s.PivotKeys.Contains(GetFieldKey(Namespace, Customer)))
                .Select(outputSchema =>
                {
                    var pivotKeys = new HashSet<FieldKey>(outputSchema.PivotKeys)
                                {
                                    GetFieldKey(Namespace, Customer)
                                   
                                };

                    return new OutputSchema(outputSchema.Name + "_Customer", this)
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
                    AssociatedFieldKey = GetFieldKey(Namespace, Customer),
                    FieldName = "Customer",
                    FriendlyName = "Customer",
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
