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
namespace WorkbenchSample
{
    class HappinessQuery: IQuery
    {
        private readonly FieldKeyFactory _fieldKeyFactory;
        public HappinessQuery(FieldKeyFactory fieldKeyFactory)
        {
            _fieldKeyFactory = fieldKeyFactory;
        }

        public Type[] DefinePrerequisiteIndexers()
        {
            return new[] {typeof (HappinessIndexer)};
        }

        public string Domain
        {
            get { return "PASample.Happiness"; }
        }

        public QueryMetadata QueryMetaData
        {
            get 
            {
                return new QueryMetadata
                {
                    Name = "Happiness",
                    Fields = new List<FieldMetadata>
                    {
                        new FieldMetadata
                        {
                            AssociatedFieldKey = _fieldKeyFactory.GetFieldKey(HappinessIndexer.Namespace, "Sum"),
                            FieldName = "Sum",
                            FriendlyName = "Sum",
                            DataType = typeof(double)
                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(HappinessIndexer.Namespace,"Min"),
                            FieldName="Min",
                            FriendlyName="Min",
                            DataType=typeof(double)

                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(HappinessIndexer.Namespace,"Max"),
                            FieldName="Max",
                            FriendlyName="Max",
                            DataType=typeof(double)
                            

                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey =  _fieldKeyFactory.GetFieldKey(HappinessIndexer.Namespace, "Count"),
                            FieldName = "Count",
                            FriendlyName = "Count",
                            DataType = typeof(int)
                        }

                    }
                };
            }
        }
    }
}
