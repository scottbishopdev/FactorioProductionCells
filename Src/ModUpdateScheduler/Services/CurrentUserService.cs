using System;
using FactorioProductionCells.Application.Common.Interfaces;

namespace FactorioProductionCells.ModUpdateScheduler.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        // TODO: It sickens me to implement this service this way, but it's all I can manage right now. I feel like I ought to be retreiving the ModUpdateUser's Id from the database
        // directly, since we don't really have a need to know it precisely (we ought to be able to allow it to be automatically generated). Thing is, the hellhole of circular
        // dependencies and the identity framework has worn down my resolve, and this project is in serious jeopardy of not being completed if I continue down that path.
        public Guid GetCurrentUserId()
        {
            return new Guid(Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_ID"));
        }

        public String GetCurrentUserName()
        {
            return Environment.GetEnvironmentVariable("MODUPDATESCHEDULER_USERNAME");
        }
    }
}
