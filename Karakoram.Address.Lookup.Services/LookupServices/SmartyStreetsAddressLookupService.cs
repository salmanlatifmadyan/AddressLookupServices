using System.Collections.Generic;
using System.Threading.Tasks;
using Karakoram.Address.Lookup.Services.Interfaces;
using Karakoram.Address.Lookup.Services.Models;

namespace Karakoram.Address.Lookup.Services.LookupServices
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
