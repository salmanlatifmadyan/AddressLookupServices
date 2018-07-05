using System.Collections.Generic;
using System.Threading.Tasks;
using Madyan.Address.Lookup.Services.Interfaces;
using Madyan.Address.Lookup.Services.Models;

namespace Madyan.Address.Lookup.Services.LookupServices
{
    class SmartyStreetsAddressLookupService : IAddressLookupService
    {
        public void Initialise(Dictionary<string, string> intialisationData)
        {
            throw new System.NotImplementedException();
        }

        public Task<LoqateFindResult> Find()
        {
            throw new System.NotImplementedException();
        }

        public Task<LoqateRetrieveResult> Retrive()
        {
            throw new System.NotImplementedException();
        }
    }
}
