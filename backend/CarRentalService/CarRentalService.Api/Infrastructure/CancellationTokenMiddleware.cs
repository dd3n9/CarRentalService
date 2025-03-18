using MediatR;

namespace CarRentalService.Api.Infrastructure
{
    public class CancellationTokenMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<CancellationTokenMiddleware<TRequest, TResponse>> _logger;

        public CancellationTokenMiddleware(ILogger<CancellationTokenMiddleware<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var response = await next();
                return response;
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                _logger.LogWarning("Request {Request} was cancelled.", typeof(TRequest).Name);
                throw;
            }
        }
    }
}
