using System;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Application.Common.Interfaces
{
    public interface IDefaultLanguageService
    {
        Language GetDefaultLanguage();
    }
}
