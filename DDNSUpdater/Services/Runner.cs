using DDNSUpdater.Models;
using DDNSUpdater.Services.Interfaces;
using Newtonsoft.Json;

namespace DDNSUpdater.Services;

public class Runner : IRunner
{
    public readonly Request _request;
    public readonly IDDNSUpdater _ddnsUpdater;
    public Reply _reply;
    
    
    public Runner(Request request, IDDNSUpdater ddnsUpdater)
    {
        _request = request;
        _ddnsUpdater = ddnsUpdater;
        _reply = new Reply();
    }

    public async void ProcessRequest()
    {
        var updateUrLs = await _ddnsUpdater.GetUpdateUrLs(_request.Domains);
        while (updateUrLs == null || updateUrLs.Count == 0 )
        {
            updateUrLs = await _ddnsUpdater.GetUpdateUrLs(_request.Domains);
        }

        for (int j = 0; j < 3; j++)
        {
            _ddnsUpdater.Update(updateUrLs);
            Thread.Sleep(3000);
        }
        
        ReplyToMessage();
    }

    public void ReplyToMessage()
    {
        Console.WriteLine(JsonConvert.SerializeObject(_reply));
    }
    
}