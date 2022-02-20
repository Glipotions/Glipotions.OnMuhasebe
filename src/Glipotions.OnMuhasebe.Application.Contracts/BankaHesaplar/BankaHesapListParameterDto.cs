using System;
using Glipotions.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.BankaHesaplar;

public class BankaHesapListParameterDto : PagedResultRequestDto, IEntityDto, IDurum
{
    /// <Özet>
    /// ! List Yaparken !
    /// 
    public BankaHesapTuru? HesapTuru { get; set; }
    public Guid SubeId { get; set; }
    public bool Durum { get; set; }
}