using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace FactorioProductionCells.Application.Common.Interfaces.ModPortalService
{
    public interface IModPortalService
    {
        Task<IList<IModDTO>> GetAllMods();

        Task<IModDTO> GetModByName(string modName);
    }
}
