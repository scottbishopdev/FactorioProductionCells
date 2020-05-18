using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ModFetcher
{
    public class ModService
    {
        private const String modPortalUrl = "https://mods.factorio.com/api/";

        private readonly HttpClient _httpClient;

        public ModService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Mod> GetModByName(string modName)
        {
            var builder = new UriBuilder(modPortalUrl + $"mods/{modName}/full");
            
            using(var response = await _httpClient.GetAsync(builder.Uri))
            {
                String rawResponse = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(rawResponse);

                JToken message;
                // TODO: See if this nonsense can be condensed down into a single if. I might not be able to because of the TryGetValue() call.
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

                Mod newMod = JsonConvert.DeserializeObject<Mod>(rawResponse);
                return newMod;
            }
        }

        public async Task<List<String>> GetModNameList()
        {
            var builder = new UriBuilder(modPortalUrl + $"mods");
            builder.Query = "page_size=max";

            using(var response = await _httpClient.GetAsync(builder.Uri))
            {
                // TODO: Implement error handling in case we get a response we don't expect (e.g. check for 200 OK)
                
                String rawResponse = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(rawResponse);

                var modNames =
                    from m in jsonResponse["results"]
                    select (String)m["name"];

                return new List<String>(modNames);
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