
using Madyan.Address.Lookup.API.Models.Enums;

namespace Madyan.Address.Lookup.API.Models
{
    public class TenantConfiguration
    {
        // creat a primary key using both fields

        public int TenantId { get; set; }

        public ServiceType Preference { get; set; } // = ServiceType.Default;

        public string APIKEY { get; set; } // = "XXXX-XXXX-XXXX-XXXX"; // by default use our api key

        public string AuthId { get; set; } // = "XXXX-XXXX-XXXX-XXXX"; // by default use our auth id

        public string AuthToken { get; set; } // = "XXXX-XXXX-XXXX-XXXX"; // by default use our auth token
    }
}
