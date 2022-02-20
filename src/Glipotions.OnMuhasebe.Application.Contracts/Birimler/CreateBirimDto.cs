using System;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Birimler;

public class CreateBirimDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}