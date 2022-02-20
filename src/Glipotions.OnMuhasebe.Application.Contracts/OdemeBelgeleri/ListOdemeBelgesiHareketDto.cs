using System;
using Glipotions.OnMuhasebe.Makbuzlar;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.OdemeBelgeleri;

public class ListOdemeBelgesiHareketDto : EntityDto<Guid>
{
    public string MakbuzNo { get; set; }
    public Guid BankaHesapId { get; set; }
    public Guid MakbuzId { get; set; }
    public DateTime Tarih { get; set; }
    public OdemeTuru OdemeTuru { get; set; }
    public string OdemeTuruAdi { get; set; }
    public string TakipNo { get; set; }
    public string BelgeNo { get; set; }
    public MakbuzTuru MakbuzTuru { get; set; }
    public string MakbuzTuruAdi { get; set; }
    public BelgeDurumu BelgeDurumu { get; set; }
    public string BelgeDurumuAdi { get; set; }
    public decimal Tutar { get; set; }
    public string Aciklama { get; set; }
}