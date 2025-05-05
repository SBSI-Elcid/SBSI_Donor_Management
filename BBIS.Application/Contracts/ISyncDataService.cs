namespace BBIS.Application.Contracts
{
    public interface ISyncDataService
    {
        Task<string> SyncDonors();
    }
}
