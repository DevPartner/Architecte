using CartService.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CartService.Application.Common.Behaviours;
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly IUser _user;

    public LoggingBehavior(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger,
        IUser user)
    {
        _logger = logger;
        _user = user;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _user.Id ?? string.Empty;
        var userRole = _user.Role ?? string.Empty;

        _logger.LogInformation("CartingService Request: {Name} {@UserId} {@UserRole} {@Request}",
            requestName, userId, userRole, request);

        return await next();
    }
}
