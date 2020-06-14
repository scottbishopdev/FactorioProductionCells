using System;
using FactorioProductionCells.Application.Common.Interfaces;

namespace FactorioProductionCells.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }
    }
}
