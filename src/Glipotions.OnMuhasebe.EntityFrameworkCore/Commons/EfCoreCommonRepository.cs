using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Glipotions.OnMuhasebe.Commons;

public class EfCoreCommonRepository<TEntity> : EfCoreRepository<OnMuhasebeDbContext, TEntity, Guid>,
    ICommonRepository<TEntity> where TEntity : class, IEntity<Guid>
{
    public EfCoreCommonRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<TEntity> GetAsync(object id, Expression<Func<TEntity, bool>> predicate = null,
    params Expression<Func<TEntity, object>>[] includeProperties)
    {
        /*  Dataları getir, getirirken de includeProperties olarak vermiş olduğumuz..
            ..Navigation Propertyleri dolu olarak getirme işlemini yapar
            Örnek quaryable = SELECT * FROM Customer. */
        var queryable = await WithDetailsAsync(includeProperties);

        // Entity aldık
        TEntity entity;

        // predicate null değilse filtre gönderilmiştir.
        if (predicate != null)
        {
            /*  Sorgu sonucu geriye dönen değer null ise First yaparsak hata alırız..
                ..ama FirstOrDefault  null dönünce veriyi null olarak gösterir yani hata vermez.
                queryable a sorgu eklenmiş olur Örnek SELECT * FROM Customer WHERE Id>5 */
            entity = await queryable.FirstOrDefaultAsync(predicate);
            if (entity == null)
                /*  Entity boş olursa Entity bulunamadı hatası gönderilir.
                    TEntity ve id Gönderilmesinin nedeni Hata dönerken..
                    "TEntity tipindeki verinizin id si bulunamadı" diye belirtmektir.*/
                throw new EntityNotFoundException(typeof(TEntity), id);
            // Hata yoksa return ettirerek aşağıdaki işlemlerin yapılmasını önledik.
            return entity;
        }

        entity = await queryable.FirstOrDefaultAsync();
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);
        return entity;
    }

    /// <Özet>
    /// Tanım: id olmadan veri getirme işlemi.
    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var queryable = await WithDetailsAsync(includeProperties);

        if (predicate != null)
            return await queryable.FirstOrDefaultAsync(predicate);

        return await queryable.FirstOrDefaultAsync();
    }

    public async Task<TEntity> GetAsync(object id, Expression<Func<TEntity, bool>> predicate = null)
    {
        // ICollection Tipindekileri Verileri Listelerken Collectionların içindeki..
        // ..Navigation Property lerin dolu olmasını sağlar.
        var queryable = await WithDetailsAsync();

        TEntity entity;

        // Bu kısım Yukarıda Anlatıldı!
        if (predicate != null)
        {
            entity = await queryable.FirstOrDefaultAsync(predicate);
            if (entity == null)
                throw new EntityNotFoundException(typeof(TEntity), id);
            return entity;
        }

        entity = await queryable.FirstOrDefaultAsync();
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);
        return entity;
    }

    public async Task<List<TEntity>> GetPagedListAsync<TKey>(int skipCount, int maxResultCount,
        Expression<Func<TEntity, bool>> predicate = null,
        Expression<Func<TEntity, TKey>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        /*  includeProperties lere göre Data getirir. Yukarıda Daha detaylı Anlatıldı!.
            Örnek SELECT * FROM Customer .*/
        var queryable = await WithDetailsAsync(includeProperties);

        if (predicate != null)
            /*  Filtre Uygulandı.
                !!! Henüz Query nin çalışması için bir şey yapılmadı bu kod sorguya ek yapar.
                Örnek SELECT * FROM Customer WHERE Id>5.
                Şeklinde WHERE Id>5 eklendi ama uygulanmadı.*/
            queryable = queryable.Where(predicate);

        if (orderBy != null)
            /*  Aynı şekilde Order By Uygulanır ve Sorguya Eklenir Yine Çalıştırılmaz!
                Örnek SELECT * FROM Customer WHERE Id>5 ORDER BY Code */
            queryable = queryable.OrderBy(orderBy);

        /*  Skip ilk skipCount kadar kayıtı atla
            Take kaç kayıt olacağını maxResultCount ile Belirtilir.
            ToListAsync ile Asenkron olarak geriye liste döndürür.*/
        return await queryable
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<List<TEntity>> GetPagedListAsync<TKey>(int skipCount, int maxResultCount,
        Expression<Func<TEntity, bool>> predicate = null,
        Expression<Func<TEntity, TKey>> orderBy = null)
    {
        // includePreperty olmadığı için uygulanmadı. Geri kalan kodlar yukarıda anlatıldı!
        var queryable = await WithDetailsAsync();

        if (predicate != null)
            queryable = queryable.Where(predicate);

        if (orderBy != null)
            queryable = queryable.OrderBy(orderBy);

        return await queryable
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    /// <Özet>
    /// 2 üstteki ile Tamamen Aynı sadece Order by Descending olarak Belirtildi.
    public async Task<List<TEntity>> GetPagedLastListAsync<TKey>(int skipCount, int maxResultCount,
        Expression<Func<TEntity, bool>> predicate = null,
        Expression<Func<TEntity, TKey>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var queryable = await WithDetailsAsync(includeProperties);

        if (predicate != null)
            queryable = queryable.Where(predicate);

        if (orderBy != null)
            queryable = queryable.OrderByDescending(orderBy);

        return await queryable
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    /// <Özet>
    /// Tanım: Hangi Kartın Kodunu Almak istiyorsak O entity'e gider..
    /// ..ilgili kod alanının en büyük değerini alır 1 ekleyip geri bize getirir
    /// <param name="propertySelector"></param          Hangi alanın arttılacağı belirtilir.
    /// <param name="predicate"></param>
    public async Task<string> GetCodeAsync(Expression<Func<TEntity, string>> propertySelector,
        Expression<Func<TEntity, bool>> predicate = null)
    {
        static string CreateNewCode(string code)
        {
            var number = "";

            foreach (var character in code)
            {
                if (char.IsDigit(character))    // karakter rakam mı? kontrolü
                    number += character;        // rakamsa number 1 arttırılır
                else
                    number = "";
            }

            // Yeni bir sayısal değer oluşturma işlemleri
            var newNumber = number == "" ? "1" : (long.Parse(number) + 1).ToString();
            var difference = code.Length - newNumber.Length;
            if (difference < 0)
                difference = 0;
             
            // (1/5) 68. Video 54. DK
            var newCode = code.Substring(0, difference);
            newCode += newNumber;

            return newCode;
        }

        var dbSet = await GetDbSetAsync();
        // MaxAsync -> En büyük kodu getirir.
        // Where kodu ile neye göre artacağı belirlenir
        var maxCode = predicate == null ?
            await dbSet.MaxAsync(propertySelector) :
            await dbSet.Where(predicate).MaxAsync(propertySelector);

        // İlk kez oluşturunca 0000000000000001 ile başlatır.
        return maxCode == null ? "0000000000000001" : CreateNewCode(maxCode);
    }

    /// <Özet>
    /// Tanım: Genellikle Raporlarda kullanılacak. Store prosedürlerin databaseden çekilmesi için kullanılır.
    /// Geriye TEntity olarak bir liste döndürür
    /// <param name="sql"></param>          Store prosedür ismi veya Sql sorgusu
    /// <param name="parameters"></param>   Varsa parametreleri gönderilir.
    public async Task<IList<TEntity>> FromSqlRawAsync(string sql, params object[] parameters)
    {
        var context = await GetDbContextAsync();        
        return await context.Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync();
    }

    /// <Özet>
    /// Tanım: Varsa Databasede değer vardır yoksa yoktur işlevini görür
    /// <param name="predicate"></param>
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        var dbSet = await GetDbSetAsync();
        return predicate == null ? await dbSet.AnyAsync() : await dbSet.AnyAsync(predicate);
    }
}