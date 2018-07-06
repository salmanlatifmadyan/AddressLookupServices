using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Karakoram.Address.Lookup.Services.Interfaces;
using Karakoram.Address.Lookup.Services.Models;


namespace Karakoram.Address.Lookup.Services.LookupServices
{
    //Use C sharp SDK if available, otherwise do direct rest calls, but discuss
    //details with Noor of how to make REST API
    //Remember to gracefully handle service outages on their end

    class SmartyStreetsAddressLookupService : IAddressLookupService
    {
        public Task<List<LoqateFindResult>> Find()
        {
            throw new NotImplementedException();
        }

        public void Initialise(Dictionary<string, string> intialisationData)
        {
            throw new NotImplementedException();
        }

        public Task<LoqateRetrieveResult> Retrive()
        {
            throw new NotImplementedException();
        }
    }
}
