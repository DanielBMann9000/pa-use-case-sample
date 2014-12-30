using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASample.Service.Models
{
    public class ExpenseApprovalRequest :ServiceRequest
    {
        
        public Decimal Amount { get; set; }
        public string Reason { get; set; }


       
    }
}
