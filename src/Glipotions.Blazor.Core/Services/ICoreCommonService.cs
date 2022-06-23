using System;
using Microsoft.AspNetCore.Components;

namespace Glipotions.Blazor.Core.Services;

/// <ÖZET>
/// 
/// ActiveEditComponent= "Şu an üzerinde işlem yaptığımız component"
public interface ICoreCommonService
{
    public Action HasChanged { get; set; }
    public ComponentBase ActiveEditComponent { get; set; }
}