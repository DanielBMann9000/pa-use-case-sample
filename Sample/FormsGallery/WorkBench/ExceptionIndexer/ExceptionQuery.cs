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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregationInterfaces.Querying;
using AggregationInterfaces.Querying.Metadata;
using AggregationInterfaces.Schema;
using PreEmptive.Workbench.Interfaces.Indexing.ComplexFields;

namespace PreEmptive.Analytics.Workbench.Plugins.Exception
{
    public class ExceptionQuery : IQuery
    {
        private readonly FieldKeyFactory _fieldKeyFactory;

        public ExceptionQuery(FieldKeyFactory fieldKeyFactory)
        {
            _fieldKeyFactory = fieldKeyFactory;
        }

        public Type[] DefinePrerequisiteIndexers()
        {
            return new[] {typeof (ExceptionIndexer)};
        }

        public string Domain
        {
            get { return "PreEmptive.Exceptions"; }
        }

        public QueryMetadata QueryMetaData
        {
            get
            {
                return new QueryMetadata
                {
                    Name = "Summary",
                    Fields = new List<FieldMetadata>
                        {
                            new FieldMetadata
                            {
                                AssociatedFieldKey = _fieldKeyFactory.GetFieldKey(ExceptionIndexer.Namespace, ExceptionIndexer.StackTrace),
                                FieldName = "ExceptionMessage",
                                FriendlyName = "Message",
                                DataType = typeof(string),
                                PreAggregationValueTransform = (md, v) => (v as ExceptionInformation).Entries[0].Message
                            },
                            new FieldMetadata
                            {
                                AssociatedFieldKey = _fieldKeyFactory.GetFieldKey(ExceptionIndexer.Namespace, ExceptionIndexer.StackTrace),
                                FieldName = "ExceptionTrace",
                                FriendlyName = "Stack Trace",
                                DataType = typeof(string),
                                PreAggregationValueTransform = (md, v) => (v as ExceptionInformation).ToString()
                            },
                            new FieldMetadata
                            {
                                AssociatedFieldKey = _fieldKeyFactory.GetFieldKey(ExceptionIndexer.Namespace, ExceptionIndexer.ExceptionCount),
                                FieldName = "Count",
                                FriendlyName = "Exception Count",
                                DataType = typeof(int),
                            },
                            new FieldMetadata
                            {
                                AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExceptionIndexer.Namespace,ExceptionIndexer.ThrownCount),
                                FieldName="Thrown",
                                FriendlyName="Thrown Count",
                                DataType=typeof(int)
                            }
                            ,
                            new FieldMetadata
                            {
                                AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExceptionIndexer.Namespace,ExceptionIndexer.CaughtCount),
                                FieldName="Caught",
                                FriendlyName="Caught Count",
                                DataType=typeof(int)
                            }
                            ,
                            new FieldMetadata
                            {
                                AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExceptionIndexer.Namespace,ExceptionIndexer.UnhandledCount),
                                FieldName="Unhandled",
                                FriendlyName="Unhandled Count",
                                DataType=typeof(int)
                            }
                    }
                };
            }
        }
    }
}
