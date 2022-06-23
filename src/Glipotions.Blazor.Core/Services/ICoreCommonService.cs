using System;
using Microsoft.AspNetCore.Components;

namespace Glipotions.Blazor.Core.Services;

/// <ÖZET>
/// 
/// HasChanged = Değişikliği Ekrana Hemen Yansıtan Görev.
/// ActiveEditComponent= "Şu an üzerinde işlem yaptığımız component"
public interface ICoreCommonService
{
    public Action HasChanged { get; set; }
    public ComponentBase ActiveEditComponent { get; set; }
}