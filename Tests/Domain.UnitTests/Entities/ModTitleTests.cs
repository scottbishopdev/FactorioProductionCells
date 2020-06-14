using System;
using System.Collections.Generic;
using Xunit;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.UnitTests.Entities
{
    public class ModTitleTests
    {
        private Mod _testMod;
        //private Guid _languageGuid;

        public ModTitleTests()
        {
            _testMod = new Mod(
                Name: "testMod",
                Titles: new List<ModTitle>(),
                Releases: new List<Release>()
            );

            _testMod.TryAddTitle(
                languageId: Guid.NewGuid(),
                title: "Scott's Super Test Mod"
            );

            var testDependencies = new List<Dependency>();
            testDependencies.Add(Dependency.For("base >= 0.14.0"));
            testDependencies.Add(Dependency.For("angelsConfig >= 0.1.2"));

            _testMod.TryAddRelease(
                releasedAt: DateTime.Parse("6/1/2020 3:24:52 AM"),
                sha1: "b53320182d7e53b81b1009d8353492614f9a5cac",
                downloadUrl: ReleaseDownloadUrl.For("/download/testMod/5a5f1ae6adcc441024d72e83"),
                releaseFileName: ReleaseFileName.For("testMod_0.1.9.zip"),
                version: ModVersion.For("0.1.9"),
                factorioVersion: FactorioVersion.For("0.17"),
                dependencies: testDependencies
            );

            //_languageGuid = Guid.NewGuid();
        

        }
        
        
        [Theory]
        [InlineData(null, "382c74c3-721d-4f34-80e5-57657b6cbc27", "Scott's Uber Test GeModden")]
        [InlineData("DEWIT", null, "Scott's Uber Test GeModden")]
        [InlineData("DEWIT", "382c74c3-721d-4f34-80e5-57657b6cbc27", null)]
        public void ModTitle_WhenConstructorParameterIsNull_ThrowsArgumentNullException(String dewIt, Guid languageId, String title)
        {
            Mod testMod = null;

            if(dewIt == "DEWIT")
            {

                testMod = _testMod;
            }
            
            Assert.Throws<ArgumentNullException>(() => new ModTitle(
                Mod: testMod,
                LanguageId: languageId,
                Title: "Scott's Uber Test GeModden"));
        }
    }
}
