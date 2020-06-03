/*
using System;
using System.Collections.Generic;
using AutoMapper;
using FactorioProductionCells.Application.Common.Mappings;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Mods.Queries.GetModViewModel
{
    public class ModDto : IMapFrom<Mod>
    {
        public String Name { get; set; }
        public IList<ModTitleDto> Titles { get; set; } = new List<ModTitleDto>();
        public IList<ReleaseDto> Releases { get; set; } = new List<ReleaseDto>();

        public void Mapping(Profile profile)
        {
            // TODO: Figure out how this works. I'm going to have to make extensive use of custom mappers.
            profile.CreateMap<Mod, ModDto>().ForMember(md => md.Name, opt => opt.MapFrom(m => m.Name));
        }
    }
}
*/