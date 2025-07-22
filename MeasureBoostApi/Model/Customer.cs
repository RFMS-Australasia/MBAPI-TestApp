namespace MeasureBoostApi.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Source { get; set; } = "Customer";

        public Address SoldTo { get; set; } = new Address();
        public Address ShipTo { get; set; } = new Address();
        public List<string> PhoneNumbers { get; set; } = new List<string>();
        public List<string> EmailAddresses { get; set; } = new List<string>();
        public List<string> Salespeople { get; set; } = new List<string>();
    }

    public class Address 
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string BusinessName { get; set; } = "";
        public string AddressLine1 { get; set; } = "";
        public string AddressLine2 { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string PostalCode { get; set; } = "";
    }
}
