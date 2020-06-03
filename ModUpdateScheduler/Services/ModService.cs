using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace ModUpdateScheduler
{
    public interface IModService
    {
        Task<List<ModDTO>> GetAllMods();
    }
    
    public class ModService : IModService
    {
        // TODO: Implement logging. Previously had issues figuring out dependency injection.
        private const String modPortalUrl = "https://mods.factorio.com/api/";

        private readonly HttpClient _httpClient;
        //private readonly ILogger<ModService> _logger;

        public ModService(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ModDTO>> GetAllMods()
        {
            var builder = new UriBuilder(modPortalUrl + $"mods");
            builder.Query = "page_size=max";

            using(var response = await _httpClient.GetAsync(builder.Uri))
            {
                // TODO: Implement error handling in case we get a response we don't expect (e.g. check for 200 OK)
                String rawResponse = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(rawResponse);
                List<JToken> results = jsonResponse["results"].Children().ToList();

                List<ModDTO> modList = new List<ModDTO>();
                results.ForEach(result => modList.Add(result.ToObject<ModDTO>()));

                return modList;
            }
        }
    }
}
