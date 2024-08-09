namespace AdSystem.DTO
{
    public class CompanyAdRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal ItemPrice { get; set; }
        public string Name { get; set; }
        public string OrganizationNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string BillingAddress { get; set; }
        public string BillingPostalCode { get; set; }
        public string BillingCity { get; set; }
    }
}