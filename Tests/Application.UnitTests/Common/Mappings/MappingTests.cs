using System;
//using Moq;
using AutoMapper;
using Xunit;
using FactorioProductionCells.Application.Mods.Queries.GetMod;
using FactorioProductionCells.Domain.Entities;



namespace FactorioProductionCells.Application.UnitTests.Common.Mappings
{    
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        //private const String modPortalUrl = "https://mods.factorio.com/api/";

        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            // This is a test provided by AutoMapper that ensures that a valid configuration exists for all mapped types.
            _configuration.AssertConfigurationIsValid();
        }

        // TODO: What's the difference between a Theory and Fact?
        [Theory]
        [InlineData(typeof(Mod), typeof(ModDto))]
        // TODO: Why is the InlineData line duplicated here?
        [InlineData(typeof(Mod), typeof(ModDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}
