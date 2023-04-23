using HelloWorld.Scaffold;
using HelloWorld.Services.Interfaces;
using Newtonsoft.Json;

namespace HelloWorld.Services;

public class Runner : IRunner
{
    public readonly Request _request;
    public Reply _reply;
    
    
    public Runner(Request request)
    {
        _request = request;
        _reply = new Reply();
    }

    public void ProcessRequest()
    {
        Random rd = new Random();
        _reply.Name = _request.Name + " Nom";
        _reply.Number = rd.Next(1000);
        ReplyToMessage();
    }

    public void ReplyToMessage()
    {
        Console.WriteLine(JsonConvert.SerializeObject(_reply));
    }
    
}