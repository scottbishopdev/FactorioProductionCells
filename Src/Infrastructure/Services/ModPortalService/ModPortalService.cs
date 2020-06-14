using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Application.Common.Interfaces.ModPortalService;
using FactorioProductionCells.Infrastructure.Services.ModPortalService.DTOs;
using FactorioProductionCells.Domain.Exceptions;

namespace FactorioProductionCells.Infrastructure.Services.ModPortalService
{
    public class ModPortalService : IModPortalService
    {
        // TODO: Implement logging.
        // TODO: It feels like this implementation should be down in the infrastructure layer, but it's living up here. Why?
        private const String modPortalUrl = "https://mods.factorio.com/api/";

        private readonly HttpClient _httpClient;
        //private readonly ILogger<ModService> _logger;

        public ModPortalService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IList<IModDTO>> GetAllMods()
        {
            var builder = new UriBuilder(modPortalUrl + $"mods");
            builder.Query = "page_size=max";

            using(var response = await _httpClient.GetAsync(builder.Uri))
            {
                // TODO: Implement error handling in case we get a response we don't expect (e.g. check for 200 OK)
                String rawResponse = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(rawResponse);
                List<JToken> results = jsonResponse["results"].Children().ToList();

                IList<IModDTO> modList = new List<IModDTO>();
                results.ForEach(result => modList.Add(result.ToObject<ModDTO>()));

                return modList;
            }
        }

        public async Task<IModDTO> GetModByName(string modName)
        {
            var builder = new UriBuilder(modPortalUrl + $"mods/{modName}/full");
            
            using(var response = await _httpClient.GetAsync(builder.Uri))
            {
                String rawResponse = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(rawResponse);

                JToken message;
                // TODO: See if this mess can be condensed down into a single if. I might not be able to because of the TryGetValue() call.
                if(jsonResponse.ContainsKey("message"))
                {
                    if(jsonResponse.TryGetValue("message", out message))
                    {
                        if(message.ToString() == "Mod not found")
                        {
                            throw new ModNotFoundException($"The mod ${modName} was not found in the Factorio mod portal.");
                        }
                    }
                }

                return JsonConvert.DeserializeObject<ModDTO>(rawResponse);
            }
        }
    }
}
