﻿using DDNSUpdater.Models;
using DDNSUpdater.Services;
using DDNSUpdater.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

string inputJson = "";
#if DEBUG
    inputJson = Console.In.ReadToEnd();
#else
    inputJson = JsonConvert.Ser
    
#endif

var input = JsonConvert.DeserializeObject<Request>(inputJson);

var builder = new ServiceCollection();

builder.AddSingleton(input ?? throw new InvalidOperationException());
builder.AddSingleton<IDDNSUpdater,DDNSUpdater.Services.DDNSUpdater>();
builder.AddSingleton<IRunner, Runner>();

var app = builder.BuildServiceProvider();

var runner = app.GetService<IRunner>();
runner.ProcessRequest();