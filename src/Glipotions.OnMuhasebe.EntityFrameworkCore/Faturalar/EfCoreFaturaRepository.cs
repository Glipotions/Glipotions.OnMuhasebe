using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Commons;
using Glipotions.OnMuhasebe.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Glipotions.OnMuhasebe.Faturalar;

public class EfCoreFaturaRepository : EfCoreCommonRepository<Fatura>, IFaturaRepository
{
    public EfCoreFaturaRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
    /// <Özet>
    /// (2/5) 13. Video 42.dk
    /// </summary>
    /// <returns></returns>
    public override async Task<IQueryable<Fatura>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .Include(x => x.Cari)
            .Include(x => x.OzelKod1)
            .Include(x => x.OzelKod2)

            // Sadece Depo Include edilir.
            .Include(x => x.FaturaHareketler).ThenInclude(x => x.Depo)

            // Sadece Stok'un Birim'i Include edilir.
            .Include(x => x.FaturaHareketler).ThenInclude(x => x.Stok)
                                             .ThenInclude(x => x.Birim)

            // Sadece Hizmet'in Birim'i Include edilir.
            .Include(x => x.FaturaHareketler).ThenInclude(x => x.Hizmet)
                                             .ThenInclude(x => x.Birim)

            // Sadece Masraf'ın Birim'i Include edilir.
            .Include(x => x.FaturaHareketler).ThenInclude(x => x.Masraf)
                                             .ThenInclude(x => x.Birim);
    }
}