﻿// Copyright (c) 2014 PreEmptive Solutions; All Right Reserved, http://www.preemptive.com/
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
using System.Text;
using System.Threading.Tasks;
using AggregationInterfaces.Querying;
using AggregationInterfaces.Querying.Metadata;
using AggregationInterfaces.Schema;

namespace WorkbenchSample
{
    public class ExpenseQuery : IQuery
    {
        
        private readonly FieldKeyFactory _fieldKeyFactory;
        public ExpenseQuery(FieldKeyFactory fieldKeyFactory)
        {
            _fieldKeyFactory = fieldKeyFactory;
        }
        public string Domain
        {
            get { return "PASample.Expense"; }
        }

        public AggregationInterfaces.Querying.Metadata.QueryMetadata QueryMetaData
        {
            get
            {
                return new QueryMetadata
                    {
                         Name = "Expense",
                    Fields = new List<FieldMetadata>
                    {
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExpenseIndexer.Namespace,ExpenseIndexer.CountField),
                            FieldName=ExpenseIndexer.CountField,
                            FriendlyName=ExpenseIndexer.CountField,
                            DataType=typeof(int)                       



                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExpenseIndexer.Namespace,ExpenseIndexer.AmountField),
                            FieldName=ExpenseIndexer.AmountField,
                            FriendlyName=ExpenseIndexer.AmountField,
                            DataType=typeof(int)                       



                        },new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExpenseIndexer.Namespace,ExpenseIndexer.RejectedField),
                            FieldName=ExpenseIndexer.RejectedField,
                            FriendlyName=ExpenseIndexer.RejectedField,
                            DataType=typeof(int)                       



                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExpenseIndexer.Namespace,ExpenseIndexer.ApprovedField),
                            FieldName=ExpenseIndexer.ApprovedField,
                            FriendlyName=ExpenseIndexer.ApprovedField,
                            DataType=typeof(int)                       



                        },                      
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExpenseIndexer.Namespace,ExpenseIndexer.RequestReasonField),
                            FieldName=ExpenseIndexer.RequestReasonField,
                            FriendlyName=ExpenseIndexer.RequestReasonField,
                            DataType=typeof(string)                       



                        },
                                              
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExpenseIndexer.Namespace,ExpenseIndexer.ApprovedAmountField),
                            FieldName=ExpenseIndexer.ApprovedAmountField,
                            FriendlyName=ExpenseIndexer.ApprovedAmountField,
                            DataType=typeof(int)                       



                        },                      
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExpenseIndexer.Namespace,ExpenseIndexer.RejectedAmountField),
                            FieldName=ExpenseIndexer.RejectedAmountField,
                            FriendlyName=ExpenseIndexer.RejectedAmountField,
                            DataType=typeof(int)                       



                        }
                    }
                    };
            }
        }

        public Type[] DefinePrerequisiteIndexers()
        {
            return new[] {typeof(ExpenseIndexer)};
        }
    }
}
