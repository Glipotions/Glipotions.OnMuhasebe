using System;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Depolar;

public class CreateDepoDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public Guid? SubeId { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}