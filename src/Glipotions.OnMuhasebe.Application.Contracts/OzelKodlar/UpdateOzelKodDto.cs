using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.OzelKodlar;

public class UpdateOzelKodDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}