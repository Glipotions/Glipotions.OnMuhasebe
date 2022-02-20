using System.Linq.Expressions;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Exceptions;
using Volo.Abp.Domain.Repositories;

namespace Glipotions.OnMuhasebe.Extensions;

public static class EntityAsyncExtensions
{
    /// <Özet>
    /// Buraya bir kod gönderilir, Kod kullanılmışsa dublicateCodeException hatası yollar.
    /// 
    /// <param name="kod"></param>          ilk gönderilen kod sadece mesaj için
    /// <param name="predicate"></param>    predicate ile gönderilen kod ise x=>x.kod==kod şeklinde kontrol eder.                
    /// <param name="check"></param>        her halükarda check etmesi için true verildi.
    /// <exception cref="DuplicateCodeException"></exception>   kod parametresinde gelen kodu hata olarak fırlatır.
    public static async Task KodAnyAsync<TEntity>(this IReadOnlyRepository<TEntity> repository,
        string kod, Expression<Func<TEntity, bool>> predicate, bool check = true)
        where TEntity : class, IEntity
    {
        if (check && await repository.AnyAsync(predicate))
            throw new DuplicateCodeException(kod);
    }

    public static async Task EntityAnyAsync<TEntity>(
        this IReadOnlyRepository<TEntity> repository, object id,
        Expression<Func<TEntity, bool>> predicate, bool check = true)
        where TEntity : class, IEntity
    {
        if (check && id != null)
        {
            var anyAsync = await repository.AnyAsync(predicate);

            if (!anyAsync)
                throw new EntityNotFoundException(typeof(TEntity), id);
        }
    }

    /// <Özet>
    /// Özel kod ile çok işlem yapıldığı için ayrı yaptık.
    /// <exception cref="EntityNotFoundException"></exception>
    public static async Task EntityAnyAsync(
        this IReadOnlyRepository<OzelKod> repository, Guid? id, OzelKodTuru kodTuru,
        KartTuru kartTuru, bool check = true)
    {
        if (check && id != null)
        {
            // Gelen id değeri, kod türü ve kart türü database de varsa, böyle bir özel kod var demektir.
            var anyAsync = await repository.AnyAsync(x => x.Id == id &&
                                                          x.KodTuru == kodTuru &&
                                                          x.KartTuru == kartTuru);

            // yukardaki işlem yoksa hata fırlatması gerekir.
            if (!anyAsync)
                throw new EntityNotFoundException(typeof(OzelKod), id);
        }
    }

    /// <Özet>
    /// silinen entitye bağlı başka entityler varsa silmeyi engelleyecek fonksiyondur.
    /// <exception cref="ConnotBeDeletedException"></exception>
    public static async Task RelationalEntityAnyAsync<TEntity>(this IReadOnlyRepository<TEntity> repository,
        Expression<Func<TEntity, bool>> predicate)
        where TEntity : class, IEntity
    {
        var anyAsync = await repository.AnyAsync(predicate);

        if (anyAsync)
            throw new ConnotBeDeletedException();
    }
}