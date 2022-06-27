using System;
using System.Collections.Generic;

namespace Glipotions.Blazor.Core.Helpers;

public static class ExtensionFunctions
{
    /// <ÖZET>
    /// typeof(TEntity) ile TEntity nin tipi alınır.
    /// GetProperty("Id") ile id si bulunur.
    /// <returns> Geriye Id döndürülür.</returns> 
    /// Kullanıldığı yere örn: BaseListPage =>DeleteAsync
    public static Guid GetEntityId<TEntity>(this TEntity entity)
    {
        var property = typeof(TEntity).GetProperty("Id");
        return (Guid)property.GetValue(entity);
    }
    /// <ÖZET>
    /// 
    /// Silme işleminde bir sonraki satır'ın indexine geçmek için yapılmış fonksiyon.
    /// <returns></returns>
    public static TItem SetSelectedItem<TItem>(this IList<TItem> listDataSource,
        int index)
    {
        int nextIndex = index;

        if (index == listDataSource.Count)
            nextIndex = index == 0 ? 0 : index - 1;

        if (listDataSource.Count > 0)
            return listDataSource[nextIndex];

        return default;
    }
    /// <ÖZET>
    /// (3/5) 36. Video 44. Dk
    /// property info oluşturulur.
    /// entitiesler içinde foreach ile id değerini alıp gelen id ye eşitse geriyeo entityi döndürür.
    /// <returns></returns>
    public static TEntity GetEntityById<TEntity>(this IList<TEntity> entities, Guid id)
    {
        var propertyInfo = typeof(TEntity).GetProperty("Id");

        foreach (var entity in entities)
            if(propertyInfo.GetValue(entity).Equals(id))
                return entity;

        return default;
    }
}