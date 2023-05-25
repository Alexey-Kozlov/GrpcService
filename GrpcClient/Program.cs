using GrpcClient;

public partial class Program
{
    static void Main(string[] args)
    {
        var client = new Client();

        var req1 = new HelloRequest { Name = "GreeterClient" };
        var req2 = new SomeMethodReq { ParamNumber = 3.5, ParamText = "Чтото там" };
        var response1 = client.Call<HelloReply, HelloRequest>(req1).Result;
        var response2 = client.Call<SomeMethodResp, SomeMethodReq>(req2).Result;

    }
}

