using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Glipotions.OnMuhasebe.Commons;

/// <Özet>
/// Tanım: No Key olarak oluşturmadığımız ICommonRepository de key olarak Guid kullandık bunda Key kullanmayacağımız için
/// tek Generic var o da TEntity
/// Kullanım Alanı: Özellikle Raporlarda Kullanacaz.
/// Video (1/5) 67. Video dk 16
/// <typeparam name="TEntity"></typeparam>
public interface ICommonNoKeyRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <Özet>
    /// Tanım: Tek bir kayıt döndürecek.
    /// <param name="sql"></param>              // Sql sorgusu veya prosedür gönderilir.
    /// <param name="parameters"></param>       // Parametre gönderilir.
    Task<TEntity> FromSqlRawSingleAsync(string sql, params object[] parameters);

    /// <Özet>
    /// <param name="sql"></param>              // Sql sorgusu veya prosedür gönderilir.
    /// <param name="parameters"></param>       // Parametre gönderilir.
    Task<IList<TEntity>> FromSqlRawAsync(string sql, params object[] parameters);
}