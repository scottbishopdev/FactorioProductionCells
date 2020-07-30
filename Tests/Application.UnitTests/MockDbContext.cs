using System.Linq;
using System.Collections.Generic;
using MockQueryable.Moq;
using Moq;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.UnitTests.Entities;
using FactorioProductionCells.Application.Common.Interfaces;

namespace FactorioProductionCells.Application.UnitTests
{
    public static class MockDbContext
    {
        public static IFactorioProductionCellsDbContext GetContext()
        {
            var languageData = new List<Language>
            {
                LanguageTests.EnglishWithId,
                LanguageTests.GermanWithId
            };
            var mockLanguagesDbSet = languageData.AsQueryable().BuildMockDbSet();

            var modData = new List<Mod>
            {
                ModTests.TestMod,
            };
            var mockModDbSet = modData.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<IFactorioProductionCellsDbContext>();
            mockContext.Setup(c => c.Languages).Returns(mockLanguagesDbSet.Object);
            mockContext.Setup(c => c.Mods).Returns(mockModDbSet.Object);

            return mockContext.Object;
        }

    }
}
