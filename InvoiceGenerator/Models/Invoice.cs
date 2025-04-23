using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(25)]
        public string InvoiceCode { get; set; }
        public DateTime InvoiceDate {  get; set; }
        public DateTime DueDate  { get; set; }
        [MaxLength(50)]
        public string CustomerName { get; set; }
        [MaxLength(25)]
        public string CustomerPhone { get; set; }
        [MaxLength(25)]
        public string CustomerAddress { get; set; }
        public string Notes { get; set; }
        public float TotalAmount { get; set; }
        public float AmountPaid { get; set; }
        public float BalanceDue { get; set; }
        [MaxLength(25)]
        public string Status  { get; set; }

    }
}
