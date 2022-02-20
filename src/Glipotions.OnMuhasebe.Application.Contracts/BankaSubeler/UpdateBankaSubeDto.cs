using System;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.BankaSubeler;

public class UpdateBankaSubeDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    // Bir banka şube belirli bir banka şubeye bağlı olduğundan bunu değiştiremeyiz.
    // Bu yüzden Burada BankaId prop yok.
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}