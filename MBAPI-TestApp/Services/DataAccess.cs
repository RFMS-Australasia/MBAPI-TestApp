using MeasureBoostApi.Model;
using Newtonsoft.Json;

namespace MBAPI_TestApp.Services
{
    public class DataAccess
    {
        private static List<Customer> _customers = new List<Customer>();

        public DataAccess() { 
            if (_customers.Count == 0) {
                var basePath = AppContext.BaseDirectory;
                var filePath = Path.Combine(basePath, "Services", "customers.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    _customers = JsonConvert.DeserializeObject<List<Customer>>(json) ?? new List<Customer>();
                }
            }
        }

        internal List<Customer> CustomerSearch(string searchString)
        {
            return _customers.Where(c => CustomerContainsString(c, searchString)).ToList();
        }

        internal bool CustomerContainsString(Customer customer, string searchString) {
            var lower = searchString.Trim().ToLower();
            if (customer.SoldTo.FirstName.ToLower().Contains(lower)) return true; 
            if (customer.SoldTo.LastName.ToLower().Contains(lower)) return true; 
            if (customer.SoldTo.BusinessName.ToLower().Contains(lower)) return true; 
            if (customer.ShipTo.FirstName.ToLower().Contains(lower)) return true; 
            if (customer.ShipTo.LastName.ToLower().Contains(lower)) return true; 
            if (customer.ShipTo.BusinessName.ToLower().Contains(lower)) return true; 
            return false;
        }
    }
}
