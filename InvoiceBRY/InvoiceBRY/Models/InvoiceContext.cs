using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace InvoiceBRY.Models
{
    public partial class InvoiceContext : DbContext
    {
        public InvoiceContext()
        {
        }

        public InvoiceContext(DbContextOptions<InvoiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDescription> InvoiceDescriptions { get; set; }
        public virtual DbSet<InvoiceImage> InvoiceImages { get; set; }
        public virtual DbSet<Subcontractor> Subcontractors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-298GPG1;Database=Invoice;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(52)
                    .IsFixedLength(true);

                entity.Property(e => e.AptUnitOrSuit)
                    .HasMaxLength(10)
                    .HasColumnName("Apt_Unit_Or_Suit")
                    .IsFixedLength(true);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .HasColumnName("Company_Name")
                    .IsFixedLength(true);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(52)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("First_Name")
                    .IsFixedLength(true);

                entity.Property(e => e.HstAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("HST_Amount");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Last_Name")
                    .IsFixedLength(true);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(15)
                    .HasColumnName("Middle_Name")
                    .IsFixedLength(true);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(14)
                    .HasColumnName("Phone_Number")
                    .IsFixedLength(true);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.InvoiceId).HasColumnName("Invoice_Id");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.IsPaid).HasColumnName("Is_Paid");

                entity.Property(e => e.NumberOfHours)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Number_Of_Hours");

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SubTotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Sub_Total");

                entity.Property(e => e.SubcontractorId).HasColumnName("Subcontractor_Id");

                entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Total_Amount");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Invoices");

                entity.HasOne(d => d.Subcontractor)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.SubcontractorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Subcontractors");
            });

            modelBuilder.Entity<InvoiceDescription>(entity =>
            {
                entity.HasKey(e => e.DescriptionId);

                entity.ToTable("Invoice_Descriptions");

                entity.Property(e => e.DescriptionId).HasColumnName("Description_Id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsFixedLength(true);

                entity.Property(e => e.InvoiceId).HasColumnName("Invoice_Id");
            });

            modelBuilder.Entity<InvoiceImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.ToTable("Invoice_Image");

                entity.Property(e => e.ImageId).HasColumnName("Image_Id");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(52)
                    .IsFixedLength(true);

                entity.Property(e => e.InvoiceId).HasColumnName("Invoice_Id");
            });

            modelBuilder.Entity<Subcontractor>(entity =>
            {
                entity.Property(e => e.SubcontractorId).HasColumnName("Subcontractor_Id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(52)
                    .IsFixedLength(true);

                entity.Property(e => e.AptUnitOrSuit)
                    .HasMaxLength(10)
                    .HasColumnName("Apt_Unit_Or_Suit")
                    .IsFixedLength(true);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .HasColumnName("Company_Name")
                    .IsFixedLength(true);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(52)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("First_Name")
                    .IsFixedLength(true);

                entity.Property(e => e.HstAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("HST_Amount");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Last_Name")
                    .IsFixedLength(true);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(15)
                    .HasColumnName("Middle_Name")
                    .IsFixedLength(true);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(14)
                    .HasColumnName("Phone_Number")
                    .IsFixedLength(true);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
