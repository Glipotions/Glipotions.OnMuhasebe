using System;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Kasalar;

public class UpdateKasaDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    // ŞubeId değiştilemeyeceği için burada yok.
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}