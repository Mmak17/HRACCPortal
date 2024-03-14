using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRACCPortal.Models
{
    public class PaymentsReceivedModel
    {
        public int PaymentsReceivedId { get; set; }
        public string InvoiceNumber{ get; set; }
        public string InvoiceAmount { get; set; }
        public string InvoiceDueDate { get; set; }
        public string CustomerName { get; set; }
        public string DateAdded { get; set; }
        public string DateUpdated { get; set; }
        public string AddedBy{ get; set; }
        public string UpdatedBy { get; set; }
        

    }
}