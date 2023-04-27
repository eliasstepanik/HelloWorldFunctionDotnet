using DDNSUpdater.Models;
using DDNSUpdater.Services;
using DDNSUpdater.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

string inputJson = "";
#if !DEBUG
    inputJson = Console.In.ReadToEnd();
#else
    inputJson = JsonConvert.SerializeObject(new Request
    {
        Domains = new List<Domain>()
        {
            new Domain("test.sailehd.systems","4dc281058e9648919a988315c84058fa.z0eKvfJSuUpeU-2W-quUCsM_6aSshAX8tdPrJ1NQUBtcaImOtoQCk82nT4kDWzBjj2l2PMo1vGXCc6vGW9bKHA")
        }
    });

#endif

var input = JsonConvert.DeserializeObject<Request>(inputJson);

var builder = new ServiceCollection();

builder.AddSingleton(input ?? throw new InvalidOperationException());
builder.AddSingleton<IDDNSUpdater,DDNSUpdater.Services.DDNSUpdater>();
builder.AddSingleton<IRunner, Runner>();

var app = builder.BuildServiceProvider();

var runner = app.GetService<IRunner>();
runner.ProcessRequest();