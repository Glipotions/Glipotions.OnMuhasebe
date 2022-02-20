namespace Glipotions.OnMuhasebe.Raporlar;

public class OdemeBelgeleriDagilim : IEntity
{
    public OdemeTuru OdemeTuru { get; set; }
    public decimal Tutar { get; set; }

    public object[] GetKeys()
    {
        throw new NotImplementedException();
    }
}