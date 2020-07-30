using System.Threading;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Application.Mods.Queries.GetModByName;

namespace FactorioProductionCells.Application.UnitTests.Mods.Queries
{
    public class GetModByNameQueryHandlerTests
    {
        [Fact]
        public async void GetModByNameQueryHandler_WhenGivenExistingName_ReturnsCorrectMod()
        {
            // TODO: I guess that maybe I should name this "sut" for System Under Test? I'm not sure if this is a reasonable practice or not.
            var getModByNameQueryHandler = new GetModByNameQueryHandler(new Mock<ILogger<GetModByNameQueryHandler>>().Object, MockDbContext.GetContext());

            var result = await getModByNameQueryHandler.Handle(new GetModByNameQuery{ Name = "Test Mod" }, CancellationToken.None);

            Assert.IsType<Mod>(result);
            Assert.Equal("Test Mod", result.Name);

            // Need to verify:
            //   - Object we get back is a Mod
            //   - It has the correct Releases
            //   - It has the correct Titles
            //   - it has the correct name
        }
    }
}
