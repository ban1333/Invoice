using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InvoiceBRY.Models
{
    public partial class Invoice
    {
        public int InvoiceId { get; set; }
        public int? CustomerId { get; set; }
        public int? SubcontractorId { get; set; }
        [Display(Name = "Paid")]
        public bool IsPaid { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Hours Worked")]
        public decimal NumberOfHours { get; set; }
        public decimal Rate { get; set; }
        [Display(Name = "Subtotal")]
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Subcontractor Subcontractor { get; set; }
    }
}
