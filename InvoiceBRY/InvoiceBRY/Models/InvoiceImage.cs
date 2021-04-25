using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InvoiceBRY.Models
{
    public partial class InvoiceImage
    {
        public int ImageId { get; set; }
        [Display(Name = "Invoice")]
        public int InvoiceId { get; set; }
        public string Image { get; set; }
    }
}
