using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace FactorioProductionCells.Application.Common.Interfaces
{
    public interface IModPortalService
    {
        Task<List<IModDTO>> GetAllMods();

        Task<IModDTO> GetModByName(string modName);
    }
}
