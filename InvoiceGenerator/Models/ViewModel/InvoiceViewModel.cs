namespace InvoiceGenerator.Models.ViewModel
{
    public class InvoiceViewModel
    {
        public Invoice Invoice { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
