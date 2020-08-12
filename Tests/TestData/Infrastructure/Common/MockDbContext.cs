using System.Linq;
using MockQueryable.Moq;
using Moq;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.TestData.Domain.Entities;

namespace FactorioProductionCells.TestData.Infrastructure.Common
{
    public static class MockDbContext
    {
        public static IFactorioProductionCellsDbContext GetContext()
        {
            var mockLanguagesDbSet = LanguageTestData.LanguagesWithIds.AsQueryable().BuildMockDbSet();
            var mockModDbSet = ModTestData.AllMods.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<IFactorioProductionCellsDbContext>();
            mockContext.Setup(c => c.Languages).Returns(mockLanguagesDbSet.Object);
            mockContext.Setup(c => c.Mods).Returns(mockModDbSet.Object);

            return mockContext.Object;
        }
    }
}
