using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Glipotions.OnMuhasebe.Commons;

//TEntity nin class olduğunu ve IEntity de Guid olma zorunluluğunu verdik
public interface ICommonRepository<TEntity> : IRepository<TEntity, Guid> 
    where TEntity : class, IEntity<Guid>
{
    /// <Özet>
    /// 
    /// </summary>
    /// <param name="id"></param>                   id gönderip Entity'i databaseden çağırır
    /// <param name="predicate"></param>            filtreleme(zorunlu değil),
    /// <param name="includeProperties"></param>    Entity'i databaseden çağırırken hangi navigation propertieslerin çağırılacağını belirtiriz 
    /// <returns></returns>
    Task<TEntity> GetAsync(object id, Expression<Func<TEntity, bool>> predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties);

    /// <Özet>
    /// Tanım: id göndermek istemediğimizde bunu kullanacaz.
    /// </summary>
    /// <param name="predicate"></param>            filtreleme( zorunlu değil)    
    /// <param name="includeProperties"></param>    Entity'i databaseden çağırırken hangi navigation propertieslerin çağırılacağını belirtiriz 
    /// <returns></returns>
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties);

    /// <Özet>
    /// Tanım: Bazı entityler ICollection olduğu için iç içe navigation propertyler olacak bu yüzden buna ihtiyaç var
    /// <param name="id"></param>                id gönderilecek
    /// <param name="predicate"></param>        filtreleme var(zorunluluk yok),
    Task<TEntity> GetAsync(object id, Expression<Func<TEntity, bool>> predicate = null);

    /// <Özet>
    /// Tanım: Kaç Kayıt Getirilecekse BAŞTAN İtibaren Listemesini sağlayan Fonksiyon
    /// <param name="skipCount"></param>            kaç veri atlanacak belirtilir -> 0 dersek baştan itibaren
    /// <param name="maxResultCount"></param>       maksimum kaç kayıt gelecek belirtilir.
    /// <param name="predicate"></param>            filtre olarak istediğimiz navigation propertyler gelir
    /// <param name="orderBy"></param>              Hangi Alana göre sıralanacağı belirtilir -> x=x.Kod
    /// <param name="includeProperties"></param>    Hangi Navigation Propertylerin geleceğini tek tek belirterek yazarız
    Task<List<TEntity>> GetPagedListAsync<TKey>(int skipCount, int maxResultCount,
        Expression<Func<TEntity, bool>> predicate = null,
        Expression<Func<TEntity, TKey>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includeProperties);

    /// <Özet>
    /// Tanım: Kaç Kayıt Getirilecekse Baştan İtibaren Listemesini sağlayan Fonksiyon
    /// ICollection tipinde navigation propertisi olan entityler için bu kullanılır.
    Task<List<TEntity>> GetPagedListAsync<TKey>(int skipCount, int maxResultCount,
        Expression<Func<TEntity, bool>> predicate = null,
        Expression<Func<TEntity, TKey>> orderBy = null);

    /// <Özet>
    /// Tanım: Kaç Kayıt Getirilecekse SONDAN İtibaren Listemesini sağlayan Fonksiyon
    /// <param name="skipCount"></param>            kaç veri atlanacak belirtilir -> 0 dersek baştan itibaren
    /// <param name="maxResultCount"></param>       maksimum kaç kayıt gelecek belirtilir.
    /// <param name="predicate"></param>            filtre olarak istediğimiz navigation propertyler gelir
    /// <param name="orderBy"></param>              Hangi Alana göre sıralanacağı belirtilir -> x=x.Kod
    /// <param name="includeProperties"></param>    Hangi Navigation Propertylerin geleceğini tek tek belirterek yazarız
    Task<List<TEntity>> GetPagedLastListAsync<TKey>(int skipCount, int maxResultCount,
        Expression<Func<TEntity, bool>> predicate = null,
        Expression<Func<TEntity, TKey>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includeProperties);
    
    /// <Özet>
    /// Tanım: Hangi Kartın Kodunu Almak istiyorsak O entity'e gider..
    /// ..ilgili kod alanının en büyük değerini alır 1 ekleyip geri bize getirir
    /// </summary>
    /// <param name="propertySelector"></param
    /// <param name="predicate"></param>
    Task<string> GetCodeAsync(Expression<Func<TEntity, string>> propertySelector,
        Expression<Func<TEntity, bool>> predicate = null);

    /// <Özet>
    /// Tanım: Raporlarda kullanılacak. Store prosedürlerin databaseden çekilmesi için kullanılır.
    /// Geriye TEntity olarak bir liste döndürür
    /// <param name="sql"></param>          Store prosedür ismi veya Sql sorgusu
    /// <param name="parameters"></param>   Varsa parametreleri gönderilir.
    Task<IList<TEntity>> FromSqlRawAsync(string sql, params object[] parameters);

    /// <Özet>
    /// Tanım: Varsa Databasede değer vardır yoksa yoktur işlevini görür
    /// <param name="predicate"></param>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);
}