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
    /// <summary>
    /// 
    /// </summary>
    public class DepartmentIndexer : IndexerBase, IIndexerPattern, IQueryPattern
    {
        public static readonly string Namespace = "PASample";

        //PivotKeys
        public const string Department = "Department";

        private readonly IFunctionalLogger _logger;

        public DepartmentIndexer(FieldKeyFactory fieldKeyFactory, SessionScope sessionScope, IFunctionalLogger logger)
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
            return new[] { MessageType.ApplicationLifeCycle };
        }

        protected override void DefineFields()
        {
            DefineField(Department, typeof(string), FieldType.PivotKey);
        }

        public override ExtractResult Extract(IStateBin tempStateBin,
            EnvelopeAttributes envelopeAttributes,
            Message message)
        {


            foreach (var customInfo in message.ExtendedInformation)
            {
                if (customInfo.Key=="Department")
                {

                    tempStateBin.AddValue(GetFieldKey(Department), customInfo.Value);
                    return new ExtractResult(true);
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
            return null;

        }

        public OutputSchema[] ExtendOutputSchemaWithPattern(OutputSchema[] outputSchemas)
        {

            return outputSchemas
                .Where(s => !s.PivotKeys.Contains(GetFieldKey(Namespace, Department)))
                .Select(outputSchema =>
                {
                    var pivotKeys = new HashSet<FieldKey>(outputSchema.PivotKeys)
                                {
                                    GetFieldKey(Namespace, Department)
                                   
                                };

                    return new OutputSchema(outputSchema.Name + "_" + Department, this)
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
                    AssociatedFieldKey = GetFieldKey(Namespace, Department),
                    FieldName = "Department",
                    FriendlyName = "Department",
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
