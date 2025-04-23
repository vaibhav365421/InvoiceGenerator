using InvoiceGenerator.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace InvoiceGenerator.Data
{
    public class AppicationDbContext : DbContext
    {
        public AppicationDbContext(DbContextOptions<AppicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
    }
}
