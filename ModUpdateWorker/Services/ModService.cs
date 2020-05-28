using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ModUpdateWorker
{
    public class ModService
    {
        private const String modPortalUrl = "https://mods.factorio.com/api/";

        private readonly HttpClient _httpClient;
        //private readonly ILogger<ModService> _logger;

        public ModService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ModDTO> GetModByName(string modName)
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

                ModDTO newMod = JsonConvert.DeserializeObject<ModDTO>(rawResponse);
                return newMod;
            }
        }
    }

    public class ModNotFoundException : Exception
    {
        public ModNotFoundException() {}

        public ModNotFoundException(string message) : base(message) {}

        public ModNotFoundException(string message, Exception inner) : base(message, inner) {}
    }
}
