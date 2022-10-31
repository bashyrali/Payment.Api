﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace Application.Common.Behavior
{
    public class LoggingBehavior<TRequest,TResponse>:IPipelineBehavior<TRequest,TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            Log.Information("Pay request: {RequestName} {@Request}",requestName,request);
            var response = await next();
            return response;
        }
    }
}