using BBIS.Application.DTOs.Lookup;

namespace BBIS.Application.Contracts
{
    public interface ILookupService
    {
        Task<List<LookupDto>> GetLookups(List<string> lookupKeys);
    }
}
