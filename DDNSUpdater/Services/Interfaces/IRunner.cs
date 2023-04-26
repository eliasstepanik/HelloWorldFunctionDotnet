using DDNSUpdater.Models;

namespace DDNSUpdater.Services.Interfaces;

public interface IRunner
{
    public void ReplyToMessage();
    public void ProcessRequest();
}