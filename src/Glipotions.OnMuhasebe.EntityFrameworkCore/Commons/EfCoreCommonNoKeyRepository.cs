using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Glipotions.OnMuhasebe.Commons;
/// <Özet>
/// Tanım: No Key olarak oluşturmadığımız EfCommonRepository de key olarak Guid kullandık bunda Key kullanmayacağımız için
/// tek Generic var o da TEntity
/// Kullanım Alanı: Özellikle Raporlarda Kullanacaz.
/// Video (1/5) 68. Video dk 65
/// <typeparam name="TEntity"></typeparam>
public class EfCoreCommonNoKeyRepository<TEntity> : EfCoreRepository<OnMuhasebeDbContext, TEntity>,
    ICommonNoKeyRepository<TEntity> where TEntity : class, IEntity
{
    public EfCoreCommonNoKeyRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
    /// <Özet>
    /// Tanım: Tek bir kayıt döndürecek.
    /// <param name="sql"></param>              // Sql sorgusu veya prosedür gönderilir.
    /// <param name="parameters"></param>       // Parametre gönderilir.
    public async Task<TEntity> FromSqlRawSingleAsync(string sql, params object[] parameters)
    {
        var dbSet = await GetDbSetAsync();
        return (await dbSet.FromSqlRaw(sql, parameters).ToListAsync()).FirstOrDefault();
    }

    /// <Özet>
    /// <param name="sql"></param>              // Sql sorgusu veya prosedür gönderilir.
    /// <param name="parameters"></param>       // Parametre gönderilir.
    public async Task<IList<TEntity>> FromSqlRawAsync(string sql, params object[] parameters)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FromSqlRaw(sql, parameters).ToListAsync();
    }
}