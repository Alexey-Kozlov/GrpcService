﻿using Grpc.Core.Interceptors;
using Grpc.Core;

namespace GrpcService
{
    public class ServerLoggerInterceptor : Interceptor
    {
        private readonly ILogger _logger;

        public ServerLoggerInterceptor(ILogger<ServerLoggerInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"Starting receiving call. Type: {MethodType.Unary}. " + $"Method: {context.Method}.");
            Console.WriteLine($"{DateTime.Now.ToUniversalTime()} - Поступил запрос. Type: {MethodType.Unary}. " + $"Method: {context.Method}.");
            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error thrown by {context.Method}.");
                throw;
            }
        }

       
    }
}
