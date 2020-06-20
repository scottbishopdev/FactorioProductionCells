using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FactorioProductionCells.Application.Common.Interfaces.ModPortalService;
using FactorioProductionCells.Infrastructure.Services.ModPortalService.DTOs;
using FactorioProductionCells.Domain.Exceptions;

namespace FactorioProductionCells.Infrastructure.Services.ModPortalService
{
    public class ModPortalService : IModPortalService
    {
        private const String modPortalUrl = "https://mods.factorio.com/api/";

        private readonly HttpClient _httpClient;
        private readonly ILogger<ModPortalService> _logger;

        public ModPortalService(
            ILogger<ModPortalService> logger)
        {
            _logger = logger;
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

                _logger.LogInformation($"Successfully retrieved {modList.Count} mods from the mod portal API.");

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
                // TODO: Clean this mess up and implement some proper error handling in case we get a response we don't expect.
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
