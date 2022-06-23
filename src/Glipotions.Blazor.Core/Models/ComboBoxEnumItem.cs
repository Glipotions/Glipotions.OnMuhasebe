using System;

namespace Glipotions.Blazor.Core.Models;

/// <ÖZET>
/// 
/// Comboboxlarda Enumların gözükmesi için bu class yapıldı.
/// Sadece Enum Gönderilebilir.
/// <typeparam name="TEnum"></typeparam>
public class ComboBoxEnumItem<TEnum> where TEnum : Enum
{
    public TEnum Value { get; set; }
    public string DisplayName { get; set; }
}