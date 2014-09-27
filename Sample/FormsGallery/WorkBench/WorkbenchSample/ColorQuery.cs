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

    class ColorQuery : IQuery
    {

        private readonly FieldKeyFactory _fieldKeyFactory;
        public ColorQuery(FieldKeyFactory fieldKeyFactory)
        {
            _fieldKeyFactory = fieldKeyFactory;
        }

        public Type[] DefinePrerequisiteIndexers()
        {
            return new[] { typeof(HappinessIndexer) };
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
                    Name = "Color",
                    Fields = new List<FieldMetadata>
                    {
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(HappinessIndexer.Namespace,"Color"),
                            FieldName="Color",
                            FriendlyName="Color",
                            DataType=typeof(string)



                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(HappinessIndexer.Namespace,"ColorCount"),
                            FieldName="ColorCount",
                            FriendlyName="ColorCount",
                            DataType=typeof(int)



                        }

                    }
                };
            }
        }


    }
}
