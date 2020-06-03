using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Infrastructure.Services;

namespace FactorioProductionCells.Infrastructure.Services.ModPortalService
{
    public class ModDTO : IModDTO
    {
        private IDefaultLanguageService _defaultLanguageService;

        public String Name { get; set; }
        public String Title { get; set; }
        public String Owner { get; set; }
        public String Summary { get; set; }
        public Int32 DownloadsCount { get; set; }
        public String Thumbnail { get; set; }
        public List<ReleaseDTO> Releases { get; set; }
        public ReleaseDTO LatestRelease
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
            Console.WriteLine($"Mod.Downloads_Count: {this.DownloadsCount}");
            Console.WriteLine($"Mod.Thumbnail: {this.Thumbnail}");

            this.Releases.ForEach(n => n.PrintRelease());
        }

        public Mod ToDbMod()
        {
            var dbMod = new Mod
            {
                Name = this.Name
            };
            
            // TODO: We need to determine how to properly extract the mod's localized title and populate this list more appropriately.
            dbMod.Titles.Add(new ModTitle
            {
                //ModId = ?,
                LanguageId = _defaultLanguageService.GetDefaultLanguage().Id,
                Title = this.Title
            });
            
            this.Releases.ForEach(r => dbMod.Releases.Add(r.ToDbRelease()));

            return dbMod;
        }
    }
}
