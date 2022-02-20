namespace Glipotions.OnMuhasebe.Parametreler;

//Entity den kalıtım yaptık -> Entity en yalın hali
public class FirmaParametre : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public Guid? DepoId { get; set; }

    public IdentityUser User { get; set; } //User'ı bu Classtan ekliyoruz, one to one ilişki. (1/5)-37.video
    public Sube Sube { get; set; }
    public Donem Donem { get; set; }
    public Depo Depo { get; set; }
}