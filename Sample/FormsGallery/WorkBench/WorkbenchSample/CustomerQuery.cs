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
using AggregationInterfaces.Querying;
using AggregationInterfaces.Querying.Metadata;
using AggregationInterfaces.Schema;
using PreEmptive.Analytics.Workbench.Plugins.Exception;
using PreEmptive.Analytics.Workbench.Plugins.Session;

namespace WorkbenchSample
{
    public class CustomerQuery : IQuery
    {
        private readonly FieldKeyFactory _fieldKeyFactory;
        public CustomerQuery(FieldKeyFactory fieldKeyFactory)
        {
            _fieldKeyFactory = fieldKeyFactory;
        }

        public Type[] DefinePrerequisiteIndexers()
        {
            return new[] {typeof (CustomerIndexer)};
        }

        public string Domain
        {
            get { return "PASample.CustomerPerformance"; }
        }

        public QueryMetadata QueryMetaData
        {
            get 
            {
                return new QueryMetadata
                {
                    Name = "Perf",
                    Fields = new List<FieldMetadata>
                    {
                        new FieldMetadata
                        {
                            AssociatedFieldKey = _fieldKeyFactory.GetFieldKey(CustomerIndexer.Namespace, CustomerIndexer.Customer),
                            FieldName = "Customer",
                            FriendlyName = "Customer",
                            DataType = typeof(string)
                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(CustomerIndexer.Namespace,"Department"),
                            FieldName="Department",
                            FriendlyName="Department",
                            DataType=typeof(string)

                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExceptionIndexer.Namespace,ExceptionIndexer.ExceptionCount),
                            FieldName="ExceptionCount",
                            FriendlyName="Exception Count",
                            DataType=typeof(int)
                            

                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey =  _fieldKeyFactory.GetFieldKey(SessionIndexer.Namespace, SessionIndexer.SessionStartCount),
                            FieldName = "SessionCount",
                            FriendlyName = "Session Count",
                            DataType = typeof(int)
                        }
                    }
                };
            }
        }
    }
}
