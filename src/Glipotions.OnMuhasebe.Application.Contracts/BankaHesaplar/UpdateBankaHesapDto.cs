using System;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.BankaHesaplar;

public class UpdateBankaHesapDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public BankaHesapTuru? HesapTuru { get; set; }
    public string HesapNo { get; set; }
    public string IbanNo { get; set; }
    public Guid? BankaSubeId { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    // Şube Id Değiştilememesi lazım olduğundan kaldırdık
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}