using Grpc.Core.Interceptors;
using Grpc.Core;

namespace GrpcService
{
    public class LoggingInterceptor : Interceptor
    {

        public LoggingInterceptor()
        {
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            Console.WriteLine($"Вызов клиента. Type: {context.Method.Type}. " + $"Method: {context.Method.Name}.");
            return continuation(request, context);
        }
    }
}
