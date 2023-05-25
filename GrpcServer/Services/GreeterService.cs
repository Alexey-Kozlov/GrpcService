using Grpc.Core;
using GrpcService;

namespace GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task<SomeMethodResp> SomeCallMethod(SomeMethodReq request, ServerCallContext context)
        {
            var textResponse = request.ParamText + "!!!!";
            var numberResponse = request.ParamNumber * 2;
            var result = new SomeMethodResp();
            result.ParamText = textResponse;
            result.ParamNumber = numberResponse;
            return await Task.FromResult(result);

        }
    }
}