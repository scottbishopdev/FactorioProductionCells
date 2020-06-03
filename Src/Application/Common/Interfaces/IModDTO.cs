using System;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Common.Interfaces
{
    public interface IModDTO
    {
        void PrintMod();
        
        Mod ToDbMod();
    }
}
