using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace ModUpdateScheduler.Tests
{    
    public class ModServiceTests
    {
        private const String modPortalUrl = "https://mods.factorio.com/api/";

        [Fact]
        public async Task GetModNameList_WhenCalled_ReturnsPopulatedList()
        {
            const String modListResponse = "{\"pagination\":null,\"results\":[{\"name\":\"custommodpleaseignore\",\"title\":\"custommodpleaseignore\",\"owner\":\"avocado_assassin\",\"summary\":\"ignore\",\"downloads_count\":51,\"latest_release\":{\"download_url\":\"/download/custommodpleaseignore/5a5f1ae6adcc441024d7364b\",\"file_name\":\"custommodpleaseignore_0.1.0.zip\",\"info_json\":{\"factorio_version\":\"0.15.2\"},\"released_at\":\"2017-04-26T16:18:40.748000Z\",\"version\":\"0.1.0\",\"sha1\":\"2ed45677aac9392fbd33d59a643a1badae5a28c3\"}},{\"name\":\"Coal To Heavy Oil Update\",\"title\":\"Crack Coal to Heavy Update\",\"owner\":\"nemreg09\",\"summary\":\"Adds a recipe that can crack Coal into Heavy Oil at a Chemical Plant.\",\"downloads_count\":643,\"latest_release\":{\"download_url\":\"/download/Coal%20To%20Heavy%20Oil%20Update/5a5f1ae6adcc441024d74015\",\"file_name\":\"Coal To Heavy Oil Update_0.1.1.zip\",\"info_json\":{\"factorio_version\":\"0.15\"},\"released_at\":\"2017-08-23T13:28:49.909000Z\",\"version\":\"0.1.1\",\"sha1\":\"3fcff0f9e645ccb6b7395a36eee1807a46fafa84\"}},{\"name\":\"Rooms\",\"title\":\"Rooms scenario\",\"owner\":\"DaCluwn\",\"summary\":\"\",\"downloads_count\":445,\"category\":\"general\",\"latest_release\":{\"download_url\":\"/download/Rooms/5a957d59921968000bdf41a0\",\"file_name\":\"Rooms_0.1.0.zip\",\"info_json\":{\"factorio_version\":\"0.16\"},\"released_at\":\"2018-02-27T15:46:33.608000Z\",\"version\":\"0.1.0\",\"sha1\":\"954f3765cd89961b00e3a1b37607fc9ffde8827a\"}}]}";
            
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK);
            fakeResponse.Content = new StringContent(modListResponse);
            
            // arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(fakeResponse);

            var modService = new ModService(new HttpClient(mockMessageHandler.Object));

            // act
            var modList = await modService.GetAllMods();

            // assert
            Assert.NotEmpty(modList);
        }
    }
}
