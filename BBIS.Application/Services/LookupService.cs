using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Lookup;
using BBIS.Database;
using Microsoft.EntityFrameworkCore;

namespace BBIS.Application.Services
{
    public class LookupService: ILookupService
    {
        private readonly BBDbContext dbContext;

        public LookupService(BBDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<LookupDto>> GetLookups(List<string> lookupKeys)
        {
            var lookups = await dbContext.Lookups
                .Include(x => x.LookupOptions)
                .Where(x => x.IsActive 
                    && (lookupKeys != null && lookupKeys.Any() ? lookupKeys.Any(l => l == x.LookupKey) : true )
                 )
                .AsNoTracking()
                .ToListAsync();
                
            var results = lookups.Select(x => 
                new LookupDto { 
                    LookupKey = x.LookupKey,
                    Description = x.Description,
                    LookupId = x.LookupId,
                    LookupOptions = x.LookupOptions
                            .Select(lo => new LookupOptionDto { LookupOptionId = lo.LookupOptionId, Text = lo.Name, Value = lo.Value })
                            .ToList()
                }).ToList();

            return results;
        }
    }
}
