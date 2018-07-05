using Karakoram.Address.Lookup.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Karakoram.Address.Lookup.Services.Interfaces
{
    public interface IAddressLookupService
    {
        void Initialise(Dictionary<string, string> intialisationData);

        Task<List<LoqateFindResult>> Find();

        Task<LoqateRetrieveResult> Retrive();
    }
}
