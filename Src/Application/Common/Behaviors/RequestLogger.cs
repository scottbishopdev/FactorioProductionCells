using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using FactorioProductionCells.Application.Common.Interfaces;

namespace FactorioProductionCells.Application.Common.Behaviors
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _identityService;

        public RequestLogger(
            ILogger logger,
            ICurrentUserService currentUserService,
            IUserService identityService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.GetCurrentUser().Id;
            var userName = await _identityService.GetUserNameAsync(userId);

            _logger.LogInformation($"Received request: Type: {requestName} UserId: {userId} UserName: {userName} Request: {request}");
            //_logger.LogInformation($"Received request: Type: {requestName} UserId: {userId} Request: {request}");
        }
    }
}
