using System.Collections;
using DDNSUpdater.APIs.Ionos.ApiClient;
using DDNSUpdater.APIs.Ionos.ApiClient.Models;
using DDNSUpdater.Models;
using DDNSUpdater.Services.Interfaces;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using RestSharp;
using Method = Microsoft.Kiota.Abstractions.Method;

namespace DDNSUpdater.Services;

public class DDNSUpdater : IDDNSUpdater
{

    private IonosAPIClient _client;
    
    public DDNSUpdater()
    {
        var authProvider  = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authProvider);
        _client = new IonosAPIClient(requestAdapter);
    }


    public async void Update(List<string> updateUrLs)
    {
        if(updateUrLs != null && updateUrLs.Count != 0)
        
        foreach (var UpdateURL in updateUrLs)
        {
            try
            {
                var client = new RestClient(UpdateURL);
                var request = new RestRequest("",RestSharp.Method.Get);
                request.AddHeader("Cookie", "0b04270753322c986927738ac2b6c0d8=ea099cbd8a6109c688f9831d6bbfa7a1; 5b66c83e4535f5f6bef8295496cfe559=e85228fccae97f107478bf9ef664e4eb; DPX=v1:ghOJrOzFTj:htgOaKFW:63d3bf8f:de");
                var body = @"";
                request.AddParameter("text/plain", body,  ParameterType.RequestBody);
                
                var response = await client.ExecuteAsync(request);
                
                Console.WriteLine(response.ErrorMessage);
            }
            catch (ApiException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

    }

    public List<string> GetUpdateUrLs(List<Domain> domains)
    {
        List<string> updateUrLs = new List<string>();
        if (updateUrLs == null) throw new ArgumentNullException(nameof(updateUrLs));

        Dictionary<string, List<string>> domainDict = new Dictionary<string, List<string>>();
        foreach (var domain in domains)
        {
            
            if (!domainDict.ContainsKey(domain.Key))
            {
                domainDict.Add(domain.Key, new List<string>());
            }
            
            domainDict[domain.Key].Add(domain.DomainString);
        }
        

        foreach (var (key, value) in domainDict)
        {
            try
            {
                var request = new DynDnsRequest
                {
                    Domains = value,
                    Description = "My DynamicDns"
                };

                var reply = _client.V1.Dyndns.PostAsync(request, configuration =>
                {
                    configuration.Headers = new RequestHeaders()
                    {
                        { "X-API-Key", key }
                    };
                }).Result;
                if (reply is { UpdateUrl: { } }) updateUrLs.Add(reply.UpdateUrl);
            }
            catch (ApiException error)
            {
                string message = error.Message;
                Console.WriteLine(message);
            }
            
            
        }

        return updateUrLs;
    }
}