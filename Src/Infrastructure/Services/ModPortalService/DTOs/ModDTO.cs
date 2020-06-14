using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Application.Common.Interfaces.ModPortalService;

namespace FactorioProductionCells.Infrastructure.Services.ModPortalService.DTOs
{
    public class ModDTO /*: IMapFrom<Mod>*/ : IModDTO
    {
        private IDefaultLanguageService _defaultLanguageService;

        public String Name { get; set; }
        public String Title { get; set; }
        public String Owner { get; set; }
        public String Summary { get; set; }
        public Int32 DownloadsCount { get; set; }
        public String Thumbnail { get; set; }
        public IList<IReleaseDTO> Releases { get; set; } = new List<IReleaseDTO>();
        public IReleaseDTO LatestRelease
        {
            get
            {
                return Releases.OrderBy(r => r.ReleasedAt).Single();
            }
            set
            {
                if(!Releases.Any())
                {
                    Releases.Add(value);
                }
                else
                {
                    throw new ArgumentException("This Mod already contains one or more releases, so LatestRelease cannot be set.");
                }
            }
        }

        public ModDTO(
            IDefaultLanguageService defaultLanguageService)
        {
            _defaultLanguageService = defaultLanguageService;
        }

        public void PrintMod()
        {
            Console.WriteLine($"Mod.Name: {this.Name}");
            Console.WriteLine($"Mod.Title: {this.Title}");
            Console.WriteLine($"Mod.Owner: {this.Owner}");
            Console.WriteLine($"Mod.Summary: {this.Summary}");
            Console.WriteLine($"Mod.DownloadsCount: {this.DownloadsCount}");
            Console.WriteLine($"Mod.Thumbnail: {this.Thumbnail}");

            List<IReleaseDTO> releases = (List<IReleaseDTO>)this.Releases;

            releases.ForEach(n => n.PrintRelease());
        }

        /*
        public Mod ToDbMod()
        {
            Guid someModId = Guid.NewGuid();
            // TODO: We need to determine how to properly extract the mod's localized title and populate this list more appropriately.
            // TODO: How can I create these things without a ModId? We're createing DB entities from data, so how can we have this ID before the record is created?
            var dbTitles = new List<ModTitle>();
            dbTitles.Add(new ModTitle(
                ModId: someModId,
                LanguageId: _defaultLanguageService.GetDefaultLanguage().Id,
                Title: this.Title
            ));
            
            var dbReleases = new List<Release>();
            foreach (var release in this.Releases)
            {
                // TODO: 
                dbReleases.Add(release.ToDbRelease(someModId));
            }

            return new Mod(
                Name: this.Name,
                Titles: dbTitles,
                Releases: dbReleases
            );
        }
        */
    }
}
