using DDNSUpdater.Models;

namespace DDNSUpdater.Services.Interfaces;

public interface IDDNSUpdater
{
    public Task<List<string>> GetUpdateUrLs(List<Domain> domains);
    public void Update(List<string> updateUrLs);
}