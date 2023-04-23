using HelloWorld.Scaffold;
using HelloWorld.Services;
using HelloWorld.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

TextReader inputJson = Console.In;
var input = JsonConvert.DeserializeObject<Request>(inputJson.ReadToEnd());

var builder = new ServiceCollection();

builder.AddSingleton(input ?? throw new InvalidOperationException());
builder.AddSingleton<IRunner, Runner>();

var app = builder.BuildServiceProvider();

var runner = app.GetService<IRunner>();
runner.ProcessRequest();