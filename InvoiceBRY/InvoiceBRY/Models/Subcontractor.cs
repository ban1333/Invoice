using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InvoiceBRY.Models
{
    public partial class Subcontractor
    {
        public Subcontractor()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int SubcontractorId { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        [Display(Name = "Apt Unit Or Suit")]
        public string AptUnitOrSuit { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "HST Amount")]
        public decimal? HstAmount { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
