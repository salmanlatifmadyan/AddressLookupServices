using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Karakoram.Address.Lookup.API.Contracts;
using Karakoram.Address.Lookup.API.Models.Enums;
using Karakoram.Address.Lookup.Services.Interfaces;
using Karakoram.Address.Lookup.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Karakoram.Address.Lookup.API.Controllers
{
    /// <summary>
    /*
     * Two more things to think about please:
     *
     * How do we prevent abuse of the service, e.g. rate limiting or has to be authenticated user (I prefer at least later for now).
     * Rate limiting may be possible from existing library or NuGet package, I'm certain someone else has already solved this problem.
     * 
     * and
     * 
     * Can we cache results to save on cost, could do this later if needed.
     */
    /// </summary>
    [Route("api/Lookup")]
    [ApiController]
    [ResponseCache(Duration = 30)]
    public class LookupController : ControllerBase
    {
        private IMemoryCache _cache;

        public LookupController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        /// <summary>
        /// Find API
        /*
         * We need to...
         * Use our tenant config to see:
         * (a) Which service the tenant wants to use -- or if they have no preference use our default rules(where do these rules come from)?
         * (b) If the tenant has uploaded their own API key, retrieve that and pass to service, otherwise use our API keys
         * 
         * At the end of this, we should know which IAddressLookupService we are using. Possibly reflection and
         * IAddressLookup service = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance("Karakoram.Address.Lookup.LoqateAddressLookupService");
         * 
         * service.Find(..);.
         */
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="countryCode"></param>
        /// <param name="query"></param>
        /// <param name="container"></param>
        /// <param name="limit"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Find")]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<IActionResult> Find(int tenantId, string countryCode, string query, string container, string language, int limit = 50)
        {            
            try
            {
                var result = new List<LoqateFindResult>();

                #region Cache Key

                var cacheKey = "Find_" + tenantId;

                if (!string.IsNullOrEmpty(countryCode))
                {
                    cacheKey += "_" + countryCode;
                }
                if (!string.IsNullOrEmpty(query))
                {
                    cacheKey += "_" + query;
                }
                if (!string.IsNullOrEmpty(container))
                {
                    cacheKey += "_" + container;
                }
                if (!string.IsNullOrEmpty(language))
                {
                    cacheKey += "_" + language;
                }
                if (limit > 0)
                {
                    cacheKey += "_" + limit;
                }

                #endregion

                // Look for cache key.
                if (!_cache.TryGetValue(cacheKey, out result))
                {
                    // Key not in cache, so get data.
                    var tenantConfig = Common.GetTenantConfiguration(tenantId);
                    var lookupService = Common.GetLookupService(tenantConfig?.Preference ?? ServiceType.Default);

                    #region Dictionary

                    var dict = new Dictionary<string, string>();

                    dict.Add("countries", countryCode);
                    dict.Add("text", query);

                    if (tenantConfig != null && !string.IsNullOrEmpty(tenantConfig.APIKEY))
                    {
                        dict.Add("key", tenantConfig.APIKEY);
                    }

                    if (!string.IsNullOrEmpty(container))
                    {
                        dict.Add("container", container);
                    }

                    if (limit > 0)
                    {
                        dict.Add("limit", limit.ToString());
                    }

                    if (!string.IsNullOrEmpty(language))
                    {
                        dict.Add("language", language);
                    }

                    #endregion

                    String serviceFullName = "Karakoram.Address.Lookup.Services.LookupServices." + lookupService;
                    Assembly assem = typeof(IAddressLookupService).Assembly;
                    IAddressLookupService lookServ = (IAddressLookupService)assem.CreateInstance(serviceFullName, true);

                    if (lookServ == null)
                        return BadRequest("Something went wrong");

                    lookServ.Initialise(dict);
                    result = await lookServ.Find();

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                    // Save data in cache.
                    _cache.Set(cacheKey, result, cacheEntryOptions);
                }                

                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieve API
        /*
         * Similar to the above, but doing retrieve not find
         * However, assuming the service bills us on this call, we need a separate
         * call where we can call AddressLookupBilling.BillLookup(tenantCode, service, user, date, ...)
         * Again, handle failures and timeout gracefully
         */
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Retrieve")]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<IActionResult> Retrieve(int tenantId, string id)
        {
            try
            {
                var result = new LoqateRetrieveResult();

                var cacheKey = "Retrieve_" + tenantId + "_" + id;

                // Look for cache key.
                if (!_cache.TryGetValue(cacheKey, out result))
                {
                    // Key not in cache, so get data.
                    var tenantConfig = Common.GetTenantConfiguration(tenantId);
                    var lookupService = Common.GetLookupService(tenantConfig?.Preference ?? ServiceType.Default);

                    #region Dictionary

                    var dict = new Dictionary<string, string>();

                    if (tenantConfig != null && !string.IsNullOrEmpty(tenantConfig.APIKEY))
                    {
                        dict.Add("key", tenantConfig.APIKEY);
                    }

                    dict.Add("limit", "10");
                    dict.Add("id", id);

                    #endregion

                    String serviceFullName = "Karakoram.Address.Lookup.Services.LookupServices." + lookupService;
                    Assembly assem = typeof(IAddressLookupService).Assembly;
                    IAddressLookupService lookServ = (IAddressLookupService)assem.CreateInstance(serviceFullName, true);

                    if (lookServ == null)
                        return BadRequest("Something went wrong");

                    lookServ.Initialise(dict);
                    result = result = await lookServ.Retrive();

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                    // Save data in cache.
                    _cache.Set(cacheKey, result, cacheEntryOptions);
                }

                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Other APIs

        //// GET api/lookup
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/lookup/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/Lookup
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/lookup/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/lookup/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        #endregion
    }
}
