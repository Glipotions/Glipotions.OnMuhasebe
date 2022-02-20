namespace Glipotions.OnMuhasebe.Subeler;

public class Sube : FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }

    public ICollection<BankaHesap> BankaHesaplar { get; set; }
    public ICollection<Depo> Depolar { get; set; }
    public ICollection<Fatura> Faturalar { get; set; }
    public ICollection<Kasa> Kasalar { get; set; }
    public ICollection<Makbuz> Makbuzlar { get; set; }
    public ICollection<FirmaParametre> FirmaParemetreler { get; set; }
}