using System;
using System.Collections.Generic;

namespace Glipotions.Blazor.Core.Helpers;

public static class ExtensionFunctions
{
    public static Guid GetEntityId<TEntity>(this TEntity entity)
    {
        var property = typeof(TEntity).GetProperty("Id");
        return (Guid)property.GetValue(entity);
    }

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

    public static TEntity GetEntityById<TEntity>(this IList<TEntity> entities, Guid id)
    {
        var propertyInfo = typeof(TEntity).GetProperty("Id");

        foreach (var entity in entities)
            if(propertyInfo.GetValue(entity).Equals(id))
                return entity;

        return default;
    }
}