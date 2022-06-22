using Microsoft.AspNetCore.Components;

namespace Glipotions.Blazor.Core.Components.Dev.DataViewers.Base;

public abstract class DevBaseViewer : ComponentBase
{
    [Parameter] public int ColumnIndex { get; set; } = 0;
    [Parameter] public int ColumnSpan { get; set; } = 0;
    [Parameter] public int RowIndex { get; set; } = 0;
    [Parameter] public int RowSpan { get; set; } = 0;
}