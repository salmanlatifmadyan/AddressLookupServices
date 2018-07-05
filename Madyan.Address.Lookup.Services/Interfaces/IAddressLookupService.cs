using Madyan.Address.Lookup.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Madyan.Address.Lookup.Services.Interfaces
{
    public interface IAddressLookupService
    {
        void Initialise(Dictionary<string, string> intialisationData);

        Task<LoqateFindResult> Find();

        Task<LoqateRetrieveResult> Retrive();
    }
}
