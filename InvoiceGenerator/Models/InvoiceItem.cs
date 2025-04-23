using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Models
{
    public class InvoiceItem
    {
      [Key]
      public int Id { get; set; }
      [MaxLength(255)]
      public string ItemName {  get; set; }
      public int Quantity {  get; set; }

      public float Price { get; set; }
      public float Amount {  get; set; }
      public Invoice Invoice {  get; set; }
        
    }
}
