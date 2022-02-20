using System;
using Glipotions.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.BankaHesaplar;

public class BankaHesapCodeParameterDto : IEntityDto, IDurum
{
    /// <Özet>
    /// ! Code Oluştururken !
    /// Databaseden Doğru kodu oluşturabilmek için bu 2 bilgi gerekli
    /// (1/5) 71.Video dk 22
    public Guid SubeId { get; set; }
    public bool Durum { get; set; }
}