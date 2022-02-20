using Glipotions.OnMuhasebe.Commons;
using Glipotions.OnMuhasebe.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Glipotions.OnMuhasebe.Raporlar;

public class EfCoreOdemeBelgeleriDagilimRepository : EfCoreCommonNoKeyRepository<OdemeBelgeleriDagilim>,
    IOdemeBelgeleriDagilimRepository
{
    public EfCoreOdemeBelgeleriDagilimRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
}