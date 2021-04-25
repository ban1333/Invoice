using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InvoiceBRY.Models
{
    public partial class InvoiceDescription
    {
        public int DescriptionId { get; set; }
        [Display(Name = "Invoice")]
        public int InvoiceId { get; set; }
        public string Description { get; set; }
    }
}
