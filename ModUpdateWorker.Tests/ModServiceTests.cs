using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace ModUpdateWorker.Tests
{    
    public class ModServiceTests
    {
        private const String modPortalUrl = "https://mods.factorio.com/api/";

        [Fact]
        public async Task GetModByName_WhenValidModIsRequested_ReturnsValidMod()
        {
            // arrange
            const String bobsLibraryModName = "boblibrary";
            // Note: The changelog field, along with most releases have been removed from the actual expected response in order to make this mock more managable.
            const String bobsLibraryResponse = "{\"category\": \"general\",\"created_at\": \"2016-06-27T23:24:50.620000Z\",\"description\": \"The core of all Bobmods!\r\nBasically, you need this to run the others.\r\n\r\n![](https://mods-data.factorio.com/assets/bb05186d4df93104ba33eac5cb4a28f379713ea3.png)\r\n\",\"downloads_count\": 1004176,\"github_path\": \"\",\"homepage\": \"https://forums.factorio.com/viewforum.php?f=51\",\"license\": {\"name\": \"Copyright. Redistribution. No editing.\",\"url\": \"https://forums.factorio.com/viewtopic.php?f=51&t=28573\"},\"name\": \"boblibrary\",\"owner\": \"Bobingabout\",\"releases\": [{\"download_url\": \"/download/boblibrary/5a5f1ae6adcc441024d72427\",\"file_name\": \"boblibrary_0.13.0.zip\",\"info_json\": {\"dependencies\": [\"base >= 0.13.0\"],\"factorio_version\": \"0.13\"},\"released_at\": \"2016-06-27T23:24:50.634000Z\",\"sha1\": \"ac301d43a1a30d9986ce43393e4489f601a90cec\",\"version\": \"0.13.0\"}, {\"download_url\": \"/download/boblibrary/5a5f1ae6adcc441024d73956\",\"file_name\": \"boblibrary_0.15.3.zip\",\"info_json\": {\"dependencies\": [\"base >= 0.15.0\"],\"factorio_version\": \"0.15\"},\"released_at\": \"2017-05-06T19:56:36.406000Z\",\"sha1\": \"a0d747867cc0e5654c4cb8886f931a55cf599615\",\"version\": \"0.15.3\"}, {\"download_url\": \"/download/boblibrary/5d081ab2c6df2f000d258283\",\"file_name\": \"boblibrary_0.17.4.zip\",\"info_json\": {\"dependencies\": [\"base >= 0.17.0\"],\"factorio_version\": \"0.17\"},\"released_at\": \"2019-06-17T22:56:50.602000Z\",\"sha1\": \"34343fd5a4ca40f4210cb36b130e4e2b9640cbf0\",\"version\": \"0.17.4\"}, {\"download_url\": \"/download/boblibrary/5e9ca5ad462d8e000dc3a438\",\"file_name\": \"boblibrary_0.18.8.zip\",\"info_json\": {\"dependencies\": [\"base >= 0.18.0\"],\"factorio_version\": \"0.18\"},\"released_at\": \"2020-04-19T19:25:33.181000Z\",\"sha1\": \"0373c1a02455b0f783c8f374dc293f3677b6738c\",\"version\": \"0.18.8\"}, {\"download_url\": \"/download/boblibrary/5e9daef69e85eb000cee2549\",\"file_name\": \"boblibrary_0.18.9.zip\",\"info_json\": {\"dependencies\": [\"base >= 0.18.0\"],\"factorio_version\": \"0.18\"},\"released_at\": \"2020-04-20T14:17:26.290000Z\",\"sha1\": \"4e804310d21fc04a9c11bef2c9b049770a2cffd3\",\"version\": \"0.18.9\"}],\"score\": -5075.833333333335,\"summary\": \"Adds a series of useful functions used by Bob's Mods. These can easilly be used by other mods too.\",\"tag\": {\"name\": \"general\"},\"thumbnail\": \"/assets/87388539faf250d8c93b1636480def5b1de3557f.thumb.png\",\"title\": \"Bob's Functions Library mod\",\"updated_at\": \"2020-04-20T14:17:26.301000Z\"}";

            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK);
            fakeResponse.Content = new StringContent(bobsLibraryResponse);

            var mockMessageHandler = new Mock<HttpMessageHandler>();
            // TODO: See if there's a way we can be more specific about the requests we're intercepting with this - e.g. only return this response when the right request is made to the proper uri.
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(fakeResponse);
                
            var modService = new ModService(new HttpClient(mockMessageHandler.Object));

            // act
            ModDTO bobsLibraryMod = await modService.GetModByName(bobsLibraryModName);

            // assert
            Assert.Equal(bobsLibraryModName, bobsLibraryMod.Name);
            Assert.NotNull(bobsLibraryMod.Title);
            Assert.NotNull(bobsLibraryMod.Owner);
            Assert.NotNull(bobsLibraryMod.Summary);
            //Assert.NotNull(bobsLibraryMod.Downloads_Count);
            Assert.NotNull(bobsLibraryMod.Thumbnail);
            
            Assert.NotEmpty(bobsLibraryMod.Releases);
            Assert.NotNull(bobsLibraryMod.Releases[0].DownloadURL);
            Assert.NotNull(bobsLibraryMod.Releases[0].FileName);
            //Assert.NotNull(bobsLibraryMod.Releases[0].Released_At);
            Assert.NotNull(bobsLibraryMod.Releases[0].Version);
            Assert.NotNull(bobsLibraryMod.Releases[0].Sha1);

            Assert.NotNull(bobsLibraryMod.Releases[0].InfoJson.FactorioVersion);
            Assert.NotEmpty(bobsLibraryMod.Releases[0].InfoJson.Dependencies);
        }

        [Fact]
        public async Task GetModByName_WhenInvalidModIsRequested_FailsGracefully()
        {
            // arrange
            const String nonexistantModName = "thisdoesntexist";
            const String nonexistantModResponse = "{\"message\": \"Mod not found\"}";

            var fakeResponse = new HttpResponseMessage(HttpStatusCode.NotFound);
            fakeResponse.Content = new StringContent(nonexistantModResponse);

            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(fakeResponse);

            var modService = new ModService(new HttpClient(mockMessageHandler.Object));

            // act
            // assert
            await Assert.ThrowsAsync<ModNotFoundException>(async () => await modService.GetModByName(nonexistantModName));
        }
    }
}
