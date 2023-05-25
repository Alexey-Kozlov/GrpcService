using System.Threading.Tasks;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using GrpcClient;
using GrpcService;
using Newtonsoft.Json;

namespace GrpcClient
{
    public class Client
    {
        public Client() { }

        public async Task<T1> Call<T1,T2>(T2 request)
        {
            try
            {
                var channel = GrpcChannel.ForAddress("http://localhost:5181");
                //добавляем компонент в конвейер
                var interceptor = channel.Intercept(new LoggingInterceptor());
                //вызываем на выполнение уже этот компонент
                var client = new Greeter.GreeterClient(interceptor);
                dynamic? reply = null;                
                switch (typeof(T2))
                {
                    case Type HelloRequest when HelloRequest == typeof(HelloRequest):
                        reply = await client.SayHelloAsync((HelloRequest) Convert.ChangeType(request!, typeof(HelloRequest)));
                        HelloReply helloReply = (HelloReply)reply;
                        Console.WriteLine("Первый метод HelloReply - " + helloReply.Message);
                        break;
                    case Type SomeMethodReq when SomeMethodReq == typeof(SomeMethodReq):
                        reply = await client.SomeCallMethodAsync((SomeMethodReq)Convert.ChangeType(request!, typeof(SomeMethodReq)));
                        SomeMethodResp someMethodResp = (SomeMethodResp)reply;
                        Console.WriteLine("Второй метод SomeMethodResp - " + someMethodResp.ParamText);
                        break;
                    default:
                        break;
                }
                switch(typeof(T1))
                {
                    case Type HelloReply when HelloReply == typeof(HelloReply):
                        HelloReply helloReply = (HelloReply)reply!;
                        //делаем чтото полезное в случае типа HelloReply
                        break;
                    case Type SomeMethodResp when SomeMethodResp == typeof(SomeMethodResp):
                        SomeMethodResp someMethodResp = (SomeMethodResp)reply!;
                        //делаем чтото полезное в случае типа SomeMethodResp
                        break;
                    default:
                        break;
                }
                if (reply == null)
                    throw new Exception();
                return (T1) Convert.ChangeType(reply!, typeof(T1));

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception();
            }
        }
    }
}
