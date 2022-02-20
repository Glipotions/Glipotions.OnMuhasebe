namespace Glipotions.OnMuhasebe.Birimler;

public class Birim : FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }

    public OzelKod OzelKod1 { get; set; }
    public OzelKod OzelKod2 { get; set; }

    public ICollection<Hizmet> Hizmetler { get; set; }
    public ICollection<Masraf> Masraflar { get; set; }
    public ICollection<Stok> Stoklar { get; set; }
}