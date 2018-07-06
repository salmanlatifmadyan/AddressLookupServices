using Karakoram.Address.Lookup.API.Models;
using Karakoram.Address.Lookup.API.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Karakoram.Address.Lookup.API.Contracts
{
    public static class Common
    {
        /// <summary>
        /// Get Tenant Configuration
        /// All the settings here will come from Database later
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public static Tenant GetTenantWithConfiguration(int tenantId)
        {
            // creating dummy tenants data along with configuration
            var tenantsList = new List<Tenant>
            {
                new Tenant
                {
                    TenantId = 1,
                    Name = "Tenant 1",
                    Phone = "123",
                    Config = new TenantConfiguration
                    {
                        TenantId = 1,
                        Preference = ServiceType.Loqate,
                        //APIKEY = "1234-5678-9012-3456"
                    }
                },
                new Tenant
                {
                    TenantId = 2,
                    Name = "Tenant 2",
                    Phone = "456",
                    Config = new TenantConfiguration
                    {
                        TenantId = 2,
                        Preference = ServiceType.CraftyClicks
                    }
                },
                new Tenant
                {
                    TenantId = 3,
                    Name = "Tenant 3",
                    Phone = "789",
                    Config = new TenantConfiguration
                    {
                        TenantId = 3,
                        Preference = ServiceType.SmartyStreets,
                        //AuthToken = "9877-1254-5323-6423",
                        //AuthId = "asga3t3sdgs3dgs"
                    }
                },
                new Tenant
                {
                    TenantId = 4,
                    Name = "Tenant 4",
                    Phone = "147",
                    Config = new TenantConfiguration
                    {
                        TenantId = 4,
                        Preference = ServiceType.PostCoder,
                        //APIKEY = "0987-6543-2109-8765"
                    }
                },
                new Tenant
                {
                    TenantId = 5,
                    Name = "Tenant 5",
                    Phone = "258"
                }
            };

            // finding the the tenant
            var findTenant = tenantsList.FirstOrDefault(a => a.TenantId == tenantId);

            return findTenant;
        }

        /// <summary>
        /// Get lookup service based on client configuration
        /// </summary>
        /// <param name="sType"></param>
        /// <returns></returns>
        public static string GetLookupService(ServiceType sType)
        {
            switch (sType)
            {
                case ServiceType.Loqate:
                    return "LoqateAddressLookupService";
                case ServiceType.CraftyClicks:
                    return "CraftyClicksAddressLookupService";
                case ServiceType.SmartyStreets:
                    return "SmartyStreetsAddressLookupService";
                case ServiceType.PostCoder:
                    return "PostCoderAddressLookupService";
                case ServiceType.Default:
                default:
                    return "MockupAddressLookupService";
            }
        }
    }
}
