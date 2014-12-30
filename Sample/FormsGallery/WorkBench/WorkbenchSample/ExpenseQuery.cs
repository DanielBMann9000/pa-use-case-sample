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
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExpenseIndexer.Namespace,ExpenseIndexer.RejectionReasonField),
                            FieldName=ExpenseIndexer.RejectionReasonField,
                            FriendlyName=ExpenseIndexer.RejectionReasonField,
                            DataType=typeof(string)                       



                        },
                        new FieldMetadata
                        {
                            AssociatedFieldKey=_fieldKeyFactory.GetFieldKey(ExpenseIndexer.Namespace,ExpenseIndexer.RequestReasonField),
                            FieldName=ExpenseIndexer.RequestReasonField,
                            FriendlyName=ExpenseIndexer.RequestReasonField,
                            DataType=typeof(string)                       



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
