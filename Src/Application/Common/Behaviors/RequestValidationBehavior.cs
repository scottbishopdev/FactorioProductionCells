using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FluentValidation;
using MediatR;
using ValidationException = FactorioProductionCells.Application.Common.Exceptions.ValidationException;

namespace FactorioProductionCells.Application.Common.Behaviors
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<RequestLogger<TRequest>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        private RequestValidationBehavior(
            ILogger<RequestLogger<TRequest>> logger,
            IEnumerable<IValidator<TRequest>> validators)
        {
            _logger = logger;
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Validating request: Type: {typeof(TRequest).Name} Request: {request}");   
            
            var context = new ValidationContext(request);

            // TODO: This is somehow trying to find validators for the request. How? It's not doing any reflection or anything...
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if(failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return next();
        }
    }
}
