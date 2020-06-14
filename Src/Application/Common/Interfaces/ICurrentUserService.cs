using System;

namespace FactorioProductionCells.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid GetCurrentUserId();

        String GetCurrentUserName();
    }
}
