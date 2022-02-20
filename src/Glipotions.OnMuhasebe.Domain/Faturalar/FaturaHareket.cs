namespace Glipotions.OnMuhasebe.Faturalar;

//FullAuditedEntity olmasının nedeni faturaya bağlı bir entity olduğu için böyle yaptık. (1/5) 24.Video 3.dk
public class FaturaHareket : FullAuditedEntity<Guid>
{
    public Guid FaturaId { get; set; }
    public FaturaHareketTuru HareketTuru { get; set; }
    public Guid? StokId { get; set; }
    public Guid? HizmetId { get; set; }
    public Guid? MasrafId { get; set; }
    public Guid? DepoId { get; set; }
    public decimal Miktar { get; set; }
    public decimal BirimFiyat { get; set; }
    public decimal BrutTutar { get; set; }
    public decimal IndirimTutar { get; set; }
    public int KdvOrani { get; set; }
    public decimal KdvHaricTutar { get; set; }
    public decimal KdvTutar { get; set; }
    public decimal NetTutar { get; set; }
    public string Aciklama { get; set; }

    public Fatura Fatura { get; set; }
    public Stok Stok { get; set; }
    public Hizmet Hizmet { get; set; }
    public Masraf Masraf { get; set; }
    public Depo Depo { get; set; }
}