using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Karakoram.Address.Lookup.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientRateLimitController : ControllerBase
    {
        private readonly ClientRateLimitOptions _options;
        private readonly IClientPolicyStore _clientPolicyStore;

        public ClientRateLimitController(IOptions<ClientRateLimitOptions> optionsAccessor, IClientPolicyStore clientPolicyStore)
        {
            _options = optionsAccessor.Value;
            _clientPolicyStore = clientPolicyStore;
        }

        [HttpGet]
        public ClientRateLimitPolicy Get()
        {
            return _clientPolicyStore.Get($"{_options.ClientPolicyPrefix}_cl-key-1");
        }

        [HttpPost]
        public void Post()
        {
            var id = $"{_options.ClientPolicyPrefix}_cl-key-1";
            var clPolicy = _clientPolicyStore.Get(id);
            clPolicy.Rules.Add(new RateLimitRule
            {
                Endpoint = "*/api/testpolicyupdate",
                Period = "1h",
                Limit = 100
            });
            _clientPolicyStore.Set(id, clPolicy);
        }
    }
}