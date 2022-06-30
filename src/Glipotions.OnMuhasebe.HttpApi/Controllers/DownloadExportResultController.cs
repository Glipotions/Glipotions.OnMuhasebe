using DevExpress.Blazor.Reporting.Controllers;
using DevExpress.Blazor.Reporting.Internal.Services;

namespace Glipotions.OnMuhasebe.Controllers;

public class DownloadExportResultController : DownloadExportResultControllerBase
{
    /// <ÖZET>
    /// Raporlama için DevExpress Controller ı
    public DownloadExportResultController(ExportResultStorage exportResultStorage)
        : base(exportResultStorage)
    {
    }
}
