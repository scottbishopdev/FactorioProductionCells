using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using FactorioProductionCells.Application.Common.Interfaces;

namespace FactorioProductionCells.Application.Common.Behaviors
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<RequestLogger<TRequest>> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public RequestLogger(
            ILogger<RequestLogger<TRequest>> logger,
            ICurrentUserService currentUserService,
            IIdentityService identityService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Received request: Type: {typeof(TRequest).Name} Request: {request}");

            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.GetCurrentUserId();

            // TODO: Figure out the difference between using the CurrentUserService and the IdentityService here. I'm guessing that this will become apparent once the web app starts using this.
            //var userName = _currentUserService.GetCurrentUserName();
            var userName = await _identityService.GetUserNameFromIdAsync(userId);

            _logger.LogInformation($"Received request: Type: {typeof(TRequest).Name} Request: {request} UserId: {userId} UserName: {userName}");
        }
    }
}
